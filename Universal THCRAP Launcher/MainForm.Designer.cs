using System.ComponentModel;
using System.Drawing;
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
            this.filterFavButton2 = new System.Windows.Forms.Button();
            this.filterFavButton1 = new System.Windows.Forms.Button();
            this.sortAZButton2 = new System.Windows.Forms.Button();
            this.sortAZButton1 = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.filterByType_button = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.appTS = new System.Windows.Forms.ToolStripMenuItem();
            this.createShortcutTS = new System.Windows.Forms.ToolStripMenuItem();
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
            this.helpTS = new System.Windows.Forms.ToolStripMenuItem();
            this.keyboardShortcutsTS = new System.Windows.Forms.ToolStripMenuItem();
            this.reportBugTS = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // patchListBox
            // 
            this.patchListBox.FormattingEnabled = true;
            this.patchListBox.Location = new System.Drawing.Point(0, 1);
            this.patchListBox.Name = "patchListBox";
            this.patchListBox.Size = new System.Drawing.Size(153, 303);
            this.patchListBox.TabIndex = 8;
            this.toolTip1.SetToolTip(this.patchListBox, "Choose the run configuration (patch stack)");
            this.patchListBox.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            this.patchListBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            // 
            // splitContainer1
            // 
            this.splitContainer1.IsSplitterFixed = true;
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
            // 
            // gameListBox
            // 
            this.gameListBox.FormattingEnabled = true;
            this.gameListBox.Location = new System.Drawing.Point(0, 1);
            this.gameListBox.Name = "gameListBox";
            this.gameListBox.Size = new System.Drawing.Size(152, 303);
            this.gameListBox.TabIndex = 9;
            this.toolTip1.SetToolTip(this.gameListBox, "Choose the game (executable)");
            this.gameListBox.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            this.gameListBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            // 
            // filterFavButton2
            // 
            this.filterFavButton2.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Star_Hollow;
            this.filterFavButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.filterFavButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterFavButton2.Location = new System.Drawing.Point(200, 85);
            this.filterFavButton2.Name = "filterFavButton2";
            this.filterFavButton2.Size = new System.Drawing.Size(25, 25);
            this.filterFavButton2.TabIndex = 6;
            this.toolTip1.SetToolTip(this.filterFavButton2, "Filter for favourites");
            this.filterFavButton2.UseVisualStyleBackColor = true;
            this.filterFavButton2.Click += new System.EventHandler(this.filterButton2_Click);
            // 
            // filterFavButton1
            // 
            this.filterFavButton1.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Star_Hollow;
            this.filterFavButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.filterFavButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterFavButton1.Location = new System.Drawing.Point(42, 85);
            this.filterFavButton1.Name = "filterFavButton1";
            this.filterFavButton1.Size = new System.Drawing.Size(25, 25);
            this.filterFavButton1.TabIndex = 4;
            this.toolTip1.SetToolTip(this.filterFavButton1, "Filter for favourites");
            this.filterFavButton1.UseVisualStyleBackColor = true;
            this.filterFavButton1.Click += new System.EventHandler(this.filterButton1_Click);
            // 
            // sortAZButton2
            // 
            this.sortAZButton2.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Sort_Ascending;
            this.sortAZButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sortAZButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sortAZButton2.Location = new System.Drawing.Point(169, 85);
            this.sortAZButton2.Name = "sortAZButton2";
            this.sortAZButton2.Size = new System.Drawing.Size(25, 25);
            this.sortAZButton2.TabIndex = 5;
            this.toolTip1.SetToolTip(this.sortAZButton2, "Sort in alphabetical order");
            this.sortAZButton2.UseVisualStyleBackColor = true;
            this.sortAZButton2.Click += new System.EventHandler(this.sortAZButton2_Click);
            // 
            // sortAZButton1
            // 
            this.sortAZButton1.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Sort_Ascending;
            this.sortAZButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sortAZButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sortAZButton1.Location = new System.Drawing.Point(11, 85);
            this.sortAZButton1.Name = "sortAZButton1";
            this.sortAZButton1.Size = new System.Drawing.Size(25, 25);
            this.sortAZButton1.TabIndex = 3;
            this.toolTip1.SetToolTip(this.sortAZButton1, "Sort in alphabetical order");
            this.sortAZButton1.UseVisualStyleBackColor = true;
            this.sortAZButton1.Click += new System.EventHandler(this.sortAZButton1_Click);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.startButton.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Shinmera_Banner_5_mini_size;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.startButton.ForeColor = System.Drawing.Color.Red;
            this.startButton.Location = new System.Drawing.Point(14, 40);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(307, 38);
            this.startButton.TabIndex = 2;
            this.toolTip1.SetToolTip(this.startButton, "Start thcrap with the selected settings");
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            this.startButton.MouseLeave += new System.EventHandler(this.startButton_MouseLeave);
            this.startButton.MouseHover += new System.EventHandler(this.startButton_MouseHover);
            // 
            // filterByType_button
            // 
            this.filterByType_button.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.GameAndCustom;
            this.filterByType_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.filterByType_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterByType_button.Location = new System.Drawing.Point(231, 85);
            this.filterByType_button.Name = "filterByType_button";
            this.filterByType_button.Size = new System.Drawing.Size(25, 25);
            this.filterByType_button.TabIndex = 7;
            this.toolTip1.SetToolTip(this.filterByType_button, "Filter by Type");
            this.filterByType_button.UseVisualStyleBackColor = true;
            this.filterByType_button.Click += new System.EventHandler(this.filterByType_button_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appTS,
            this.thcrapTS,
            this.gitHubTS,
            this.helpTS});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(334, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // appTS
            // 
            this.appTS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createShortcutTS,
            this.settingsTS,
            this.toolStripSeparator1,
            this.restartTS,
            this.exitTS});
            this.appTS.Name = "appTS";
            this.appTS.Size = new System.Drawing.Size(41, 20);
            this.appTS.Text = "App";
            // 
            // createShortcutTS
            // 
            this.createShortcutTS.Name = "createShortcutTS";
            this.createShortcutTS.Size = new System.Drawing.Size(216, 22);
            this.createShortcutTS.Text = "Create Shortcut to Desktop";
            this.createShortcutTS.Click += new System.EventHandler(this.createShortcutTS_Click);
            // 
            // settingsTS
            // 
            this.settingsTS.Name = "settingsTS";
            this.settingsTS.Size = new System.Drawing.Size(216, 22);
            this.settingsTS.Text = "Settings...";
            this.settingsTS.Click += new System.EventHandler(this.settingsTS_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(213, 6);
            // 
            // restartTS
            // 
            this.restartTS.Name = "restartTS";
            this.restartTS.ShortcutKeyDisplayString = "Ctrl+R";
            this.restartTS.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.restartTS.Size = new System.Drawing.Size(216, 22);
            this.restartTS.Text = "Restart";
            this.restartTS.Click += new System.EventHandler(this.restartTS_Click);
            // 
            // exitTS
            // 
            this.exitTS.Name = "exitTS";
            this.exitTS.ShortcutKeyDisplayString = "Alt+F4";
            this.exitTS.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitTS.Size = new System.Drawing.Size(216, 22);
            this.exitTS.Text = "Exit";
            this.exitTS.Click += new System.EventHandler(this.exitTS_Click);
            // 
            // thcrapTS
            // 
            this.thcrapTS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openConfigureTS,
            this.openMenuTS});
            this.thcrapTS.Name = "thcrapTS";
            this.thcrapTS.Size = new System.Drawing.Size(65, 20);
            this.thcrapTS.Text = "THCRAP";
            // 
            // openConfigureTS
            // 
            this.openConfigureTS.Name = "openConfigureTS";
            this.openConfigureTS.Size = new System.Drawing.Size(208, 22);
            this.openConfigureTS.Text = "Open THCRAP Configure";
            this.openConfigureTS.Click += new System.EventHandler(this.openConfigureTS_Click);
            // 
            // openMenuTS
            // 
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
            this.openFolderTS.Name = "openFolderTS";
            this.openFolderTS.Size = new System.Drawing.Size(309, 22);
            this.openFolderTS.Text = "Open Folder";
            this.openFolderTS.Click += new System.EventHandler(this.openFolderTS_Click);
            // 
            // openGamesListTS
            // 
            this.openGamesListTS.Name = "openGamesListTS";
            this.openGamesListTS.Size = new System.Drawing.Size(309, 22);
            this.openGamesListTS.Text = "Open Games List";
            this.openGamesListTS.Click += new System.EventHandler(this.openGamesListTS_Click);
            // 
            // openSelectedPatchConfigurationTS
            // 
            this.openSelectedPatchConfigurationTS.Name = "openSelectedPatchConfigurationTS";
            this.openSelectedPatchConfigurationTS.ShortcutKeyDisplayString = "Ctrl + O";
            this.openSelectedPatchConfigurationTS.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openSelectedPatchConfigurationTS.Size = new System.Drawing.Size(309, 22);
            this.openSelectedPatchConfigurationTS.Text = "Open Selected Patch Configuration";
            this.openSelectedPatchConfigurationTS.Click += new System.EventHandler(this.openSelectedPatchConfigurationTS_Click);
            // 
            // gitHubTS
            // 
            this.gitHubTS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gitHubPageTS,
            this.feedbackTS});
            this.gitHubTS.Name = "gitHubTS";
            this.gitHubTS.Size = new System.Drawing.Size(57, 20);
            this.gitHubTS.Text = "GitHub";
            // 
            // gitHubPageTS
            // 
            this.gitHubPageTS.Name = "gitHubPageTS";
            this.gitHubPageTS.Size = new System.Drawing.Size(141, 22);
            this.gitHubPageTS.Text = "GitHub Page";
            this.gitHubPageTS.Click += new System.EventHandler(this.gitHubPageTS_Click);
            // 
            // feedbackTS
            // 
            this.feedbackTS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bugReportTS,
            this.featureRequestTS,
            this.otherTS});
            this.feedbackTS.Name = "feedbackTS";
            this.feedbackTS.Size = new System.Drawing.Size(141, 22);
            this.feedbackTS.Text = "Feedback...";
            // 
            // bugReportTS
            // 
            this.bugReportTS.Name = "bugReportTS";
            this.bugReportTS.Size = new System.Drawing.Size(158, 22);
            this.bugReportTS.Text = "Bug Report";
            this.bugReportTS.Click += new System.EventHandler(this.bugReportTS_Click);
            // 
            // featureRequestTS
            // 
            this.featureRequestTS.Name = "featureRequestTS";
            this.featureRequestTS.Size = new System.Drawing.Size(158, 22);
            this.featureRequestTS.Text = "Feature Request";
            this.featureRequestTS.Click += new System.EventHandler(this.featureRequestTS_Click);
            // 
            // otherTS
            // 
            this.otherTS.Name = "otherTS";
            this.otherTS.Size = new System.Drawing.Size(158, 22);
            this.otherTS.Text = "Other";
            this.otherTS.Click += new System.EventHandler(this.otherTS_Click);
            // 
            // helpTS
            // 
            this.helpTS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keyboardShortcutsTS});
            this.helpTS.Name = "helpTS";
            this.helpTS.ShortcutKeyDisplayString = "F1";
            this.helpTS.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpTS.Size = new System.Drawing.Size(44, 20);
            this.helpTS.Text = "Help";
            this.helpTS.Click += new System.EventHandler(this.keyboardShortcutsTS_Click);
            // 
            // keyboardShortcutsTS
            // 
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 431);
            this.Controls.Add(this.filterByType_button);
            this.Controls.Add(this.filterFavButton2);
            this.Controls.Add(this.filterFavButton1);
            this.Controls.Add(this.sortAZButton2);
            this.Controls.Add(this.sortAZButton1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 300);
            this.Name = "MainForm";
            this.Text = "Universal THCRAP Launcher";
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
        private Button sortAZButton1;
        private Button sortAZButton2;
        private Button filterFavButton1;
        private Button filterFavButton2;
        private Button filterByType_button;
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
        private ToolStripMenuItem createShortcutTS;
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
    }
}

