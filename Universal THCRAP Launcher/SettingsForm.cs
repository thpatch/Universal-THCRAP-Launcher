using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Universal_THCRAP_Launcher
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private readonly Dictionary<string, string> _langNameToFile = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _langFileToName = new Dictionary<string, string>();

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            UpdateLang();

            #region Load languages
            
            if (I18N.LangNumber() > 0) languageComboBox.Items.Clear();

            foreach (var file in Directory.GetFiles(I18N.I18NDir))
            {
                string raw = File.ReadAllText(file);
                dynamic langFile = JsonConvert.DeserializeObject(raw);
                _langNameToFile.Add($"{langFile.metadata.native} ({langFile.metadata.english})",file);
                _langFileToName.Add(file, $"{langFile.metadata.native} ({langFile.metadata.english})");
                languageComboBox.Items.Add($"{langFile.metadata.native} ({langFile.metadata.english})");
            }
            #endregion
            #region Select appropiate lang

            if (Configuration.Lang == null)
                languageComboBox.SelectedIndex = 0;

            else languageComboBox.SelectedItem = _langFileToName[I18N.I18NDir + Configuration.Lang];
            Debug.WriteLine("Configuration.Lang = " + Configuration.Lang);
            Debug.WriteLine("Language Selected: " + languageComboBox.SelectedItem + " | " + languageComboBox.SelectedIndex);
            #endregion
        }

        private void UpdateLang()
        {
            Text = I18N.LangResource.settingsForm.settings;
            languageLabel.Text = I18N.LangResource.settingsForm.language + ':';
            closeOnExitCheckBox.Text = I18N.LangResource.settingsForm.closeOnExit;
            string credits = "";
            foreach (var author in I18N.LangResource.metadata.authors)
            {
                credits += author + ", ";
            }
            credits = credits.TrimEnd(' ', ',');
            langCreditsLabel.Text = String.Format(I18N.LangResource.settingsForm.langCredits, credits);
        }

        private void closeOnExitCheckBox_CheckedChanged(object sender, EventArgs e) =>
            MainForm.Configuration1.ExitAfterStartup = closeOnExitCheckBox.Checked;

        private void languageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _langNameToFile.TryGetValue(languageComboBox.SelectedItem.ToString(), out var file);
            I18N.GetLangResource(file);
        }


    }
}
