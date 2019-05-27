using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.Threading;

namespace Universal_THCRAP_Launcher
{
    public partial class SettingsForm : Form
    {
        MainForm mf;
        public SettingsForm(MainForm mainForm)
        {
            InitializeComponent();
            mf = mainForm;
        }

        private readonly Dictionary<string, string> _langNameToFile = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _langFileToName = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _downNameToPatch = new Dictionary<string, string>();

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            cB_hidePatchExtension.Checked = MainForm.Configuration1.HidePatchExtension;
            closeOnExitCheckBox.Checked = MainForm.Configuration1.ExitAfterStartup;
   
            UpdateLang();
            UpdateCredits();
            LoadLangs();
        }

        private void LoadLangs()
        {
            #region LoadLangs
            languageComboBox.Items.Clear();
            _langFileToName.Clear();
            _langNameToFile.Clear();
            Trace.WriteLine("Loading languages...");
            foreach (var file in Directory.GetFiles(I18N.I18NDir))
            {
                string raw = File.ReadAllText(file);
                //Trace.WriteLine($"Language File: {file}. Here's the raw:\n{raw}");
                dynamic langFile = JsonConvert.DeserializeObject(raw);
                Trace.WriteLine($"\tLoading Language:\n\tFile: {file}\n\tEnglish name: {langFile.metadata.english}");
                if (!_langNameToFile.ContainsKey($"{langFile.metadata.native} ({langFile.metadata.english})"))
                _langNameToFile.Add($"{langFile.metadata.native} ({langFile.metadata.english})", file);
                if (!_langFileToName.ContainsKey(file))
                _langFileToName.Add(file, $"{langFile.metadata.native} ({langFile.metadata.english})");
                if (!languageComboBox.Items.Contains($"{langFile.metadata.native} ({langFile.metadata.english})"))
                languageComboBox.Items.Add($"{langFile.metadata.native} ({langFile.metadata.english})");
            }
            #endregion
            Trace.WriteLine("Language loading ended.");
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
            Text = I18N.LangResource.settingsForm.settings.ToString();
            languageLabel.Text = I18N.LangResource.settingsForm.language.ToString() + ':';
            closeOnExitCheckBox.Text = I18N.LangResource.settingsForm.closeOnExit.ToString();
            btn_dwnlAllLangs.Text = I18N.LangResource.settingsForm.downloadAll.ToString();
            cB_hidePatchExtension.Text = I18N.LangResource.settingsForm.hidePatchExtension.ToString();
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
                credits = credits.Remove(place, 1).Insert(place, " " + I18N.LangResource.settingsForm.and.ToString());
            }
            langCreditsLabel.Text = string.Format(I18N.LangResource.settingsForm.langCredits.ToString(), credits);
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
            if (I18N.LangResource.settingsForm.downloading != null)
                btn_dwnlAllLangs.Text = I18N.LangResource.settingsForm.downloading.ToString();
            else btn_dwnlAllLangs.Text = "Downloading...";
            btn_dwnlAllLangs.Enabled = false;
            try
            {
                string gh = ReadTextFromUrl("https://api.github.com/repos/Tudi20/Universal-THCRAP-Launcher/contents/langs?ref=master");
                dynamic obj_gh = JsonConvert.DeserializeObject(gh);
                foreach (var item in obj_gh)
                {
                    string langtxt = ReadTextFromUrl(item.download_url.ToString());
                    File.WriteAllText(I18N.I18NDir + item.name, langtxt);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"[{DateTime.Now.ToShortTimeString()}] Couldn't connect to GitHub for pulling down languages.\nReason: {ex.ToString()}");
                MessageBox.Show(I18N.LangResource.error.downloadError.ToString(),I18N.LangResource.errors.error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btn_dwnlAllLangs.Text = I18N.LangResource.settingsForm.downloadAll.ToString();
            btn_dwnlAllLangs.Enabled = true;

            LoadLangs();
        }

        private void CB_hidePatchExtension_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.Configuration1.HidePatchExtension = cB_hidePatchExtension.Checked;
            mf.PopulatePatchList();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mf.UpdateConfigFile();
        }
    }
}
