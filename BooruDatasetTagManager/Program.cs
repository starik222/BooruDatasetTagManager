using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Translator;
using Newtonsoft.Json;
using System.IO;

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
            Application.SetCompatibleTextRenderingDefault(false);
            tools = new TextTool(Application.StartupPath);
            Settings = new AppSettings(Application.StartupPath);
            TagsList = new TagsDB();
            string tagsDir = Path.Combine(Application.StartupPath, "tags");
            string tagFile = Path.Combine(tagsDir, "list.tag");
            if (File.Exists(tagFile))
            {
                TagsList.LoadFromTagFile(tagFile, false);
            }
            else
            {
                if (Directory.Exists(tagsDir))
                {
                    string[] csvFiles = Directory.GetFiles(tagsDir, "*.csv");
                    foreach (var item in csvFiles)
                    {
                        TagsList.LoadFromCSVFile(item, true);
                    }
                    List<string> temp = new List<string>(TagsList.Tags.Cast<string>());
                    temp.Sort();
                    TagsList.Tags.Clear();
                    TagsList.Tags.AddRange(temp.ToArray());
                    TagsList.SaveTags(tagFile);
                }
            }
            Application.Run(new Form1());
        }

        public static DatasetManager DataManager;

        public static TextTool tools;

        public static AppSettings Settings;

        public static TagsDB TagsList;
    }
}
