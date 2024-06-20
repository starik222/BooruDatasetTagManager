
namespace BooruDatasetTagManager
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            gridViewTags = new System.Windows.Forms.DataGridView();
            ImageTags = new CustomTextBoxColumn();
            Translation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ImageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Image = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            toolStripTags = new System.Windows.Forms.ToolStrip();
            BtnTagAdd = new System.Windows.Forms.ToolStripButton();
            BtnTagDelete = new System.Windows.Forms.ToolStripButton();
            BtnTagUndo = new System.Windows.Forms.ToolStripButton();
            BtnTagRedo = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            BtnTagCopy = new System.Windows.Forms.ToolStripButton();
            BtnTagPaste = new System.Windows.Forms.ToolStripButton();
            BtnTagSetToAll = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            BtnTagPasteFromClipBoard = new System.Windows.Forms.ToolStripButton();
            BtnTagShow = new System.Windows.Forms.ToolStripButton();
            toolStripSplitButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            BtnMenuGenTagsWithCurrentSettings = new System.Windows.Forms.ToolStripMenuItem();
            BtnMenuGenTagsWithSetWindow = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            BtnTagUp = new System.Windows.Forms.ToolStripButton();
            BtnTagDown = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            BtnTagFindInAll = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAllChangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            MenuShowPreview = new System.Windows.Forms.ToolStripMenuItem();
            MenuItemTranslateTags = new System.Windows.Forms.ToolStripMenuItem();
            MenuShowTagCount = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            MenuHideAllTags = new System.Windows.Forms.ToolStripMenuItem();
            MenuHideTags = new System.Windows.Forms.ToolStripMenuItem();
            MenuHideDataset = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            MenuSetting = new System.Windows.Forms.ToolStripMenuItem();
            settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            autoTaggerSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            MenuLanguage = new System.Windows.Forms.ToolStripMenuItem();
            toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            replaceTransparentBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            generateTagsWithAutoTaggerForAllImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            gridViewAllTags = new System.Windows.Forms.DataGridView();
            TagsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            TranslationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            CountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            toolStripAllTags = new System.Windows.Forms.ToolStrip();
            BtnTagSwitch = new System.Windows.Forms.ToolStripButton();
            BtnTagAddToAll = new System.Windows.Forms.ToolStripButton();
            BtnTagDeleteForAll = new System.Windows.Forms.ToolStripButton();
            BtnTagReplace = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            BtnTagAddToSelected = new System.Windows.Forms.ToolStripButton();
            BtnTagDeleteForSelected = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            BtnTagAddToFiltered = new System.Windows.Forms.ToolStripButton();
            BtnTagDeleteForFiltered = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            BtnTagMultiModeSwitch = new System.Windows.Forms.ToolStripButton();
            BtnImageFilter = new System.Windows.Forms.ToolStripButton();
            BtnImageExitFilter = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            BtnTagFilter = new System.Windows.Forms.ToolStripButton();
            BtnTagExitFilter = new System.Windows.Forms.ToolStripButton();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            toolStripContainer3 = new System.Windows.Forms.ToolStripContainer();
            gridViewDS = new System.Windows.Forms.DataGridView();
            toolStrip4 = new System.Windows.Forms.ToolStrip();
            toolStripLabelDataSet = new System.Windows.Forms.ToolStripLabel();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            toolStrip3 = new System.Windows.Forms.ToolStrip();
            toolStripLabelWeight = new System.Windows.Forms.ToolStripLabel();
            toolStripMenuItemWeight = new ToolStripCustomMenuItem();
            toolStripTextBoxWeight = new System.Windows.Forms.ToolStripTextBox();
            toolStripTagsHeader = new System.Windows.Forms.ToolStrip();
            toolStripLabelImageTags = new System.Windows.Forms.ToolStripLabel();
            toolStripPromptSortBtn = new System.Windows.Forms.ToolStripButton();
            toolStrippromptFixedLengthComboBox = new System.Windows.Forms.ToolStripComboBox();
            toolStripPromptFixTipLabel = new System.Windows.Forms.ToolStripLabel();
            tabControl1 = new Manina.Windows.Forms.TabControl();
            tabAllTags = new Manina.Windows.Forms.Tab();
            toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            toolStripLabelAllTags = new System.Windows.Forms.ToolStripLabel();
            toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            tabAutoTags = new Manina.Windows.Forms.Tab();
            toolStripContainer4 = new System.Windows.Forms.ToolStripContainer();
            gridViewAutoTags = new System.Windows.Forms.DataGridView();
            toolStripAutoTags = new System.Windows.Forms.ToolStrip();
            btnAutoGetTagsDefSet = new System.Windows.Forms.ToolStripButton();
            btnAutoGetTagsOpenSet = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            btnAutoAddSelToImageTags = new System.Windows.Forms.ToolStripButton();
            toolStrip2 = new System.Windows.Forms.ToolStrip();
            toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            tabPreview = new Manina.Windows.Forms.Tab();
            pictureBoxPreview = new System.Windows.Forms.PictureBox();
            customTextBoxColumn1 = new CustomTextBoxColumn();
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            contextMenuImageGridHeader = new System.Windows.Forms.ContextMenuStrip(components);
            ((System.ComponentModel.ISupportInitialize)gridViewTags).BeginInit();
            toolStripTags.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridViewAllTags).BeginInit();
            toolStripAllTags.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            toolStripContainer3.ContentPanel.SuspendLayout();
            toolStripContainer3.TopToolStripPanel.SuspendLayout();
            toolStripContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridViewDS).BeginInit();
            toolStrip4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            toolStripContainer2.BottomToolStripPanel.SuspendLayout();
            toolStripContainer2.ContentPanel.SuspendLayout();
            toolStripContainer2.RightToolStripPanel.SuspendLayout();
            toolStripContainer2.TopToolStripPanel.SuspendLayout();
            toolStripContainer2.SuspendLayout();
            toolStrip3.SuspendLayout();
            toolStripTagsHeader.SuspendLayout();
            tabControl1.SuspendLayout();
            tabAllTags.SuspendLayout();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.RightToolStripPanel.SuspendLayout();
            toolStripContainer1.TopToolStripPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            toolStrip1.SuspendLayout();
            tabAutoTags.SuspendLayout();
            toolStripContainer4.ContentPanel.SuspendLayout();
            toolStripContainer4.RightToolStripPanel.SuspendLayout();
            toolStripContainer4.TopToolStripPanel.SuspendLayout();
            toolStripContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridViewAutoTags).BeginInit();
            toolStripAutoTags.SuspendLayout();
            toolStrip2.SuspendLayout();
            tabPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // gridViewTags
            // 
            gridViewTags.AllowDrop = true;
            gridViewTags.AllowUserToAddRows = false;
            gridViewTags.AllowUserToResizeColumns = false;
            gridViewTags.AllowUserToResizeRows = false;
            gridViewTags.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            gridViewTags.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            gridViewTags.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            gridViewTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridViewTags.ColumnHeadersVisible = false;
            gridViewTags.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ImageTags, Translation, ImageName, Image, Id });
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            gridViewTags.DefaultCellStyle = dataGridViewCellStyle1;
            gridViewTags.Dock = System.Windows.Forms.DockStyle.Fill;
            gridViewTags.Location = new System.Drawing.Point(0, 0);
            gridViewTags.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridViewTags.MultiSelect = false;
            gridViewTags.Name = "gridViewTags";
            gridViewTags.RowHeadersVisible = false;
            gridViewTags.RowHeadersWidth = 72;
            gridViewTags.RowTemplate.Height = 29;
            gridViewTags.Size = new System.Drawing.Size(373, 614);
            gridViewTags.TabIndex = 2;
            gridViewTags.TabStop = false;
            gridViewTags.CellEndEdit += gridViewTags_CellEndEdit;
            gridViewTags.CellMouseEnter += dataGridViewTags_CellMouseEnter;
            gridViewTags.CellMouseLeave += dataGridViewTags_CellMouseLeave;
            gridViewTags.CellValueChanged += gridViewTags_CellValueChanged;
            gridViewTags.EditingControlShowing += dataGridView1_EditingControlShowing;
            gridViewTags.SelectionChanged += gridViewTags_SelectionChanged;
            gridViewTags.DragDrop += dataGridView1_DragDrop;
            gridViewTags.DragOver += dataGridView1_DragOver;
            gridViewTags.Enter += gridView_Enter;
            gridViewTags.KeyDown += dataGridView1_KeyDown;
            gridViewTags.Leave += gridView_Leave;
            gridViewTags.MouseDown += dataGridView1_MouseDown;
            gridViewTags.MouseMove += dataGridView1_MouseMove;
            // 
            // ImageTags
            // 
            ImageTags.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            ImageTags.DataPropertyName = "Tag";
            ImageTags.HeaderText = "Tags";
            ImageTags.MinimumWidth = 9;
            ImageTags.Name = "ImageTags";
            ImageTags.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            ImageTags.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Translation
            // 
            Translation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            Translation.DataPropertyName = "Translation";
            Translation.HeaderText = "Translation";
            Translation.Name = "Translation";
            Translation.ReadOnly = true;
            Translation.Visible = false;
            // 
            // ImageName
            // 
            ImageName.DataPropertyName = "ImageName";
            ImageName.HeaderText = "ImageName";
            ImageName.Name = "ImageName";
            ImageName.ReadOnly = true;
            ImageName.Visible = false;
            ImageName.Width = 5;
            // 
            // Image
            // 
            Image.DataPropertyName = "Image";
            Image.HeaderText = "Image";
            Image.Name = "Image";
            Image.ReadOnly = true;
            Image.Visible = false;
            Image.Width = 5;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Visible = false;
            Id.Width = 5;
            // 
            // toolStripTags
            // 
            toolStripTags.Dock = System.Windows.Forms.DockStyle.None;
            toolStripTags.ImageScalingSize = new System.Drawing.Size(32, 32);
            toolStripTags.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { BtnTagAdd, BtnTagDelete, BtnTagUndo, BtnTagRedo, toolStripSeparator1, BtnTagCopy, BtnTagPaste, BtnTagSetToAll, toolStripSeparator2, BtnTagPasteFromClipBoard, BtnTagShow, toolStripSplitButton1, toolStripSeparator4, BtnTagUp, BtnTagDown, toolStripSeparator7, BtnTagFindInAll, toolStripSeparator9 });
            toolStripTags.Location = new System.Drawing.Point(0, 3);
            toolStripTags.Name = "toolStripTags";
            toolStripTags.Size = new System.Drawing.Size(37, 548);
            toolStripTags.TabIndex = 3;
            toolStripTags.Text = "toolStrip2";
            // 
            // BtnTagAdd
            // 
            BtnTagAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagAdd.Image = Properties.Resources.Add;
            BtnTagAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagAdd.Name = "BtnTagAdd";
            BtnTagAdd.Size = new System.Drawing.Size(35, 36);
            BtnTagAdd.Text = "Add";
            BtnTagAdd.Click += BtnAddTag_Clicked;
            // 
            // BtnTagDelete
            // 
            BtnTagDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagDelete.Image = Properties.Resources.Delete;
            BtnTagDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagDelete.Name = "BtnTagDelete";
            BtnTagDelete.Size = new System.Drawing.Size(35, 36);
            BtnTagDelete.Text = "Delete";
            BtnTagDelete.Click += BtnTagDelete_Click;
            // 
            // BtnTagUndo
            // 
            BtnTagUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagUndo.Image = Properties.Resources.Reset;
            BtnTagUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagUndo.Name = "BtnTagUndo";
            BtnTagUndo.Size = new System.Drawing.Size(35, 36);
            BtnTagUndo.Text = "Undo";
            BtnTagUndo.Click += toolStripButton11_Click;
            // 
            // BtnTagRedo
            // 
            BtnTagRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagRedo.Image = Properties.Resources.Redo;
            BtnTagRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagRedo.Name = "BtnTagRedo";
            BtnTagRedo.Size = new System.Drawing.Size(35, 36);
            BtnTagRedo.Text = "Redo";
            BtnTagRedo.Click += BtnTagRedo_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(35, 6);
            // 
            // BtnTagCopy
            // 
            BtnTagCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagCopy.Image = Properties.Resources.Copy;
            BtnTagCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagCopy.Name = "BtnTagCopy";
            BtnTagCopy.Size = new System.Drawing.Size(35, 36);
            BtnTagCopy.Text = "Copy tags";
            BtnTagCopy.Click += toolStripButton9_Click;
            // 
            // BtnTagPaste
            // 
            BtnTagPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagPaste.Image = Properties.Resources.Paste;
            BtnTagPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagPaste.Name = "BtnTagPaste";
            BtnTagPaste.Size = new System.Drawing.Size(35, 36);
            BtnTagPaste.Text = "Paste tags";
            BtnTagPaste.Click += BtnPasteTag_Click;
            // 
            // BtnTagSetToAll
            // 
            BtnTagSetToAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagSetToAll.Image = Properties.Resources.SetToAll;
            BtnTagSetToAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagSetToAll.Name = "BtnTagSetToAll";
            BtnTagSetToAll.Size = new System.Drawing.Size(35, 36);
            BtnTagSetToAll.Text = "Set current tag list to all images";
            BtnTagSetToAll.Click += toolStripButton17_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(35, 6);
            // 
            // BtnTagPasteFromClipBoard
            // 
            BtnTagPasteFromClipBoard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagPasteFromClipBoard.Image = Properties.Resources.PasteClipboard;
            BtnTagPasteFromClipBoard.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagPasteFromClipBoard.Name = "BtnTagPasteFromClipBoard";
            BtnTagPasteFromClipBoard.Size = new System.Drawing.Size(35, 36);
            BtnTagPasteFromClipBoard.Text = "Paste from clipboard";
            BtnTagPasteFromClipBoard.Click += toolStripButton15_Click;
            // 
            // BtnTagShow
            // 
            BtnTagShow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagShow.Image = Properties.Resources.print;
            BtnTagShow.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagShow.Name = "BtnTagShow";
            BtnTagShow.Size = new System.Drawing.Size(35, 36);
            BtnTagShow.Text = "Show formed tag text";
            BtnTagShow.Click += toolStripButton16_Click;
            // 
            // toolStripSplitButton1
            // 
            toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { BtnMenuGenTagsWithCurrentSettings, BtnMenuGenTagsWithSetWindow });
            toolStripSplitButton1.Image = Properties.Resources.AutoTaggerDef;
            toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripSplitButton1.Name = "toolStripSplitButton1";
            toolStripSplitButton1.ShowDropDownArrow = false;
            toolStripSplitButton1.Size = new System.Drawing.Size(35, 36);
            toolStripSplitButton1.Text = "Auto generate tags";
            // 
            // BtnMenuGenTagsWithCurrentSettings
            // 
            BtnMenuGenTagsWithCurrentSettings.Name = "BtnMenuGenTagsWithCurrentSettings";
            BtnMenuGenTagsWithCurrentSettings.Size = new System.Drawing.Size(270, 22);
            BtnMenuGenTagsWithCurrentSettings.Text = "Generate tags with current settings";
            BtnMenuGenTagsWithCurrentSettings.Click += generateTagsWithCurrentSettingsToolStripMenuItem_Click;
            // 
            // BtnMenuGenTagsWithSetWindow
            // 
            BtnMenuGenTagsWithSetWindow.Name = "BtnMenuGenTagsWithSetWindow";
            BtnMenuGenTagsWithSetWindow.Size = new System.Drawing.Size(270, 22);
            BtnMenuGenTagsWithSetWindow.Text = "Generate tags with settings window...";
            BtnMenuGenTagsWithSetWindow.Click += generateTagsWithSettingsWindowToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(35, 6);
            // 
            // BtnTagUp
            // 
            BtnTagUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagUp.Image = Properties.Resources.Up;
            BtnTagUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagUp.Name = "BtnTagUp";
            BtnTagUp.Size = new System.Drawing.Size(35, 36);
            BtnTagUp.Text = "Up";
            BtnTagUp.Click += toolStripButton4_Click;
            // 
            // BtnTagDown
            // 
            BtnTagDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagDown.Image = Properties.Resources.Down;
            BtnTagDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagDown.Name = "BtnTagDown";
            BtnTagDown.Size = new System.Drawing.Size(35, 36);
            BtnTagDown.Text = "Down";
            BtnTagDown.Click += toolStripButton5_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new System.Drawing.Size(35, 6);
            // 
            // BtnTagFindInAll
            // 
            BtnTagFindInAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagFindInAll.Image = Properties.Resources.Find;
            BtnTagFindInAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagFindInAll.Name = "BtnTagFindInAll";
            BtnTagFindInAll.Size = new System.Drawing.Size(35, 36);
            BtnTagFindInAll.Text = "Fing tag in \"All tags\"";
            BtnTagFindInAll.Click += toolStripButton23_Click;
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new System.Drawing.Size(35, 6);
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem, MenuSetting, toolsToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(1224, 29);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openFolderToolStripMenuItem, saveAllChangesToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(46, 25);
            fileToolStripMenuItem.Text = "File";
            // 
            // openFolderToolStripMenuItem
            // 
            openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            openFolderToolStripMenuItem.Size = new System.Drawing.Size(194, 26);
            openFolderToolStripMenuItem.Text = "Load folder...";
            openFolderToolStripMenuItem.Click += openFolderToolStripMenuItem_Click;
            // 
            // saveAllChangesToolStripMenuItem
            // 
            saveAllChangesToolStripMenuItem.Name = "saveAllChangesToolStripMenuItem";
            saveAllChangesToolStripMenuItem.Size = new System.Drawing.Size(194, 26);
            saveAllChangesToolStripMenuItem.Text = "Save all changes";
            saveAllChangesToolStripMenuItem.Click += saveAllChangesToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { MenuShowPreview, MenuItemTranslateTags, MenuShowTagCount, toolStripSeparator10, MenuHideAllTags, MenuHideTags, MenuHideDataset, toolStripSeparator11 });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(56, 25);
            viewToolStripMenuItem.Text = "View";
            // 
            // MenuShowPreview
            // 
            MenuShowPreview.Name = "MenuShowPreview";
            MenuShowPreview.Size = new System.Drawing.Size(251, 26);
            MenuShowPreview.Text = "Show preview";
            MenuShowPreview.Click += showPreviewToolStripMenuItem_Click;
            // 
            // MenuItemTranslateTags
            // 
            MenuItemTranslateTags.Name = "MenuItemTranslateTags";
            MenuItemTranslateTags.Size = new System.Drawing.Size(251, 26);
            MenuItemTranslateTags.Text = "Translate tags";
            MenuItemTranslateTags.Click += translateTagsToolStripMenuItem_Click;
            // 
            // MenuShowTagCount
            // 
            MenuShowTagCount.Name = "MenuShowTagCount";
            MenuShowTagCount.Size = new System.Drawing.Size(251, 26);
            MenuShowTagCount.Text = "Show tag counts";
            MenuShowTagCount.Click += MenuShowTagCount_Click;
            // 
            // toolStripSeparator10
            // 
            toolStripSeparator10.Name = "toolStripSeparator10";
            toolStripSeparator10.Size = new System.Drawing.Size(248, 6);
            // 
            // MenuHideAllTags
            // 
            MenuHideAllTags.Name = "MenuHideAllTags";
            MenuHideAllTags.Size = new System.Drawing.Size(251, 26);
            MenuHideAllTags.Text = "Hide all tags window";
            MenuHideAllTags.Click += MenuHideAllTags_Click;
            // 
            // MenuHideTags
            // 
            MenuHideTags.Name = "MenuHideTags";
            MenuHideTags.Size = new System.Drawing.Size(251, 26);
            MenuHideTags.Text = "Hide image tags window";
            MenuHideTags.Click += MenuHideTags_Click;
            // 
            // MenuHideDataset
            // 
            MenuHideDataset.Name = "MenuHideDataset";
            MenuHideDataset.Size = new System.Drawing.Size(251, 26);
            MenuHideDataset.Text = "Hide dataset";
            MenuHideDataset.Click += MenuHideDataset_Click;
            // 
            // toolStripSeparator11
            // 
            toolStripSeparator11.Name = "toolStripSeparator11";
            toolStripSeparator11.Size = new System.Drawing.Size(248, 6);
            // 
            // MenuSetting
            // 
            MenuSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { settingsToolStripMenuItem, autoTaggerSettingsToolStripMenuItem, MenuLanguage });
            MenuSetting.Name = "MenuSetting";
            MenuSetting.Size = new System.Drawing.Size(77, 25);
            MenuSetting.Text = "Options";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            settingsToolStripMenuItem.Text = "Settings...";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // autoTaggerSettingsToolStripMenuItem
            // 
            autoTaggerSettingsToolStripMenuItem.Name = "autoTaggerSettingsToolStripMenuItem";
            autoTaggerSettingsToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            autoTaggerSettingsToolStripMenuItem.Text = "Auto tagger settings...";
            autoTaggerSettingsToolStripMenuItem.Click += autoTaggerSettingsToolStripMenuItem_Click;
            // 
            // MenuLanguage
            // 
            MenuLanguage.Name = "MenuLanguage";
            MenuLanguage.Size = new System.Drawing.Size(229, 26);
            MenuLanguage.Text = "Language";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { replaceTransparentBackgroundToolStripMenuItem, generateTagsWithAutoTaggerForAllImagesToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new System.Drawing.Size(57, 25);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // replaceTransparentBackgroundToolStripMenuItem
            // 
            replaceTransparentBackgroundToolStripMenuItem.Name = "replaceTransparentBackgroundToolStripMenuItem";
            replaceTransparentBackgroundToolStripMenuItem.Size = new System.Drawing.Size(437, 26);
            replaceTransparentBackgroundToolStripMenuItem.Text = "Replace transparent background of selected images";
            replaceTransparentBackgroundToolStripMenuItem.Click += replaceTransparentBackgroundToolStripMenuItem_Click;
            // 
            // generateTagsWithAutoTaggerForAllImagesToolStripMenuItem
            // 
            generateTagsWithAutoTaggerForAllImagesToolStripMenuItem.Name = "generateTagsWithAutoTaggerForAllImagesToolStripMenuItem";
            generateTagsWithAutoTaggerForAllImagesToolStripMenuItem.Size = new System.Drawing.Size(437, 26);
            generateTagsWithAutoTaggerForAllImagesToolStripMenuItem.Text = "Generate tags with AutoTagger for all images";
            generateTagsWithAutoTaggerForAllImagesToolStripMenuItem.Click += generateTagsWithAutoTaggerForAllImagesToolStripMenuItem_Click;
            // 
            // gridViewAllTags
            // 
            gridViewAllTags.AllowDrop = true;
            gridViewAllTags.AllowUserToAddRows = false;
            gridViewAllTags.AllowUserToResizeColumns = false;
            gridViewAllTags.AllowUserToResizeRows = false;
            gridViewAllTags.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            gridViewAllTags.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            gridViewAllTags.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            gridViewAllTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridViewAllTags.ColumnHeadersVisible = false;
            gridViewAllTags.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { TagsColumn, TranslationColumn, CountColumn });
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            gridViewAllTags.DefaultCellStyle = dataGridViewCellStyle2;
            gridViewAllTags.Dock = System.Windows.Forms.DockStyle.Fill;
            gridViewAllTags.Location = new System.Drawing.Point(0, 0);
            gridViewAllTags.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridViewAllTags.Name = "gridViewAllTags";
            gridViewAllTags.ReadOnly = true;
            gridViewAllTags.RowHeadersVisible = false;
            gridViewAllTags.RowHeadersWidth = 72;
            gridViewAllTags.RowTemplate.Height = 29;
            gridViewAllTags.RowTemplate.ReadOnly = true;
            gridViewAllTags.Size = new System.Drawing.Size(352, 630);
            gridViewAllTags.TabIndex = 2;
            gridViewAllTags.TabStop = false;
            gridViewAllTags.CellDoubleClick += dataGridView2_CellDoubleClick;
            gridViewAllTags.SelectionChanged += gridViewAllTags_SelectionChanged;
            gridViewAllTags.Enter += gridView_Enter;
            gridViewAllTags.KeyDown += gridViewAllTags_KeyDown;
            gridViewAllTags.KeyPress += gridViewAllTags_KeyPress;
            gridViewAllTags.Leave += gridView_Leave;
            // 
            // TagsColumn
            // 
            TagsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            TagsColumn.DataPropertyName = "Tag";
            TagsColumn.HeaderText = "Tags";
            TagsColumn.Name = "TagsColumn";
            TagsColumn.ReadOnly = true;
            // 
            // TranslationColumn
            // 
            TranslationColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            TranslationColumn.DataPropertyName = "Translation";
            TranslationColumn.HeaderText = "Translation";
            TranslationColumn.Name = "TranslationColumn";
            TranslationColumn.ReadOnly = true;
            TranslationColumn.Visible = false;
            // 
            // CountColumn
            // 
            CountColumn.DataPropertyName = "Count";
            CountColumn.HeaderText = "Count";
            CountColumn.Name = "CountColumn";
            CountColumn.ReadOnly = true;
            CountColumn.Visible = false;
            CountColumn.Width = 5;
            // 
            // toolStripAllTags
            // 
            toolStripAllTags.Dock = System.Windows.Forms.DockStyle.None;
            toolStripAllTags.ImageScalingSize = new System.Drawing.Size(32, 32);
            toolStripAllTags.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { BtnTagSwitch, BtnTagAddToAll, BtnTagDeleteForAll, BtnTagReplace, toolStripSeparator3, BtnTagAddToSelected, BtnTagDeleteForSelected, toolStripSeparator5, BtnTagAddToFiltered, BtnTagDeleteForFiltered, toolStripSeparator6, BtnTagMultiModeSwitch, BtnImageFilter, BtnImageExitFilter, toolStripSeparator8, BtnTagFilter, BtnTagExitFilter });
            toolStripAllTags.Location = new System.Drawing.Point(0, 3);
            toolStripAllTags.Name = "toolStripAllTags";
            toolStripAllTags.Padding = new System.Windows.Forms.Padding(2, 0, 5, 0);
            toolStripAllTags.Size = new System.Drawing.Size(43, 542);
            toolStripAllTags.TabIndex = 3;
            toolStripAllTags.Text = "toolStrip2";
            // 
            // BtnTagSwitch
            // 
            BtnTagSwitch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagSwitch.Image = Properties.Resources.Change;
            BtnTagSwitch.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagSwitch.Name = "BtnTagSwitch";
            BtnTagSwitch.Size = new System.Drawing.Size(35, 36);
            BtnTagSwitch.Text = "Change all tags/common tags";
            BtnTagSwitch.Click += toolStripButton6_Click;
            // 
            // BtnTagAddToAll
            // 
            BtnTagAddToAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagAddToAll.Image = Properties.Resources.Add;
            BtnTagAddToAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagAddToAll.Name = "BtnTagAddToAll";
            BtnTagAddToAll.Size = new System.Drawing.Size(35, 36);
            BtnTagAddToAll.Text = "Add to all";
            BtnTagAddToAll.Click += BtnAddTagForAll_Click;
            // 
            // BtnTagDeleteForAll
            // 
            BtnTagDeleteForAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagDeleteForAll.Image = Properties.Resources.Delete;
            BtnTagDeleteForAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagDeleteForAll.Name = "BtnTagDeleteForAll";
            BtnTagDeleteForAll.Size = new System.Drawing.Size(35, 36);
            BtnTagDeleteForAll.Text = "Remove from all";
            BtnTagDeleteForAll.Click += BtnDeleteTagForAll_Click;
            // 
            // BtnTagReplace
            // 
            BtnTagReplace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagReplace.Image = Properties.Resources.ReplaceTags;
            BtnTagReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagReplace.Name = "BtnTagReplace";
            BtnTagReplace.Size = new System.Drawing.Size(35, 36);
            BtnTagReplace.Text = "Replace tag";
            BtnTagReplace.Click += toolStripButton8_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(35, 6);
            // 
            // BtnTagAddToSelected
            // 
            BtnTagAddToSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagAddToSelected.Image = Properties.Resources.AddSelToTags;
            BtnTagAddToSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagAddToSelected.Name = "BtnTagAddToSelected";
            BtnTagAddToSelected.Size = new System.Drawing.Size(35, 36);
            BtnTagAddToSelected.Text = "Add selected 'All tags' to image tags";
            BtnTagAddToSelected.Click += toolStripButton19_Click;
            // 
            // BtnTagDeleteForSelected
            // 
            BtnTagDeleteForSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagDeleteForSelected.Image = Properties.Resources.DelSelFromTags;
            BtnTagDeleteForSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagDeleteForSelected.Name = "BtnTagDeleteForSelected";
            BtnTagDeleteForSelected.Size = new System.Drawing.Size(35, 36);
            BtnTagDeleteForSelected.Text = "Remove selected 'All tags' from image tags";
            BtnTagDeleteForSelected.Click += toolStripButton20_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(35, 6);
            // 
            // BtnTagAddToFiltered
            // 
            BtnTagAddToFiltered.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagAddToFiltered.Image = Properties.Resources.AddToFiltered;
            BtnTagAddToFiltered.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagAddToFiltered.Name = "BtnTagAddToFiltered";
            BtnTagAddToFiltered.Size = new System.Drawing.Size(35, 36);
            BtnTagAddToFiltered.Text = "Add to filtered";
            BtnTagAddToFiltered.Click += toolStripButton21_Click;
            // 
            // BtnTagDeleteForFiltered
            // 
            BtnTagDeleteForFiltered.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagDeleteForFiltered.Image = Properties.Resources.DelFromFiltered;
            BtnTagDeleteForFiltered.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagDeleteForFiltered.Name = "BtnTagDeleteForFiltered";
            BtnTagDeleteForFiltered.Size = new System.Drawing.Size(35, 36);
            BtnTagDeleteForFiltered.Text = "Remove from filtered";
            BtnTagDeleteForFiltered.Click += toolStripButton22_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new System.Drawing.Size(35, 6);
            // 
            // BtnTagMultiModeSwitch
            // 
            BtnTagMultiModeSwitch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagMultiModeSwitch.Image = Properties.Resources.ORIcon;
            BtnTagMultiModeSwitch.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagMultiModeSwitch.Name = "BtnTagMultiModeSwitch";
            BtnTagMultiModeSwitch.Size = new System.Drawing.Size(35, 36);
            BtnTagMultiModeSwitch.Text = "Multi-tag filtering mode";
            BtnTagMultiModeSwitch.Click += toolStripButton18_Click;
            // 
            // BtnImageFilter
            // 
            BtnImageFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnImageFilter.Image = Properties.Resources.Find;
            BtnImageFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnImageFilter.Name = "BtnImageFilter";
            BtnImageFilter.Size = new System.Drawing.Size(35, 36);
            BtnImageFilter.Text = "Find in dataset";
            BtnImageFilter.Click += toolStripButton13_Click;
            // 
            // BtnImageExitFilter
            // 
            BtnImageExitFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnImageExitFilter.Enabled = false;
            BtnImageExitFilter.Image = Properties.Resources.ResetFilter;
            BtnImageExitFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnImageExitFilter.Name = "BtnImageExitFilter";
            BtnImageExitFilter.Size = new System.Drawing.Size(35, 36);
            BtnImageExitFilter.Text = "Reset filter";
            BtnImageExitFilter.Click += toolStripButton14_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new System.Drawing.Size(35, 6);
            // 
            // BtnTagFilter
            // 
            BtnTagFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagFilter.Image = Properties.Resources.filterAdd;
            BtnTagFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagFilter.Name = "BtnTagFilter";
            BtnTagFilter.Size = new System.Drawing.Size(35, 36);
            BtnTagFilter.Text = "Filter in all tags";
            BtnTagFilter.Click += toolStripButton24_Click;
            // 
            // BtnTagExitFilter
            // 
            BtnTagExitFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTagExitFilter.Image = Properties.Resources.filterRemove;
            BtnTagExitFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTagExitFilter.Name = "BtnTagExitFilter";
            BtnTagExitFilter.Size = new System.Drawing.Size(35, 36);
            BtnTagExitFilter.Text = "Remove filter";
            BtnTagExitFilter.Click += toolStripButton25_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { statusLabel });
            statusStrip1.Location = new System.Drawing.Point(0, 706);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusStrip1.Size = new System.Drawing.Size(1224, 22);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(12, 17);
            statusLabel.Text = "-";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 29);
            splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(toolStripContainer3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new System.Drawing.Size(1224, 677);
            splitContainer1.SplitterDistance = 410;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 7;
            // 
            // toolStripContainer3
            // 
            // 
            // toolStripContainer3.ContentPanel
            // 
            toolStripContainer3.ContentPanel.Controls.Add(gridViewDS);
            toolStripContainer3.ContentPanel.Size = new System.Drawing.Size(410, 653);
            toolStripContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            toolStripContainer3.Location = new System.Drawing.Point(0, 0);
            toolStripContainer3.Name = "toolStripContainer3";
            toolStripContainer3.Size = new System.Drawing.Size(410, 677);
            toolStripContainer3.TabIndex = 7;
            toolStripContainer3.Text = "toolStripContainer3";
            // 
            // toolStripContainer3.TopToolStripPanel
            // 
            toolStripContainer3.TopToolStripPanel.Controls.Add(toolStrip4);
            // 
            // gridViewDS
            // 
            gridViewDS.AllowUserToAddRows = false;
            gridViewDS.AllowUserToDeleteRows = false;
            gridViewDS.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            gridViewDS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            gridViewDS.ColumnHeadersHeight = 40;
            gridViewDS.Dock = System.Windows.Forms.DockStyle.Fill;
            gridViewDS.Location = new System.Drawing.Point(0, 0);
            gridViewDS.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridViewDS.Name = "gridViewDS";
            gridViewDS.ReadOnly = true;
            gridViewDS.RowHeadersVisible = false;
            gridViewDS.RowHeadersWidth = 72;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            gridViewDS.RowsDefaultCellStyle = dataGridViewCellStyle3;
            gridViewDS.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            gridViewDS.RowTemplate.Height = 140;
            gridViewDS.RowTemplate.ReadOnly = true;
            gridViewDS.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            gridViewDS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            gridViewDS.Size = new System.Drawing.Size(410, 653);
            gridViewDS.TabIndex = 6;
            gridViewDS.TabStop = false;
            gridViewDS.DataSourceChanged += dataGridView3_DataSourceChanged;
            gridViewDS.CellMouseDown += gridViewDS_CellMouseDown;
            gridViewDS.ColumnHeaderMouseClick += gridViewDS_ColumnHeaderMouseClick;
            gridViewDS.SelectionChanged += dataGridView3_SelectionChanged;
            gridViewDS.Enter += gridView_Enter;
            gridViewDS.KeyDown += gridViewDS_KeyDown;
            gridViewDS.KeyUp += gridViewDS_KeyUp;
            gridViewDS.Leave += gridView_Leave;
            gridViewDS.MouseDoubleClick += gridViewDS_MouseDoubleClick;
            // 
            // toolStrip4
            // 
            toolStrip4.Dock = System.Windows.Forms.DockStyle.None;
            toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripLabelDataSet });
            toolStrip4.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            toolStrip4.Location = new System.Drawing.Point(3, 0);
            toolStrip4.Name = "toolStrip4";
            toolStrip4.Size = new System.Drawing.Size(63, 24);
            toolStrip4.TabIndex = 0;
            // 
            // toolStripLabelDataSet
            // 
            toolStripLabelDataSet.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            toolStripLabelDataSet.Name = "toolStripLabelDataSet";
            toolStripLabelDataSet.Size = new System.Drawing.Size(62, 21);
            toolStripLabelDataSet.Text = "Dataset";
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.Location = new System.Drawing.Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(toolStripContainer2);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(tabControl1);
            splitContainer2.Size = new System.Drawing.Size(809, 677);
            splitContainer2.SplitterDistance = 410;
            splitContainer2.TabIndex = 8;
            // 
            // toolStripContainer2
            // 
            // 
            // toolStripContainer2.BottomToolStripPanel
            // 
            toolStripContainer2.BottomToolStripPanel.Controls.Add(toolStrip3);
            // 
            // toolStripContainer2.ContentPanel
            // 
            toolStripContainer2.ContentPanel.Controls.Add(gridViewTags);
            toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(373, 614);
            toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            toolStripContainer2.Location = new System.Drawing.Point(0, 0);
            toolStripContainer2.Name = "toolStripContainer2";
            // 
            // toolStripContainer2.RightToolStripPanel
            // 
            toolStripContainer2.RightToolStripPanel.Controls.Add(toolStripTags);
            toolStripContainer2.Size = new System.Drawing.Size(410, 677);
            toolStripContainer2.TabIndex = 0;
            toolStripContainer2.Text = "toolStripContainer2";
            // 
            // toolStripContainer2.TopToolStripPanel
            // 
            toolStripContainer2.TopToolStripPanel.Controls.Add(toolStripTagsHeader);
            // 
            // toolStrip3
            // 
            toolStrip3.Dock = System.Windows.Forms.DockStyle.None;
            toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripLabelWeight, toolStripMenuItemWeight, toolStripTextBoxWeight });
            toolStrip3.Location = new System.Drawing.Point(3, 0);
            toolStrip3.Name = "toolStrip3";
            toolStrip3.Size = new System.Drawing.Size(362, 38);
            toolStrip3.TabIndex = 0;
            // 
            // toolStripLabelWeight
            // 
            toolStripLabelWeight.Name = "toolStripLabelWeight";
            toolStripLabelWeight.Size = new System.Drawing.Size(48, 35);
            toolStripLabelWeight.Text = "Weight:";
            // 
            // toolStripMenuItemWeight
            // 
            toolStripMenuItemWeight.Name = "toolStripMenuItemWeight";
            toolStripMenuItemWeight.Size = new System.Drawing.Size(200, 35);
            toolStripMenuItemWeight.Text = "toolStripMenuItem3";
            toolStripMenuItemWeight.ValueChanged += toolStripMenuItemWeight_ValueChanged;
            // 
            // toolStripTextBoxWeight
            // 
            toolStripTextBoxWeight.Name = "toolStripTextBoxWeight";
            toolStripTextBoxWeight.ReadOnly = true;
            toolStripTextBoxWeight.Size = new System.Drawing.Size(100, 38);
            toolStripTextBoxWeight.Text = "1";
            // 
            // toolStripTagsHeader
            // 
            toolStripTagsHeader.Dock = System.Windows.Forms.DockStyle.None;
            toolStripTagsHeader.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStripTagsHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripLabelImageTags, toolStripPromptSortBtn, toolStrippromptFixedLengthComboBox, toolStripPromptFixTipLabel });
            toolStripTagsHeader.Location = new System.Drawing.Point(3, 0);
            toolStripTagsHeader.Name = "toolStripTagsHeader";
            toolStripTagsHeader.Size = new System.Drawing.Size(317, 25);
            toolStripTagsHeader.TabIndex = 0;
            // 
            // toolStripLabelImageTags
            // 
            toolStripLabelImageTags.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            toolStripLabelImageTags.Name = "toolStripLabelImageTags";
            toolStripLabelImageTags.Size = new System.Drawing.Size(86, 22);
            toolStripLabelImageTags.Text = "Image tags";
            // 
            // toolStripPromptSortBtn
            // 
            toolStripPromptSortBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            toolStripPromptSortBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripPromptSortBtn.Image = Properties.Resources.Down;
            toolStripPromptSortBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripPromptSortBtn.Name = "toolStripPromptSortBtn";
            toolStripPromptSortBtn.Size = new System.Drawing.Size(23, 22);
            toolStripPromptSortBtn.Text = "Sort";
            toolStripPromptSortBtn.Click += promptSortBtn_Click;
            // 
            // toolStrippromptFixedLengthComboBox
            // 
            toolStrippromptFixedLengthComboBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            toolStrippromptFixedLengthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStrippromptFixedLengthComboBox.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            toolStrippromptFixedLengthComboBox.Name = "toolStrippromptFixedLengthComboBox";
            toolStrippromptFixedLengthComboBox.Size = new System.Drawing.Size(75, 25);
            // 
            // toolStripPromptFixTipLabel
            // 
            toolStripPromptFixTipLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            toolStripPromptFixTipLabel.Name = "toolStripPromptFixTipLabel";
            toolStripPromptFixTipLabel.Size = new System.Drawing.Size(128, 22);
            toolStripPromptFixTipLabel.Text = "Don't sort first N rows :";
            // 
            // tabControl1
            // 
            tabControl1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tabControl1.Controls.Add(tabAllTags);
            tabControl1.Controls.Add(tabAutoTags);
            tabControl1.Controls.Add(tabPreview);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(395, 677);
            tabControl1.TabIndex = 0;
            tabControl1.TabLocation = Manina.Windows.Forms.TabLocation.Near | Manina.Windows.Forms.TabLocation.Bottom;
            tabControl1.Tabs.Add(tabAllTags);
            tabControl1.Tabs.Add(tabAutoTags);
            tabControl1.Tabs.Add(tabPreview);
            tabControl1.TabSizing = Manina.Windows.Forms.TabSizing.Stretch;
            // 
            // tabAllTags
            // 
            tabAllTags.Controls.Add(toolStripContainer1);
            tabAllTags.Location = new System.Drawing.Point(0, 0);
            tabAllTags.Name = "tabAllTags";
            tabAllTags.Size = new System.Drawing.Size(395, 655);
            tabAllTags.Text = "All / Common tags";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            toolStripContainer1.ContentPanel.Controls.Add(gridViewAllTags);
            toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(352, 630);
            toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.RightToolStripPanel
            // 
            toolStripContainer1.RightToolStripPanel.Controls.Add(toolStripAllTags);
            toolStripContainer1.Size = new System.Drawing.Size(395, 655);
            toolStripContainer1.TabIndex = 0;
            toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripLabelAllTags, toolStripTextBox1, toolStripButton1 });
            toolStrip1.Location = new System.Drawing.Point(3, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(64, 25);
            toolStrip1.TabIndex = 0;
            // 
            // toolStripLabelAllTags
            // 
            toolStripLabelAllTags.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            toolStripLabelAllTags.Name = "toolStripLabelAllTags";
            toolStripLabelAllTags.Size = new System.Drawing.Size(61, 22);
            toolStripLabelAllTags.Text = "All tags";
            // 
            // toolStripTextBox1
            // 
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.Size = new System.Drawing.Size(229, 25);
            toolStripTextBox1.Visible = false;
            toolStripTextBox1.KeyDown += textBox1_KeyDown;
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = Properties.Resources.Delete;
            toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new System.Drawing.Size(23, 22);
            toolStripButton1.Text = "toolStripButton1";
            toolStripButton1.Visible = false;
            toolStripButton1.Click += button1_Click;
            // 
            // tabAutoTags
            // 
            tabAutoTags.Controls.Add(toolStripContainer4);
            tabAutoTags.Location = new System.Drawing.Point(0, 0);
            tabAutoTags.Name = "tabAutoTags";
            tabAutoTags.Size = new System.Drawing.Size(0, 0);
            tabAutoTags.Text = "AutoTagger preview window";
            // 
            // toolStripContainer4
            // 
            // 
            // toolStripContainer4.ContentPanel
            // 
            toolStripContainer4.ContentPanel.Controls.Add(gridViewAutoTags);
            toolStripContainer4.ContentPanel.Size = new System.Drawing.Size(0, 0);
            toolStripContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            toolStripContainer4.Location = new System.Drawing.Point(0, 0);
            toolStripContainer4.Name = "toolStripContainer4";
            // 
            // toolStripContainer4.RightToolStripPanel
            // 
            toolStripContainer4.RightToolStripPanel.Controls.Add(toolStripAutoTags);
            toolStripContainer4.Size = new System.Drawing.Size(0, 0);
            toolStripContainer4.TabIndex = 0;
            toolStripContainer4.Text = "toolStripContainer4";
            // 
            // toolStripContainer4.TopToolStripPanel
            // 
            toolStripContainer4.TopToolStripPanel.Controls.Add(toolStrip2);
            // 
            // gridViewAutoTags
            // 
            gridViewAutoTags.AllowUserToAddRows = false;
            gridViewAutoTags.AllowUserToDeleteRows = false;
            gridViewAutoTags.AllowUserToResizeRows = false;
            gridViewAutoTags.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            gridViewAutoTags.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            gridViewAutoTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            gridViewAutoTags.DefaultCellStyle = dataGridViewCellStyle4;
            gridViewAutoTags.Dock = System.Windows.Forms.DockStyle.Fill;
            gridViewAutoTags.Location = new System.Drawing.Point(0, 0);
            gridViewAutoTags.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridViewAutoTags.Name = "gridViewAutoTags";
            gridViewAutoTags.ReadOnly = true;
            gridViewAutoTags.RowHeadersVisible = false;
            gridViewAutoTags.RowHeadersWidth = 72;
            gridViewAutoTags.RowTemplate.Height = 29;
            gridViewAutoTags.Size = new System.Drawing.Size(0, 0);
            gridViewAutoTags.TabIndex = 0;
            gridViewAutoTags.CellDoubleClick += gridViewAutoTags_CellDoubleClick;
            // 
            // toolStripAutoTags
            // 
            toolStripAutoTags.Dock = System.Windows.Forms.DockStyle.None;
            toolStripAutoTags.ImageScalingSize = new System.Drawing.Size(32, 32);
            toolStripAutoTags.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { btnAutoGetTagsDefSet, btnAutoGetTagsOpenSet, toolStripSeparator12, btnAutoAddSelToImageTags });
            toolStripAutoTags.Location = new System.Drawing.Point(0, 3);
            toolStripAutoTags.Name = "toolStripAutoTags";
            toolStripAutoTags.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            toolStripAutoTags.Size = new System.Drawing.Size(41, 50);
            toolStripAutoTags.TabIndex = 0;
            // 
            // btnAutoGetTagsDefSet
            // 
            btnAutoGetTagsDefSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            btnAutoGetTagsDefSet.Image = Properties.Resources.AutoTaggerDef;
            btnAutoGetTagsDefSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnAutoGetTagsDefSet.Name = "btnAutoGetTagsDefSet";
            btnAutoGetTagsDefSet.Size = new System.Drawing.Size(36, 36);
            btnAutoGetTagsDefSet.Text = "Generate tags with current settings";
            btnAutoGetTagsDefSet.Click += BtnAutoGetTagsDefSet_Click;
            // 
            // btnAutoGetTagsOpenSet
            // 
            btnAutoGetTagsOpenSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            btnAutoGetTagsOpenSet.Image = Properties.Resources.AutoTaggerSet;
            btnAutoGetTagsOpenSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnAutoGetTagsOpenSet.Name = "btnAutoGetTagsOpenSet";
            btnAutoGetTagsOpenSet.Size = new System.Drawing.Size(36, 36);
            btnAutoGetTagsOpenSet.Text = "Generate tags with settings window...";
            btnAutoGetTagsOpenSet.Click += btnAutoGetTagsOpenSet_Click;
            // 
            // toolStripSeparator12
            // 
            toolStripSeparator12.Name = "toolStripSeparator12";
            toolStripSeparator12.Size = new System.Drawing.Size(6, 39);
            // 
            // btnAutoAddSelToImageTags
            // 
            btnAutoAddSelToImageTags.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            btnAutoAddSelToImageTags.Image = Properties.Resources.Add;
            btnAutoAddSelToImageTags.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnAutoAddSelToImageTags.Name = "btnAutoAddSelToImageTags";
            btnAutoAddSelToImageTags.Size = new System.Drawing.Size(36, 36);
            btnAutoAddSelToImageTags.Text = "Add selected tags to image tags";
            btnAutoAddSelToImageTags.Click += toolStripButton2_Click;
            // 
            // toolStrip2
            // 
            toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripLabel1 });
            toolStrip2.Location = new System.Drawing.Point(3, 0);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Size = new System.Drawing.Size(50, 25);
            toolStrip2.TabIndex = 0;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new System.Drawing.Size(150, 22);
            toolStripLabel1.Text = "Auto generated tags";
            // 
            // tabPreview
            // 
            tabPreview.BackColor = System.Drawing.SystemColors.AppWorkspace;
            tabPreview.Controls.Add(pictureBoxPreview);
            tabPreview.Location = new System.Drawing.Point(0, 0);
            tabPreview.Name = "tabPreview";
            tabPreview.Size = new System.Drawing.Size(0, 0);
            tabPreview.Text = "Preview";
            // 
            // pictureBoxPreview
            // 
            pictureBoxPreview.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBoxPreview.Location = new System.Drawing.Point(0, 0);
            pictureBoxPreview.Name = "pictureBoxPreview";
            pictureBoxPreview.Size = new System.Drawing.Size(0, 0);
            pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBoxPreview.TabIndex = 0;
            pictureBoxPreview.TabStop = false;
            // 
            // customTextBoxColumn1
            // 
            customTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            customTextBoxColumn1.HeaderText = "Tags";
            customTextBoxColumn1.MinimumWidth = 9;
            customTextBoxColumn1.Name = "customTextBoxColumn1";
            customTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            customTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(192, 48);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(191, 22);
            toolStripMenuItem1.Text = "Open folder";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(191, 22);
            toolStripMenuItem2.Text = "Delete image and tags";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // contextMenuImageGridHeader
            // 
            contextMenuImageGridHeader.Name = "contextMenuImageGridHeader";
            contextMenuImageGridHeader.Size = new System.Drawing.Size(61, 4);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(1224, 728);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MainForm";
            Text = "BooruDatasetTagManager";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)gridViewTags).EndInit();
            toolStripTags.ResumeLayout(false);
            toolStripTags.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridViewAllTags).EndInit();
            toolStripAllTags.ResumeLayout(false);
            toolStripAllTags.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            toolStripContainer3.ContentPanel.ResumeLayout(false);
            toolStripContainer3.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer3.TopToolStripPanel.PerformLayout();
            toolStripContainer3.ResumeLayout(false);
            toolStripContainer3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridViewDS).EndInit();
            toolStrip4.ResumeLayout(false);
            toolStrip4.PerformLayout();
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            toolStripContainer2.BottomToolStripPanel.ResumeLayout(false);
            toolStripContainer2.BottomToolStripPanel.PerformLayout();
            toolStripContainer2.ContentPanel.ResumeLayout(false);
            toolStripContainer2.RightToolStripPanel.ResumeLayout(false);
            toolStripContainer2.RightToolStripPanel.PerformLayout();
            toolStripContainer2.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer2.TopToolStripPanel.PerformLayout();
            toolStripContainer2.ResumeLayout(false);
            toolStripContainer2.PerformLayout();
            toolStrip3.ResumeLayout(false);
            toolStrip3.PerformLayout();
            toolStripTagsHeader.ResumeLayout(false);
            toolStripTagsHeader.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabAllTags.ResumeLayout(false);
            toolStripContainer1.ContentPanel.ResumeLayout(false);
            toolStripContainer1.RightToolStripPanel.ResumeLayout(false);
            toolStripContainer1.RightToolStripPanel.PerformLayout();
            toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.PerformLayout();
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tabAutoTags.ResumeLayout(false);
            toolStripContainer4.ContentPanel.ResumeLayout(false);
            toolStripContainer4.RightToolStripPanel.ResumeLayout(false);
            toolStripContainer4.RightToolStripPanel.PerformLayout();
            toolStripContainer4.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer4.TopToolStripPanel.PerformLayout();
            toolStripContainer4.ResumeLayout(false);
            toolStripContainer4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridViewAutoTags).EndInit();
            toolStripAutoTags.ResumeLayout(false);
            toolStripAutoTags.PerformLayout();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            tabPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.DataGridView gridViewTags;
        private System.Windows.Forms.ToolStrip toolStripTags;
        private System.Windows.Forms.ToolStripButton BtnTagAdd;
        private System.Windows.Forms.ToolStripButton BtnTagDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton BtnTagUp;
        private System.Windows.Forms.ToolStripButton BtnTagDown;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllChangesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuShowPreview;
        private System.Windows.Forms.DataGridView gridViewAllTags;
        private System.Windows.Forms.ToolStrip toolStripAllTags;
        private System.Windows.Forms.ToolStripButton BtnTagSwitch;
        private System.Windows.Forms.ToolStripButton BtnTagAddToAll;
        private System.Windows.Forms.ToolStripButton BtnTagReplace;
        private System.Windows.Forms.ToolStripButton BtnTagCopy;
        private System.Windows.Forms.ToolStripButton BtnTagPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripButton BtnTagUndo;
        private System.Windows.Forms.ToolStripButton BtnTagDeleteForAll;
        private System.Windows.Forms.ToolStripMenuItem MenuItemTranslateTags;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton BtnImageFilter;
        private System.Windows.Forms.ToolStripButton BtnImageExitFilter;
        private System.Windows.Forms.ToolStripButton BtnTagPasteFromClipBoard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton BtnTagShow;
        private System.Windows.Forms.ToolStripButton BtnTagSetToAll;
        private System.Windows.Forms.DataGridView gridViewDS;
        private System.Windows.Forms.ToolStripButton BtnTagMultiModeSwitch;
        private System.Windows.Forms.ToolStripButton BtnTagAddToSelected;
        private System.Windows.Forms.ToolStripButton BtnTagDeleteForSelected;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton BtnTagAddToFiltered;
        private System.Windows.Forms.ToolStripButton BtnTagDeleteForFiltered;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton BtnTagFindInAll;
        private CustomTextBoxColumn customTextBoxColumn1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton BtnTagFilter;
        private System.Windows.Forms.ToolStripButton BtnTagExitFilter;
        private System.Windows.Forms.ToolStripButton promptSortButton;
        private System.Windows.Forms.ToolStripMenuItem MenuSetting;
        private System.Windows.Forms.ToolStripMenuItem MenuShowTagCount;
        private CustomTextBoxColumn ImageTags;
        private System.Windows.Forms.DataGridViewTextBoxColumn Translation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImageName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Image;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.ToolStripButton BtnTagRedo;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuLanguage;
        private System.Windows.Forms.ToolStripMenuItem replaceTransparentBackgroundToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer2;
        private System.Windows.Forms.ToolStrip toolStripTagsHeader;
        private System.Windows.Forms.ToolStripLabel toolStripLabelImageTags;
        private System.Windows.Forms.ToolStripContainer toolStripContainer3;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripLabel toolStripLabelDataSet;
        private System.Windows.Forms.ToolStripLabel toolStripPromptFixTipLabel;
        private System.Windows.Forms.ToolStripComboBox toolStrippromptFixedLengthComboBox;
        private System.Windows.Forms.ToolStripButton toolStripPromptSortBtn;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelAllTags;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private Manina.Windows.Forms.TabControl tabControl1;
        private Manina.Windows.Forms.Tab tabAllTags;
        private Manina.Windows.Forms.Tab tabAutoTags;
        private System.Windows.Forms.ToolStripContainer toolStripContainer4;
        private System.Windows.Forms.ToolStrip toolStripAutoTags;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.DataGridView gridViewAutoTags;
        private System.Windows.Forms.ToolStripButton btnAutoGetTagsDefSet;
        private System.Windows.Forms.ToolStripMenuItem autoTaggerSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripDropDownButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem BtnMenuGenTagsWithCurrentSettings;
        private System.Windows.Forms.ToolStripMenuItem BtnMenuGenTagsWithSetWindow;
        private System.Windows.Forms.ToolStripButton btnAutoGetTagsOpenSet;
        private System.Windows.Forms.ToolStripMenuItem MenuHideAllTags;
        private System.Windows.Forms.ToolStripMenuItem MenuHideTags;
        private System.Windows.Forms.ToolStripMenuItem MenuHideDataset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabelWeight;
        private ToolStripCustomMenuItem toolStripMenuItemWeight;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxWeight;
        private System.Windows.Forms.ToolStripMenuItem generateTagsWithAutoTaggerForAllImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton btnAutoAddSelToImageTags;
        private System.Windows.Forms.DataGridViewTextBoxColumn TagsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TranslationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountColumn;
        private Manina.Windows.Forms.Tab tabPreview;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.ContextMenuStrip contextMenuImageGridHeader;
    }
}

