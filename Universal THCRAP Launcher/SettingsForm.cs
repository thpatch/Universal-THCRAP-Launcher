using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

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
        private readonly Dictionary<string, string> _downNameToPatch = new Dictionary<string, string>();

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            UpdateLang();

            cB_hidePatchExtension.Checked = MainForm.Configuration1.HidePatchExtension;
            closeOnExitCheckBox.Checked = MainForm.Configuration1.ExitAfterStartup;

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

            UpdateLang();
            UpdateCredits();
        }

        private void UpdateLang()
        {
            Text = I18N.LangResource.settingsForm.settings;
            languageLabel.Text = I18N.LangResource.settingsForm.language + ':';
            closeOnExitCheckBox.Text = I18N.LangResource.settingsForm.closeOnExit;
            btn_dwnlAllLangs.Text = I18N.LangResource.settingsForm.downloadAll;
            cB_hidePatchExtension.Text = I18N.LangResource.settingsForm.hidePatchExtension;
        }

        private void UpdateCredits()
        {
            string credits = "";
            foreach (var author in I18N.LangResource.metadata.authors)
            {
                credits += author + ", ";
            }
            credits = credits.TrimEnd(' ', ',');
            int place = credits.LastIndexOf(',');
            if (place != -1)
            {
                credits = credits.Remove(place, 1).Insert(place, " " + I18N.LangResource.settingsForm.and);
            }
            langCreditsLabel.Text = string.Format((string)I18N.LangResource.settingsForm.langCredits, credits);
        }

        private void closeOnExitCheckBox_CheckedChanged(object sender, EventArgs e) =>
            MainForm.Configuration1.ExitAfterStartup = closeOnExitCheckBox.Checked;

        private void languageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _langNameToFile.TryGetValue(languageComboBox.SelectedItem.ToString(), out var file);
            I18N.GetLangResource(file);
            UpdateLang();
            UpdateCredits();
        }

        static string ReadTextFromUrl(string url)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Assume UTF8, but detect BOM - could also honor response charset I suppose
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.UserAgent, "UTL");
            var stream = client.OpenRead(url);
            using (var textReader = new StreamReader(stream, System.Text.Encoding.UTF8, true))
            {
                return textReader.ReadToEnd();
            }
        }

        private void Btn_dwnlAllLangs_Click(object sender, EventArgs e)
        {
            btn_dwnlAllLangs.Enabled = false;
            btn_dwnlAllLangs.Text = I18N.LangResource.settingsForm.downloading;
            string gh = ReadTextFromUrl("https://api.github.com/repos/Tudi20/Universal-THCRAP-Launcher/contents/langs?ref=master");
            dynamic obj_gh = JsonConvert.DeserializeObject(gh);
            foreach (var item in obj_gh)
            {
                string langtxt = ReadTextFromUrl(item.download_url.ToString());
                File.WriteAllText(I18N.I18NDir + item.name, langtxt);
            }
            btn_dwnlAllLangs.Text = I18N.LangResource.settingsForm.downloadAll;
            btn_dwnlAllLangs.Enabled = true;
        }

        private void CB_hidePatchExtension_CheckedChanged(object sender, EventArgs e) => MainForm.Configuration1.HidePatchExtension = cB_hidePatchExtension.Checked;
    }
}
