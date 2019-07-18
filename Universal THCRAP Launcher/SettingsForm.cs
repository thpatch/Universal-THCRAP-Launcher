using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
// ReSharper disable IdentifierTypo

namespace Universal_THCRAP_Launcher
{
    public partial class SettingsForm : Form
    {
        private readonly MainForm _mf;
        public SettingsForm(MainForm mainForm)
        {
            InitializeComponent();
            _mf = mainForm;
        }

        #region Globals
        private readonly Dictionary<string, string> _langNameToFile = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _langFileToName = new Dictionary<string, string>();
        //private readonly Dictionary<string, string> _downNameToPatch = new Dictionary<string, string>();
        #endregion

        #region Form Events
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            UpdateLang();
            cB_hidePatchExtension.Checked = MainForm.Configuration1.HidePatchExtension;
            closeOnExitCheckBox.Checked = MainForm.Configuration1.ExitAfterStartup;
            cB_ShowVanilla.Checked = MainForm.Configuration1.ShowVanilla;
            cB_OnlyAllowOneExe.Checked = MainForm.Configuration1.OnlyAllowOneExecutable;
            comboBox_gamesNamingType.SelectedIndex = (int)MainForm.Configuration1.NamingForGames;
            cB_onlyOneUTL.Checked = MainForm.Configuration1.OnlyAllowOneUtl;
            UpdateCredits();
            LoadLanguages();
        }
        #endregion

