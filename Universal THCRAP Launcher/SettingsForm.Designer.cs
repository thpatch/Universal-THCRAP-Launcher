namespace Universal_THCRAP_Launcher
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.SuspendLayout();
            // 
            // closeOnExitCheckBox
            // 
            this.closeOnExitCheckBox.AutoSize = true;
            this.closeOnExitCheckBox.Checked = true;
            this.closeOnExitCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.closeOnExitCheckBox.Location = new System.Drawing.Point(12, 153);
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
            this.languageComboBox.Location = new System.Drawing.Point(77, 12);
            this.languageComboBox.MaxDropDownItems = 16;
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.Size = new System.Drawing.Size(208, 21);
            this.languageComboBox.TabIndex = 12;
            this.languageComboBox.SelectedIndexChanged += new System.EventHandler(this.languageComboBox_SelectedIndexChanged);
            // 
            // languageLabel
            // 
            this.languageLabel.AutoSize = true;
            this.languageLabel.Location = new System.Drawing.Point(12, 15);
            this.languageLabel.Name = "languageLabel";
            this.languageLabel.Size = new System.Drawing.Size(58, 13);
            this.languageLabel.TabIndex = 13;
            this.languageLabel.Text = "Language:";
            // 
            // langCreditsLabel
            // 
            this.langCreditsLabel.AutoSize = true;
            this.langCreditsLabel.Location = new System.Drawing.Point(12, 36);
            this.langCreditsLabel.MaximumSize = new System.Drawing.Size(276, 0);
            this.langCreditsLabel.Name = "langCreditsLabel";
            this.langCreditsLabel.Size = new System.Drawing.Size(110, 13);
            this.langCreditsLabel.TabIndex = 14;
            this.langCreditsLabel.Text = "Translated by: Tudi20";
            // 
            // btn_dwnlAllLangs
            // 
            this.btn_dwnlAllLangs.Location = new System.Drawing.Point(15, 124);
            this.btn_dwnlAllLangs.Name = "btn_dwnlAllLangs";
            this.btn_dwnlAllLangs.Size = new System.Drawing.Size(270, 23);
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
            this.cB_hidePatchExtension.Location = new System.Drawing.Point(12, 175);
            this.cB_hidePatchExtension.Name = "cB_hidePatchExtension";
            this.cB_hidePatchExtension.Size = new System.Drawing.Size(159, 17);
            this.cB_hidePatchExtension.TabIndex = 16;
            this.cB_hidePatchExtension.Text = "Hide extension on patch list.";
            this.cB_hidePatchExtension.UseVisualStyleBackColor = true;
            this.cB_hidePatchExtension.CheckedChanged += new System.EventHandler(this.CB_hidePatchExtension_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 204);
            this.Controls.Add(this.cB_hidePatchExtension);
            this.Controls.Add(this.btn_dwnlAllLangs);
            this.Controls.Add(this.langCreditsLabel);
            this.Controls.Add(this.languageLabel);
            this.Controls.Add(this.languageComboBox);
            this.Controls.Add(this.closeOnExitCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox closeOnExitCheckBox;
        private System.Windows.Forms.ComboBox languageComboBox;
        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.Label langCreditsLabel;
        private System.Windows.Forms.Button btn_dwnlAllLangs;
        private System.Windows.Forms.CheckBox cB_hidePatchExtension;
    }
}