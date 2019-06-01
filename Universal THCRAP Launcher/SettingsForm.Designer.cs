using System.ComponentModel;
using System.Windows.Forms;

namespace Universal_THCRAP_Launcher
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.closeOnExitCheckBox = new System.Windows.Forms.CheckBox();
            this.languageComboBox = new System.Windows.Forms.ComboBox();
            this.languageLabel = new System.Windows.Forms.Label();
            this.langCreditsLabel = new System.Windows.Forms.Label();
            this.btn_dwnlAllLangs = new System.Windows.Forms.Button();
            this.cB_hidePatchExtension = new System.Windows.Forms.CheckBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_General = new System.Windows.Forms.TabPage();
            this.tabPage_Language = new System.Windows.Forms.TabPage();
            this.cB_ShowVanilla = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.tabPage_General.SuspendLayout();
            this.tabPage_Language.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeOnExitCheckBox
            // 
            this.closeOnExitCheckBox.AutoSize = true;
            this.closeOnExitCheckBox.Checked = true;
            this.closeOnExitCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.closeOnExitCheckBox.Location = new System.Drawing.Point(8, 6);
            this.closeOnExitCheckBox.Name = "closeOnExitCheckBox";
            this.closeOnExitCheckBox.Size = new System.Drawing.Size(138, 17);
            this.closeOnExitCheckBox.TabIndex = 11;
            this.closeOnExitCheckBox.Text = "Close when game starts";
            this.closeOnExitCheckBox.UseVisualStyleBackColor = true;
            this.closeOnExitCheckBox.CheckedChanged += new System.EventHandler(this.closeOnExitCheckBox_CheckedChanged);
            // 
            // languageComboBox
            // 
            this.languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageComboBox.FormattingEnabled = true;
            this.languageComboBox.Items.AddRange(new object[] {
            "English (English)"});
            this.languageComboBox.Location = new System.Drawing.Point(62, 35);
            this.languageComboBox.MaxDropDownItems = 16;
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.Size = new System.Drawing.Size(237, 21);
            this.languageComboBox.TabIndex = 12;
            this.languageComboBox.SelectedIndexChanged += new System.EventHandler(this.languageComboBox_SelectedIndexChanged);
            // 
            // languageLabel
            // 
            this.languageLabel.AutoSize = true;
            this.languageLabel.Location = new System.Drawing.Point(5, 38);
            this.languageLabel.Name = "languageLabel";
            this.languageLabel.Size = new System.Drawing.Size(58, 13);
            this.languageLabel.TabIndex = 13;
            this.languageLabel.Text = "Language:";
            // 
            // langCreditsLabel
            // 
            this.langCreditsLabel.AutoSize = true;
            this.langCreditsLabel.Location = new System.Drawing.Point(5, 59);
            this.langCreditsLabel.MaximumSize = new System.Drawing.Size(276, 0);
            this.langCreditsLabel.Name = "langCreditsLabel";
            this.langCreditsLabel.Size = new System.Drawing.Size(110, 13);
            this.langCreditsLabel.TabIndex = 14;
            this.langCreditsLabel.Text = "Translated by: Tudi20";
            // 
            // btn_dwnlAllLangs
            // 
            this.btn_dwnlAllLangs.Location = new System.Drawing.Point(6, 6);
            this.btn_dwnlAllLangs.Name = "btn_dwnlAllLangs";
            this.btn_dwnlAllLangs.Size = new System.Drawing.Size(293, 23);
            this.btn_dwnlAllLangs.TabIndex = 15;
            this.btn_dwnlAllLangs.Text = "Download All Languages";
            this.btn_dwnlAllLangs.UseVisualStyleBackColor = true;
            this.btn_dwnlAllLangs.Click += new System.EventHandler(this.Btn_dwnlAllLangs_Click);
            // 
            // cB_hidePatchExtension
            // 
            this.cB_hidePatchExtension.AutoSize = true;
            this.cB_hidePatchExtension.Checked = true;
            this.cB_hidePatchExtension.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_hidePatchExtension.Location = new System.Drawing.Point(8, 28);
            this.cB_hidePatchExtension.Name = "cB_hidePatchExtension";
            this.cB_hidePatchExtension.Size = new System.Drawing.Size(159, 17);
            this.cB_hidePatchExtension.TabIndex = 16;
            this.cB_hidePatchExtension.Text = "Hide extension on patch list.";
            this.cB_hidePatchExtension.UseVisualStyleBackColor = true;
            this.cB_hidePatchExtension.CheckedChanged += new System.EventHandler(this.CB_hidePatchExtension_CheckedChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_General);
            this.tabControl.Controls.Add(this.tabPage_Language);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(313, 191);
            this.tabControl.TabIndex = 17;
            // 
            // tabPage_General
            // 
            this.tabPage_General.Controls.Add(this.cB_ShowVanilla);
            this.tabPage_General.Controls.Add(this.closeOnExitCheckBox);
            this.tabPage_General.Controls.Add(this.cB_hidePatchExtension);
            this.tabPage_General.Location = new System.Drawing.Point(4, 22);
            this.tabPage_General.Name = "tabPage_General";
            this.tabPage_General.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_General.Size = new System.Drawing.Size(305, 165);
            this.tabPage_General.TabIndex = 1;
            this.tabPage_General.Text = "General";
            this.tabPage_General.UseVisualStyleBackColor = true;
            // 
            // tabPage_Language
            // 
            this.tabPage_Language.Controls.Add(this.btn_dwnlAllLangs);
            this.tabPage_Language.Controls.Add(this.languageComboBox);
            this.tabPage_Language.Controls.Add(this.languageLabel);
            this.tabPage_Language.Controls.Add(this.langCreditsLabel);
            this.tabPage_Language.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Language.Name = "tabPage_Language";
            this.tabPage_Language.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Language.Size = new System.Drawing.Size(305, 165);
            this.tabPage_Language.TabIndex = 0;
            this.tabPage_Language.Text = "Language";
            this.tabPage_Language.UseVisualStyleBackColor = true;
            // 
            // cB_ShowVanilla
            // 
            this.cB_ShowVanilla.AutoSize = true;
            this.cB_ShowVanilla.Checked = true;
            this.cB_ShowVanilla.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_ShowVanilla.Location = new System.Drawing.Point(8, 51);
            this.cB_ShowVanilla.Name = "cB_ShowVanilla";
            this.cB_ShowVanilla.Size = new System.Drawing.Size(287, 17);
            this.cB_ShowVanilla.TabIndex = 17;
            this.cB_ShowVanilla.Text = "Show Vanilla (THCRAP-less) launch option in patch list.";
            this.cB_ShowVanilla.UseVisualStyleBackColor = true;
            this.cB_ShowVanilla.CheckedChanged += new System.EventHandler(this.CB_ShowVanilla_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 190);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage_General.ResumeLayout(false);
            this.tabPage_General.PerformLayout();
            this.tabPage_Language.ResumeLayout(false);
            this.tabPage_Language.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CheckBox closeOnExitCheckBox;
        private ComboBox languageComboBox;
        private Label languageLabel;
        private Label langCreditsLabel;
        private Button btn_dwnlAllLangs;
        private CheckBox cB_hidePatchExtension;
        private TabControl tabControl;
        private TabPage tabPage_Language;
        private TabPage tabPage_General;
        private CheckBox cB_ShowVanilla;
    }
}