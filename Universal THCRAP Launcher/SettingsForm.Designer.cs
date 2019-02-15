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
            this.closeOnExitCheckBox = new System.Windows.Forms.CheckBox();
            this.languageComboBox = new System.Windows.Forms.ComboBox();
            this.languageLabel = new System.Windows.Forms.Label();
            this.langCreditsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // closeOnExitCheckBox
            // 
            this.closeOnExitCheckBox.AutoSize = true;
            this.closeOnExitCheckBox.Checked = true;
            this.closeOnExitCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.closeOnExitCheckBox.Location = new System.Drawing.Point(12, 125);
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
            this.languageComboBox.Enabled = false;
            this.languageComboBox.FormattingEnabled = true;
            this.languageComboBox.Items.AddRange(new object[] {
            "English (English)"});
            this.languageComboBox.Location = new System.Drawing.Point(77, 12);
            this.languageComboBox.MaxDropDownItems = 16;
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.Size = new System.Drawing.Size(121, 21);
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
            this.langCreditsLabel.Location = new System.Drawing.Point(11, 41);
            this.langCreditsLabel.Name = "langCreditsLabel";
            this.langCreditsLabel.Size = new System.Drawing.Size(110, 13);
            this.langCreditsLabel.TabIndex = 14;
            this.langCreditsLabel.Text = "Translated by: Tudi20";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 154);
            this.Controls.Add(this.langCreditsLabel);
            this.Controls.Add(this.languageLabel);
            this.Controls.Add(this.languageComboBox);
            this.Controls.Add(this.closeOnExitCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox closeOnExitCheckBox;
        private System.Windows.Forms.ComboBox languageComboBox;
        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.Label langCreditsLabel;
    }
}