        #region GUI events
        private void closeOnExitCheckBox_CheckedChanged(object sender, EventArgs e) => MainForm.Configuration1.ExitAfterStartup = closeOnExitCheckBox.Checked;
        private void languageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _langNameToFile.TryGetValue(languageComboBox.SelectedItem.ToString(), out var file);
            I18N.UpdateLangResource(file);
            UpdateLang();
            UpdateCredits();
            _mf.PopulateGames();
        }
        private void Btn_dwnlAllLangs_Click(object sender, EventArgs e)
        {
            btn_dwnlAllLangs.Text = I18N.LangResource.settingsForm.downloading != null ? (string)I18N.LangResource.settingsForm.downloading.ToString() : "Downloading...";
            btn_dwnlAllLangs.Enabled = false;
            try
            {
                string gh = ReadTextFromUrl("https://api.github.com/repos/Tudi20/Universal-THCRAP-Launcher/contents/langs?ref=master");
                dynamic objGh = JsonConvert.DeserializeObject(gh);
                foreach (dynamic item in objGh)
                {
                    string langtxt = ReadTextFromUrl(item.download_url.ToString());
                    File.WriteAllText(I18N.I18NDir + item.name, langtxt);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"[{DateTime.Now.ToShortTimeString()}] Couldn't connect to GitHub for pulling down languages.\nReason: {ex}");
                MessageBox.Show(I18N.LangResource.error.downloadError?.ToString(), I18N.LangResource.errors.error?.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btn_dwnlAllLangs.Text = I18N.LangResource.settingsForm.downloadAll?.ToString();
            btn_dwnlAllLangs.Enabled = true;

            LoadLanguages();
        }
        private void CB_hidePatchExtension_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.Configuration1.HidePatchExtension = cB_hidePatchExtension.Checked;
            _mf.PopulatePatchList();
        }
        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e) => _mf.UpdateConfigFile();
        private void CB_ShowVanilla_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.Configuration1.ShowVanilla = cB_ShowVanilla.Checked;
            _mf.PopulatePatchList();
        }
        private void CB_OnlyAllowOneExe_CheckedChanged(object sender, EventArgs e) =>
            MainForm.Configuration1.OnlyAllowOneExecutable = cB_OnlyAllowOneExe.Checked;

        private void ComboBox_gamesNamingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainForm.Configuration1.NamingForGames = (GameNameType)comboBox_gamesNamingType.SelectedIndex;
            _mf.PopulateGames();
        }

        private void CB_onlyOneUTL_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.Configuration1.OnlyAllowOneUtl = cB_onlyOneUTL.Checked;
            if (!cB_onlyOneUTL.Checked) return;
            Process[] processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            foreach (Process t in processes) if (t.Id != Process.GetCurrentProcess().Id) t.Kill();
        }
        #endregion

        #region GUI-releated Methods
        private void LoadLanguages()
        {
            #region LoadLanguages
            languageComboBox.Items.Clear();
            _langFileToName.Clear();
            _langNameToFile.Clear();
            Trace.WriteLine($"[{DateTime.Now.ToShortTimeString()}] Loading languages...");
            foreach (var file in Directory.GetFiles(I18N.I18NDir))
            {
                string raw = File.ReadAllText(file);
                //Trace.WriteLine($"Language File: {file}. Here's the raw:\n{raw}");
                try
                {
                    dynamic langFile = JsonConvert.DeserializeObject(raw);
                    Trace.WriteLine($"\tLoading Language:\n\tFile: {file}\n\tEnglish name: {langFile.metadata.english}");
                    if (!_langNameToFile.ContainsKey($"{langFile.metadata.native} ({langFile.metadata.english})"))
                        _langNameToFile.Add($"{langFile.metadata.native} ({langFile.metadata.english})", file);
                    if (!_langFileToName.ContainsKey(file))
                        _langFileToName.Add(file, $"{langFile.metadata.native} ({langFile.metadata.english})");
                    if (!languageComboBox.Items.Contains($"{langFile.metadata.native} ({langFile.metadata.english})"))
                        languageComboBox.Items.Add($"{langFile.metadata.native} ({langFile.metadata.english})");
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"[{DateTime.Now.ToShortTimeString()}] Exception while parsing language file {file}\nException: {ex}");
                    MessageBox.Show(I18N.LangResource.errors.oops?.ToString() + Environment.CurrentDirectory, I18N.LangResource.errors.error?.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
            Trace.WriteLine($"[{DateTime.Now.ToShortTimeString()}] Language loading ended.");
            #region Select appropiate lang
            if (Configuration.Lang == null)
                languageComboBox.SelectedIndex = 0;
            else languageComboBox.SelectedItem = _langFileToName[I18N.I18NDir + Configuration.Lang];
            Debug.WriteLine($"[{DateTime.Now.ToShortTimeString()}] Configuration.Lang is " + Configuration.Lang);
            Debug.WriteLine($"[{DateTime.Now.ToShortTimeString()}] Selected Language In GUI: " + languageComboBox.SelectedItem + " | " + languageComboBox.SelectedIndex);
            #endregion
        }
        private void UpdateLang()
        {
            Text = I18N.LangResource.settingsForm?.settings?.ToString();
            languageLabel.Text = I18N.LangResource.settingsForm?.language?.ToString() + ':';
            closeOnExitCheckBox.Text = I18N.LangResource.settingsForm?.closeOnExit?.ToString();
            btn_dwnlAllLangs.Text = I18N.LangResource.settingsForm?.downloadAll?.ToString();
            cB_hidePatchExtension.Text = I18N.LangResource.settingsForm?.hidePatchExtension?.ToString();
            tabPage_General.Text = I18N.LangResource.settingsForm?.tabs?.general?.ToString();
            tabPage_Language.Text = I18N.LangResource.settingsForm?.tabs?.language?.ToString();
            cB_ShowVanilla.Text = I18N.LangResource.settingsForm?.showVanilla?.ToString();
            cB_OnlyAllowOneExe.Text = I18N.LangResource.settingsForm?.onlyOneExe?.ToString();
            cB_onlyOneUTL.Text = I18N.LangResource.settingsForm?.onlyOneUTL?.ToString();
            comboBox_gamesNamingType.Items.Clear();
            for (var i = 0; i < 4; i++) comboBox_gamesNamingType.Items.Add(I18N.LangResource.settingsForm?.namingType?[i].ToString() ?? throw new InvalidOperationException());
        }
        private void UpdateCredits()
        {
            var credits = "";
            foreach (dynamic author in I18N.LangResource.metadata.authors)
            {
                credits += author?.ToString() + ", ";
            }
            credits = credits.TrimEnd(' ', ',');
            int place = credits.LastIndexOf(',');
            if (place != -1)
            {
                credits = credits.Remove(place, 1).Insert(place, " " + I18N.LangResource.settingsForm.and?.ToString());
            }
            langCreditsLabel.Text = string.Format(I18N.LangResource.settingsForm.langCredits?.ToString(), credits);
        }
        #endregion

        #region GUI-less Methods
        static string ReadTextFromUrl(string url)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Assume UTF8, but detect BOM - could also honor response charset I suppose
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.UserAgent, "UTL");
            var stream = client.OpenRead(url);
            using (var textReader = new StreamReader(stream ?? throw new InvalidOperationException(), Encoding.UTF8, true))
            {
                return textReader.ReadToEnd();
            }
        }
        #endregion
    }
}
