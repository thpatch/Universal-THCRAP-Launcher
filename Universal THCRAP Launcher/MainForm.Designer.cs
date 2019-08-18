using System.ComponentModel;
using System.Windows.Forms;

namespace Universal_THCRAP_Launcher
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.patchListBox = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gameListBox = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btn_filterFav2 = new System.Windows.Forms.Button();
            this.btn_filterFav1 = new System.Windows.Forms.Button();
            this.btn_sortAZ2 = new System.Windows.Forms.Button();
            this.btn_sortAZ1 = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.btn_filterByType = new System.Windows.Forms.Button();
            this.btn_AddFavorite0 = new System.Windows.Forms.Button();
            this.btn_AddFavorite1 = new System.Windows.Forms.Button();
            this.btn_Random1 = new System.Windows.Forms.Button();
            this.btn_Random2 = new System.Windows.Forms.Button();
            this.btnDeletePatch = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.appTS = new System.Windows.Forms.ToolStripMenuItem();
            this.createShortcutDesktopTS = new System.Windows.Forms.ToolStripMenuItem();
            this.createShortcutStartMenuTS = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsTS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.restartTS = new System.Windows.Forms.ToolStripMenuItem();
            this.exitTS = new System.Windows.Forms.ToolStripMenuItem();
            this.thcrapTS = new System.Windows.Forms.ToolStripMenuItem();
            this.openConfigureTS = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuTS = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderTS = new System.Windows.Forms.ToolStripMenuItem();
            this.openGamesListTS = new System.Windows.Forms.ToolStripMenuItem();
            this.openSelectedPatchConfigurationTS = new System.Windows.Forms.ToolStripMenuItem();
            this.gitHubTS = new System.Windows.Forms.ToolStripMenuItem();
            this.gitHubPageTS = new System.Windows.Forms.ToolStripMenuItem();
            this.feedbackTS = new System.Windows.Forms.ToolStripMenuItem();
            this.bugReportTS = new System.Windows.Forms.ToolStripMenuItem();
            this.featureRequestTS = new System.Windows.Forms.ToolStripMenuItem();
            this.otherTS = new System.Windows.Forms.ToolStripMenuItem();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpTS = new System.Windows.Forms.ToolStripMenuItem();
            this.keyboardShortcutsTS = new System.Windows.Forms.ToolStripMenuItem();
            this.reportBugTS = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // patchListBox
            // 
            this.patchListBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.patchListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.patchListBox.ForeColor = System.Drawing.SystemColors.Control;
            this.patchListBox.FormattingEnabled = true;
            this.patchListBox.Location = new System.Drawing.Point(0, 1);
            this.patchListBox.Name = "patchListBox";
            this.patchListBox.Size = new System.Drawing.Size(153, 301);
            this.patchListBox.TabIndex = 12;
            this.toolTip1.SetToolTip(this.patchListBox, "Choose the run configuration (patch stack)");
            this.patchListBox.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            this.patchListBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(11, 117);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.patchListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gameListBox);
            this.splitContainer1.Size = new System.Drawing.Size(312, 306);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.SplitContainer1_SplitterMoving);
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer1_SplitterMoved);
            // 
            // gameListBox
            // 
            this.gameListBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.gameListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameListBox.ForeColor = System.Drawing.SystemColors.Control;
            this.gameListBox.FormattingEnabled = true;
            this.gameListBox.Location = new System.Drawing.Point(0, 1);
            this.gameListBox.Name = "gameListBox";
            this.gameListBox.Size = new System.Drawing.Size(152, 301);
            this.gameListBox.TabIndex = 13;
            this.toolTip1.SetToolTip(this.gameListBox, "Choose the game (executable)");
            this.gameListBox.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            this.gameListBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            // 
            // btn_filterFav2
            // 
            this.btn_filterFav2.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Star_Hollow;
            this.btn_filterFav2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_filterFav2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_filterFav2.Location = new System.Drawing.Point(200, 85);
            this.btn_filterFav2.Name = "btn_filterFav2";
            this.btn_filterFav2.Size = new System.Drawing.Size(25, 25);
            this.btn_filterFav2.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btn_filterFav2, "Filter for favourites");
            this.btn_filterFav2.UseVisualStyleBackColor = true;
            this.btn_filterFav2.Click += new System.EventHandler(this.filterButton2_Click);
            // 
            // btn_filterFav1
            // 
            this.btn_filterFav1.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Star_Hollow;
            this.btn_filterFav1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_filterFav1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_filterFav1.Location = new System.Drawing.Point(42, 85);
            this.btn_filterFav1.Name = "btn_filterFav1";
            this.btn_filterFav1.Size = new System.Drawing.Size(25, 25);
            this.btn_filterFav1.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btn_filterFav1, "Filter for favourites");
            this.btn_filterFav1.UseVisualStyleBackColor = true;
            this.btn_filterFav1.Click += new System.EventHandler(this.filterButton1_Click);
            // 
            // btn_sortAZ2
            // 
            this.btn_sortAZ2.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Sort_Ascending;
            this.btn_sortAZ2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_sortAZ2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sortAZ2.Location = new System.Drawing.Point(169, 85);
            this.btn_sortAZ2.Name = "btn_sortAZ2";
            this.btn_sortAZ2.Size = new System.Drawing.Size(25, 25);
            this.btn_sortAZ2.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btn_sortAZ2, "Sort in alphabetical order");
            this.btn_sortAZ2.UseVisualStyleBackColor = true;
            this.btn_sortAZ2.Click += new System.EventHandler(this.sortAZButton2_Click);
            // 
            // btn_sortAZ1
            // 
            this.btn_sortAZ1.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Sort_Ascending;
            this.btn_sortAZ1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_sortAZ1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sortAZ1.Location = new System.Drawing.Point(11, 85);
            this.btn_sortAZ1.Name = "btn_sortAZ1";
            this.btn_sortAZ1.Size = new System.Drawing.Size(25, 25);
            this.btn_sortAZ1.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btn_sortAZ1, "Sort in alphabetical order");
            this.btn_sortAZ1.UseVisualStyleBackColor = true;
            this.btn_sortAZ1.Click += new System.EventHandler(this.sortAZButton1_Click);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.startButton.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Shinmera_Banner_5_mini_size;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.startButton.ForeColor = System.Drawing.Color.Red;
            this.startButton.Location = new System.Drawing.Point(11, 40);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(312, 38);
            this.startButton.TabIndex = 2;
            this.toolTip1.SetToolTip(this.startButton, "Start thcrap with the selected settings");
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            this.startButton.MouseLeave += new System.EventHandler(this.startButton_MouseLeave);
            this.startButton.MouseHover += new System.EventHandler(this.startButton_MouseHover);
            // 
            // btn_filterByType
            // 
            this.btn_filterByType.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.GameAndCustom;
            this.btn_filterByType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_filterByType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_filterByType.Location = new System.Drawing.Point(231, 85);
            this.btn_filterByType.Name = "btn_filterByType";
            this.btn_filterByType.Size = new System.Drawing.Size(25, 25);
            this.btn_filterByType.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btn_filterByType, "Filter by Type");
            this.btn_filterByType.UseVisualStyleBackColor = true;
            this.btn_filterByType.Click += new System.EventHandler(this.filterByType_button_Click);
            // 
            // btn_AddFavorite0
            // 
            this.btn_AddFavorite0.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Favorite;
            this.btn_AddFavorite0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AddFavorite0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddFavorite0.Location = new System.Drawing.Point(73, 85);
            this.btn_AddFavorite0.Name = "btn_AddFavorite0";
            this.btn_AddFavorite0.Size = new System.Drawing.Size(25, 25);
            this.btn_AddFavorite0.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btn_AddFavorite0, "Add patch to favourites");
            this.btn_AddFavorite0.UseVisualStyleBackColor = true;
            this.btn_AddFavorite0.Click += new System.EventHandler(this.Btn_AddFavorite0_Click);
            // 
            // btn_AddFavorite1
            // 
            this.btn_AddFavorite1.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Favorite;
            this.btn_AddFavorite1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AddFavorite1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddFavorite1.Location = new System.Drawing.Point(262, 85);
            this.btn_AddFavorite1.Name = "btn_AddFavorite1";
            this.btn_AddFavorite1.Size = new System.Drawing.Size(25, 25);
            this.btn_AddFavorite1.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btn_AddFavorite1, "Add game or exe to favourites");
            this.btn_AddFavorite1.UseVisualStyleBackColor = true;
            this.btn_AddFavorite1.Click += new System.EventHandler(this.Btn_AddFavorite1_Click);
            // 
            // btn_Random1
            // 
            this.btn_Random1.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Dice;
            this.btn_Random1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Random1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Random1.Location = new System.Drawing.Point(104, 85);
            this.btn_Random1.Name = "btn_Random1";
            this.btn_Random1.Size = new System.Drawing.Size(25, 25);
            this.btn_Random1.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btn_Random1, "Select a random element");
            this.btn_Random1.UseVisualStyleBackColor = true;
            this.btn_Random1.Click += new System.EventHandler(this.Btn_Random1_Click);
            // 
            // btn_Random2
            // 
            this.btn_Random2.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Dice;
            this.btn_Random2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Random2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Random2.Location = new System.Drawing.Point(293, 85);
            this.btn_Random2.Name = "btn_Random2";
            this.btn_Random2.Size = new System.Drawing.Size(25, 25);
            this.btn_Random2.TabIndex = 11;
            this.toolTip1.SetToolTip(this.btn_Random2, "Select something randomly");
            this.btn_Random2.UseVisualStyleBackColor = true;
            this.btn_Random2.Click += new System.EventHandler(this.Btn_Random2_Click);
            // 
            // btnDeletePatch
            // 
            this.btnDeletePatch.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Trash;
            this.btnDeletePatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeletePatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePatch.Location = new System.Drawing.Point(135, 85);
            this.btnDeletePatch.Name = "btnDeletePatch";
            this.btnDeletePatch.Size = new System.Drawing.Size(25, 25);
            this.btnDeletePatch.TabIndex = 12;
            this.toolTip1.SetToolTip(this.btnDeletePatch, "Delete selected patch");
            this.btnDeletePatch.UseVisualStyleBackColor = true;
            this.btnDeletePatch.Click += new System.EventHandler(this.BtnDeletePatch_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appTS,
            this.thcrapTS,
            this.gitHubTS,
            this.helpTS});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(334, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.TabStop = true;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // appTS
            // 
            this.appTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.appTS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createShortcutDesktopTS,
            this.createShortcutStartMenuTS,
            this.settingsTS,
            this.toolStripSeparator1,
            this.restartTS,
            this.exitTS});
            this.appTS.Name = "appTS";
            this.appTS.Size = new System.Drawing.Size(41, 20);
            this.appTS.Text = "App";
            // 
            // createShortcutDesktopTS
            // 
            this.createShortcutDesktopTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.createShortcutDesktopTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.createShortcutDesktopTS.Name = "createShortcutDesktopTS";
            this.createShortcutDesktopTS.Size = new System.Drawing.Size(231, 22);
            this.createShortcutDesktopTS.Text = "Create Shortcut to Desktop";
            this.createShortcutDesktopTS.Click += new System.EventHandler(this.CreateShortcutDesktopTS_Click);
            // 
            // createShortcutStartMenuTS
            // 
            this.createShortcutStartMenuTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.createShortcutStartMenuTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.createShortcutStartMenuTS.Name = "createShortcutStartMenuTS";
            this.createShortcutStartMenuTS.Size = new System.Drawing.Size(231, 22);
            this.createShortcutStartMenuTS.Text = "Create Shortcut to Start Menu";
            this.createShortcutStartMenuTS.Click += new System.EventHandler(this.CreateShortcutStartMenuTS_Click);
            // 
            // settingsTS
            // 
            this.settingsTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.settingsTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsTS.Name = "settingsTS";
            this.settingsTS.Size = new System.Drawing.Size(231, 22);
            this.settingsTS.Text = "Settings...";
            this.settingsTS.Click += new System.EventHandler(this.settingsTS_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(228, 6);
            // 
            // restartTS
            // 
            this.restartTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.restartTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.restartTS.Name = "restartTS";
            this.restartTS.ShortcutKeyDisplayString = "Ctrl+R";
            this.restartTS.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.restartTS.Size = new System.Drawing.Size(231, 22);
            this.restartTS.Text = "Restart";
            this.restartTS.Click += new System.EventHandler(this.restartTS_Click);
            // 
            // exitTS
            // 
            this.exitTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.exitTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitTS.Name = "exitTS";
            this.exitTS.ShortcutKeyDisplayString = "Alt+F4";
            this.exitTS.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitTS.Size = new System.Drawing.Size(231, 22);
            this.exitTS.Text = "Exit";
            this.exitTS.Click += new System.EventHandler(this.exitTS_Click);
            // 
            // thcrapTS
            // 
            this.thcrapTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.thcrapTS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openConfigureTS,
            this.openMenuTS});
            this.thcrapTS.Name = "thcrapTS";
            this.thcrapTS.Size = new System.Drawing.Size(65, 20);
            this.thcrapTS.Text = "THCRAP";
            // 
            // openConfigureTS
            // 
            this.openConfigureTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.openConfigureTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openConfigureTS.Name = "openConfigureTS";
            this.openConfigureTS.Size = new System.Drawing.Size(208, 22);
            this.openConfigureTS.Text = "Open THCRAP Configure";
            this.openConfigureTS.Click += new System.EventHandler(this.openConfigureTS_Click);
            // 
            // openMenuTS
            // 
            this.openMenuTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.openMenuTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openMenuTS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderTS,
            this.openGamesListTS,
            this.openSelectedPatchConfigurationTS});
            this.openMenuTS.Name = "openMenuTS";
            this.openMenuTS.Size = new System.Drawing.Size(208, 22);
            this.openMenuTS.Text = "Open...";
            // 
            // openFolderTS
            // 
            this.openFolderTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.openFolderTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openFolderTS.Name = "openFolderTS";
            this.openFolderTS.Size = new System.Drawing.Size(309, 22);
            this.openFolderTS.Text = "Open Folder";
            this.openFolderTS.Click += new System.EventHandler(this.openFolderTS_Click);
            // 
            // openGamesListTS
            // 
            this.openGamesListTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.openGamesListTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openGamesListTS.Name = "openGamesListTS";
            this.openGamesListTS.Size = new System.Drawing.Size(309, 22);
            this.openGamesListTS.Text = "Open Games List";
            this.openGamesListTS.Click += new System.EventHandler(this.openGamesListTS_Click);
            // 
            // openSelectedPatchConfigurationTS
            // 
            this.openSelectedPatchConfigurationTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.openSelectedPatchConfigurationTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openSelectedPatchConfigurationTS.Name = "openSelectedPatchConfigurationTS";
            this.openSelectedPatchConfigurationTS.ShortcutKeyDisplayString = "Ctrl + O";
            this.openSelectedPatchConfigurationTS.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openSelectedPatchConfigurationTS.Size = new System.Drawing.Size(309, 22);
            this.openSelectedPatchConfigurationTS.Text = "Open Selected Patch Configuration";
            this.openSelectedPatchConfigurationTS.Click += new System.EventHandler(this.openSelectedPatchConfigurationTS_Click);
            // 
            // gitHubTS
            // 
            this.gitHubTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.gitHubTS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gitHubPageTS,
            this.feedbackTS,
            this.donateToolStripMenuItem});
            this.gitHubTS.Name = "gitHubTS";
            this.gitHubTS.Size = new System.Drawing.Size(57, 20);
            this.gitHubTS.Text = "GitHub";
            // 
            // gitHubPageTS
            // 
            this.gitHubPageTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.gitHubPageTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.gitHubPageTS.Name = "gitHubPageTS";
            this.gitHubPageTS.Size = new System.Drawing.Size(180, 22);
            this.gitHubPageTS.Text = "GitHub Page";
            this.gitHubPageTS.Click += new System.EventHandler(this.gitHubPageTS_Click);
            // 
            // feedbackTS
            // 
            this.feedbackTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.feedbackTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.feedbackTS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bugReportTS,
            this.featureRequestTS,
            this.otherTS});
            this.feedbackTS.Name = "feedbackTS";
            this.feedbackTS.Size = new System.Drawing.Size(180, 22);
            this.feedbackTS.Text = "Feedback...";
            // 
            // bugReportTS
            // 
            this.bugReportTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.bugReportTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bugReportTS.Name = "bugReportTS";
            this.bugReportTS.Size = new System.Drawing.Size(180, 22);
            this.bugReportTS.Text = "Bug Report";
            this.bugReportTS.Click += new System.EventHandler(this.bugReportTS_Click);
            // 
            // featureRequestTS
            // 
            this.featureRequestTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.featureRequestTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.featureRequestTS.Name = "featureRequestTS";
            this.featureRequestTS.Size = new System.Drawing.Size(180, 22);
            this.featureRequestTS.Text = "Feature Request";
            this.featureRequestTS.Click += new System.EventHandler(this.featureRequestTS_Click);
            // 
            // otherTS
            // 
            this.otherTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.otherTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.otherTS.Name = "otherTS";
            this.otherTS.Size = new System.Drawing.Size(180, 22);
            this.otherTS.Text = "Other";
            this.otherTS.Click += new System.EventHandler(this.otherTS_Click);
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.donateToolStripMenuItem.Text = "Donate";
            this.donateToolStripMenuItem.Click += new System.EventHandler(this.DonateToolStripMenuItem_Click);
            // 
            // helpTS
            // 
            this.helpTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.helpTS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keyboardShortcutsTS});
            this.helpTS.Name = "helpTS";
            this.helpTS.ShortcutKeyDisplayString = "F1";
            this.helpTS.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpTS.Size = new System.Drawing.Size(44, 20);
            this.helpTS.Text = "Help";
            // 
            // keyboardShortcutsTS
            // 
            this.keyboardShortcutsTS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.keyboardShortcutsTS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.keyboardShortcutsTS.Name = "keyboardShortcutsTS";
            this.keyboardShortcutsTS.ShortcutKeyDisplayString = "F1";
            this.keyboardShortcutsTS.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.keyboardShortcutsTS.Size = new System.Drawing.Size(196, 22);
            this.keyboardShortcutsTS.Text = "Keyboard Shortcuts";
            this.keyboardShortcutsTS.Click += new System.EventHandler(this.keyboardShortcutsTS_Click);
            // 
            // reportBugTS
            // 
            this.reportBugTS.Name = "reportBugTS";
            this.reportBugTS.Size = new System.Drawing.Size(32, 19);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Universal THCRAP Launcher";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(334, 431);
            this.Controls.Add(this.btnDeletePatch);
            this.Controls.Add(this.btn_Random2);
            this.Controls.Add(this.btn_Random1);
            this.Controls.Add(this.btn_AddFavorite1);
            this.Controls.Add(this.btn_AddFavorite0);
            this.Controls.Add(this.btn_filterByType);
            this.Controls.Add(this.btn_filterFav2);
            this.Controls.Add(this.btn_filterFav1);
            this.Controls.Add(this.btn_sortAZ2);
            this.Controls.Add(this.btn_sortAZ1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(350, 300);
            this.Name = "MainForm";
            this.Text = "Universal THCRAP Launcher";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private SplitContainer splitContainer1;
        private ListBox gameListBox;
        private Button startButton;
        private ToolTip toolTip1;
        private Button btn_sortAZ1;
        private Button btn_sortAZ2;
        private Button btn_filterFav1;
        private Button btn_filterFav2;
        private Button btn_filterByType;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem appTS;
        private ToolStripMenuItem restartTS;
        private ToolStripMenuItem exitTS;
        private ToolStripMenuItem helpTS;
        private ToolStripMenuItem gitHubTS;
        private ToolStripMenuItem reportBugTS;
        private ToolStripMenuItem thcrapTS;
        private ToolStripMenuItem openConfigureTS;
        private ToolStripMenuItem openGamesListTS;
        private ToolStripMenuItem openFolderTS;
        private ToolStripMenuItem createShortcutDesktopTS;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem openMenuTS;
        private ToolStripMenuItem openSelectedPatchConfigurationTS;
        private ToolStripMenuItem keyboardShortcutsTS;
        private ToolStripMenuItem settingsTS;
        private ToolStripMenuItem gitHubPageTS;
        private ToolStripMenuItem feedbackTS;
        private ToolStripMenuItem bugReportTS;
        private ToolStripMenuItem featureRequestTS;
        private ToolStripMenuItem otherTS;
        private ListBox patchListBox;
        private Button btn_AddFavorite0;
        private Button btn_AddFavorite1;
        private Button btn_Random1;
        private Button btn_Random2;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem createShortcutStartMenuTS;
        private ToolStripMenuItem donateToolStripMenuItem;
        private Button btnDeletePatch;
    }
}

