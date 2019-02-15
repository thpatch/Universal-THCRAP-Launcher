using System;
using System.Collections.Generic;
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

        private readonly Dictionary<string, string> _languages = new Dictionary<string, string>();

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            UpdateLang();

            #region Load languages

            foreach (var file in Directory.GetFiles(I18N.i18nDir))
            {
                string raw = File.ReadAllText(file);
                dynamic langFile = JsonConvert.DeserializeObject(raw);
                _languages.Add($"{langFile.metadata.native} ({langFile.metadata.english})",file);
                languageComboBox.Items.Add($"{langFile.metadata.native} ({langFile.metadata.english})");
            }
            #endregion
            #region Select appropiate lang
            if (Configuration.Lang == null)
                if (languageComboBox.Contains(new Control("English (English)")))
                    languageComboBox.SelectedItem = "English (English)";
                else
                    languageComboBox.SelectedIndex = 0;
            else languageComboBox.SelectedItem = Configuration.Lang;
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
            string file;
            _languages.TryGetValue(languageComboBox.SelectedItem.ToString(), out file);
            I18N.GetLangResource(file);
        }


    }
}
