using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using System.Threading;
using UmaMusumeDBBrowser;

namespace BooruDatasetTagManager    
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
#if NET5_0_OR_GREATER
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
#endif
            Application.SetCompatibleTextRenderingDefault(false);
            AppPath = Application.StartupPath;
            Settings = new AppSettings(Application.StartupPath);
            EditableTagListLocker = new SemaphoreSlim(1,1);
            ListChangeLocker = new object();
            TranslationLocker = new SemaphoreSlim(1, 1);
            ColorManager = new ColorSchemeManager();
            ColorManager.Load(Path.Combine(Application.StartupPath, "ColorScheme.json"));
            ColorManager.SelectScheme(Program.Settings.ColorScheme);
            #region waitForm
            Form f_wait = new Form();
            I18n.Initialize(Program.Settings.Language);
            Settings.Hotkeys.ChangeLanguage();
            f_wait.AutoScaleMode = AutoScaleMode.Dpi;
            f_wait.Width = 480;
            f_wait.Height = 144;
            f_wait.FormBorderStyle = FormBorderStyle.FixedDialog;
            f_wait.ControlBox = false;
            f_wait.StartPosition = FormStartPosition.CenterScreen;
            Label mes = new Label();
            mes.Text = I18n.GetText("TipTagLoad");
            mes.Location = new System.Drawing.Point(10, 10);
            mes.AutoSize = true;

            f_wait.Controls.Add(mes);

            ColorManager.ChangeColorScheme(f_wait, ColorManager.SelectedScheme);
            ColorManager.ChangeColorSchemeInConteiner(f_wait.Controls, ColorManager.SelectedScheme);
            
            f_wait.Shown += async (o, i) =>
            {
                await Task.Run(() =>
                {
                    string translationsDir = Path.Combine(Application.StartupPath, "Translations");
                    if (!Directory.Exists(translationsDir))
                        Directory.CreateDirectory(translationsDir);
                    TransManager = new TranslationManager(Program.Settings.TranslationLanguage, Program.Settings.TransService, translationsDir);
                    TransManager.LoadTranslations();
                    string tagsDir = Path.Combine(Application.StartupPath, "Tags");
                    if(!Directory.Exists(tagsDir))
                        Directory.CreateDirectory(tagsDir);
                    string tagFile = Path.Combine(tagsDir, "List.tdb");
                    TagsList = TagsDB.LoadFromTagFile(tagFile);
                    if (TagsList == null)
                        TagsList = new TagsDB();
                    if (TagsList.IsNeedUpdate(tagsDir))
                    {
                        TagsList.SetNeedFixTags(Program.Settings.FixTagsOnSaveLoad);
                        TagsList.ClearDb();
                        TagsList.ClearLoadedFiles();
                        TagsList.ResetVersion();
                        TagsList.LoadCSVFromDir(tagsDir);
                        TagsList.LoadTxtFromDir(tagsDir);
                        TagsList.SortTags();
                        TagsList.SaveTags(tagFile);
                    }
                    TagsList.LoadTranslation(TransManager);
                });
                f_wait.Close();
            };
            f_wait.ShowDialog();
            #endregion
            AutoTagger = new Interrogator();

            Application.Run(new MainForm());
        }

        public static string AppPath;

        public static TranslationManager TransManager;

        public static DatasetManager DataManager;

        public static AppSettings Settings;

        public static TagsDB TagsList;

        public static Interrogator AutoTagger;

        public static ColorSchemeManager ColorManager;

        #region locks
        public static SemaphoreSlim EditableTagListLocker;
        public static object ListChangeLocker;
        public static SemaphoreSlim TranslationLocker;
        #endregion
    }
}
