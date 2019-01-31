using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Universal_THCRAP_Launcher
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.star_button2 = new System.Windows.Forms.Button();
            this.star_button1 = new System.Windows.Forms.Button();
            this.sort_az_button2 = new System.Windows.Forms.Button();
            this.sort_az_button1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.filterByType_button = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.appToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createShortcutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thcrapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTHCRAPConfigureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openGamesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSelectedPatchConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.requestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportBugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.requestAFeatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyboardShortcutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 1);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(153, 303);
            this.listBox1.TabIndex = 8;
            this.toolTip1.SetToolTip(this.listBox1, "Choose the run configuration (patch stack).");
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            this.listBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            // 
            // splitContainer1
            // 
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(11, 117);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listBox2);
            this.splitContainer1.Size = new System.Drawing.Size(312, 306);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 1;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(0, 1);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(152, 303);
            this.listBox2.TabIndex = 9;
            this.toolTip1.SetToolTip(this.listBox2, "Choose the game (executable).");
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            this.listBox2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(11, 435);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(138, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Close when game starts";
            this.toolTip1.SetToolTip(this.checkBox1, "If checked, the applicated will close itself when starting thcrap.");
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // star_button2
            // 
            this.star_button2.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Star;
            this.star_button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.star_button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.star_button2.Location = new System.Drawing.Point(200, 85);
            this.star_button2.Name = "star_button2";
            this.star_button2.Size = new System.Drawing.Size(25, 25);
            this.star_button2.TabIndex = 6;
            this.toolTip1.SetToolTip(this.star_button2, "Filter for favourites.");
            this.star_button2.UseVisualStyleBackColor = true;
            this.star_button2.Click += new System.EventHandler(this.star_button2_Click);
            // 
            // star_button1
            // 
            this.star_button1.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Star;
            this.star_button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.star_button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.star_button1.Location = new System.Drawing.Point(42, 85);
            this.star_button1.Name = "star_button1";
            this.star_button1.Size = new System.Drawing.Size(25, 25);
            this.star_button1.TabIndex = 4;
            this.toolTip1.SetToolTip(this.star_button1, "Filter for favourites.");
            this.star_button1.UseVisualStyleBackColor = true;
            this.star_button1.Click += new System.EventHandler(this.star_button1_Click);
            // 
            // sort_az_button2
            // 
            this.sort_az_button2.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Sort_Decending;
            this.sort_az_button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sort_az_button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sort_az_button2.Location = new System.Drawing.Point(169, 85);
            this.sort_az_button2.Name = "sort_az_button2";
            this.sort_az_button2.Size = new System.Drawing.Size(25, 25);
            this.sort_az_button2.TabIndex = 5;
            this.toolTip1.SetToolTip(this.sort_az_button2, "Sort in alphabetical order.");
            this.sort_az_button2.UseVisualStyleBackColor = true;
            this.sort_az_button2.Click += new System.EventHandler(this.sort_az_button2_Click);
            // 
            // sort_az_button1
            // 
            this.sort_az_button1.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Sort_Decending;
            this.sort_az_button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sort_az_button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sort_az_button1.Location = new System.Drawing.Point(11, 85);
            this.sort_az_button1.Name = "sort_az_button1";
            this.sort_az_button1.Size = new System.Drawing.Size(25, 25);
            this.sort_az_button1.TabIndex = 3;
            this.toolTip1.SetToolTip(this.sort_az_button1, "Sort in alphabetical order.");
            this.sort_az_button1.UseVisualStyleBackColor = true;
            this.sort_az_button1.Click += new System.EventHandler(this.sort_az_button1_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.BackgroundImage = global::Universal_THCRAP_Launcher.Properties.Resources.Shinmera_Banner_5_mini_size;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(14, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(307, 38);
            this.button1.TabIndex = 2;
            this.toolTip1.SetToolTip(this.button1, "Start thcrap with the selected settings.");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            this.button1.MouseHover += new System.EventHandler(this.button1_MouseHover);
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
            this.appToolStripMenuItem,
            this.thcrapToolStripMenuItem,
            this.gitHubToolStripMenuItem,
            this.keyboardShortcutsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(334, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // appToolStripMenuItem
            // 
            this.appToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createShortcutToolStripMenuItem,
            this.toolStripSeparator1,
            this.restartToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.appToolStripMenuItem.Name = "appToolStripMenuItem";
            this.appToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.appToolStripMenuItem.Text = "App";
            // 
            // createShortcutToolStripMenuItem
            // 
            this.createShortcutToolStripMenuItem.Name = "createShortcutToolStripMenuItem";
            this.createShortcutToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.createShortcutToolStripMenuItem.Text = "Create Shortcut to Desktop";
            this.createShortcutToolStripMenuItem.Click += new System.EventHandler(this.createShortcutToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(213, 6);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+R";
            this.restartToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "Alt+F4";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // thcrapToolStripMenuItem
            // 
            this.thcrapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTHCRAPConfigureToolStripMenuItem,
            this.openMenuItem});
            this.thcrapToolStripMenuItem.Name = "thcrapToolStripMenuItem";
            this.thcrapToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.thcrapToolStripMenuItem.Text = "THCRAP";
            // 
            // openTHCRAPConfigureToolStripMenuItem
            // 
            this.openTHCRAPConfigureToolStripMenuItem.Name = "openTHCRAPConfigureToolStripMenuItem";
            this.openTHCRAPConfigureToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.openTHCRAPConfigureToolStripMenuItem.Text = "Open THCRAP Configure";
            this.openTHCRAPConfigureToolStripMenuItem.Click += new System.EventHandler(this.openTHCRAPConfigureToolStripMenuItem_Click);
            // 
            // openMenuItem
            // 
            this.openMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderToolStripMenuItem,
            this.openGamesListToolStripMenuItem,
            this.openSelectedPatchConfigurationToolStripMenuItem});
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.Size = new System.Drawing.Size(208, 22);
            this.openMenuItem.Text = "Open...";
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(309, 22);
            this.openFolderToolStripMenuItem.Text = "Open Folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // openGamesListToolStripMenuItem
            // 
            this.openGamesListToolStripMenuItem.Name = "openGamesListToolStripMenuItem";
            this.openGamesListToolStripMenuItem.Size = new System.Drawing.Size(309, 22);
            this.openGamesListToolStripMenuItem.Text = "Open Games List";
            this.openGamesListToolStripMenuItem.Click += new System.EventHandler(this.openGamesListToolStripMenuItem_Click);
            // 
            // openSelectedPatchConfigurationToolStripMenuItem
            // 
            this.openSelectedPatchConfigurationToolStripMenuItem.Name = "openSelectedPatchConfigurationToolStripMenuItem";
            this.openSelectedPatchConfigurationToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + O";
            this.openSelectedPatchConfigurationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openSelectedPatchConfigurationToolStripMenuItem.Size = new System.Drawing.Size(309, 22);
            this.openSelectedPatchConfigurationToolStripMenuItem.Text = "Open Selected Patch Configuration";
            this.openSelectedPatchConfigurationToolStripMenuItem.Click += new System.EventHandler(this.openSelectedPatchConfigurationToolStripMenuItem_Click);
            // 
            // gitHubToolStripMenuItem
            // 
            this.gitHubToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.requestToolStripMenuItem,
            this.releasesToolStripMenuItem});
            this.gitHubToolStripMenuItem.Name = "gitHubToolStripMenuItem";
            this.gitHubToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.gitHubToolStripMenuItem.Text = "GitHub";
            this.gitHubToolStripMenuItem.Click += new System.EventHandler(this.gitHubToolStripMenuItem_Click);
            // 
            // requestToolStripMenuItem
            // 
            this.requestToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportBugToolStripMenuItem,
            this.requestAFeatureToolStripMenuItem});
            this.requestToolStripMenuItem.Name = "requestToolStripMenuItem";
            this.requestToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.requestToolStripMenuItem.Text = "Request";
            this.requestToolStripMenuItem.Click += new System.EventHandler(this.requestToolStripMenuItem_Click);
            // 
            // reportBugToolStripMenuItem
            // 
            this.reportBugToolStripMenuItem.Name = "reportBugToolStripMenuItem";
            this.reportBugToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.reportBugToolStripMenuItem.Text = "Report Bug";
            this.reportBugToolStripMenuItem.Click += new System.EventHandler(this.reportBugToolStripMenuItem_Click);
            // 
            // requestAFeatureToolStripMenuItem
            // 
            this.requestAFeatureToolStripMenuItem.Name = "requestAFeatureToolStripMenuItem";
            this.requestAFeatureToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.requestAFeatureToolStripMenuItem.Text = "Request a feature";
            this.requestAFeatureToolStripMenuItem.Click += new System.EventHandler(this.requestAFeatureToolStripMenuItem_Click);
            // 
            // releasesToolStripMenuItem
            // 
            this.releasesToolStripMenuItem.Name = "releasesToolStripMenuItem";
            this.releasesToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.releasesToolStripMenuItem.Text = "Releases";
            this.releasesToolStripMenuItem.Click += new System.EventHandler(this.releasesToolStripMenuItem_Click);
            // 
            // keyboardShortcutsToolStripMenuItem
            // 
            this.keyboardShortcutsToolStripMenuItem.Name = "keyboardShortcutsToolStripMenuItem";
            this.keyboardShortcutsToolStripMenuItem.ShortcutKeyDisplayString = "F1";
            this.keyboardShortcutsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.keyboardShortcutsToolStripMenuItem.Size = new System.Drawing.Size(122, 20);
            this.keyboardShortcutsToolStripMenuItem.Text = "Keyboard Shortcuts";
            this.keyboardShortcutsToolStripMenuItem.Click += new System.EventHandler(this.keyboardShortcutsToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 461);
            this.Controls.Add(this.filterByType_button);
            this.Controls.Add(this.star_button2);
            this.Controls.Add(this.star_button1);
            this.Controls.Add(this.sort_az_button2);
            this.Controls.Add(this.sort_az_button1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 300);
            this.Name = "Form1";
            this.Text = "Universal THCRAP Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
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

        private ListBox listBox1;
        private SplitContainer splitContainer1;
        private ListBox listBox2;
        private CheckBox checkBox1;
        private Button button1;
        private ToolTip toolTip1;
        private Button sort_az_button1;
        private Button sort_az_button2;
        private Button star_button1;
        private Button star_button2;
        private Button filterByType_button;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem appToolStripMenuItem;
        private ToolStripMenuItem restartToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem keyboardShortcutsToolStripMenuItem;
        private ToolStripMenuItem gitHubToolStripMenuItem;
        private ToolStripMenuItem requestToolStripMenuItem;
        private ToolStripMenuItem releasesToolStripMenuItem;
        private ToolStripMenuItem reportBugToolStripMenuItem;
        private ToolStripMenuItem requestAFeatureToolStripMenuItem;
        private ToolStripMenuItem thcrapToolStripMenuItem;
        private ToolStripMenuItem openTHCRAPConfigureToolStripMenuItem;
        private ToolStripMenuItem openGamesListToolStripMenuItem;
        private ToolStripMenuItem openFolderToolStripMenuItem;
        private ToolStripMenuItem createShortcutToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem openMenuItem;
        private ToolStripMenuItem openSelectedPatchConfigurationToolStripMenuItem;
    }
}

