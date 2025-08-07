using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Universal_THCRAP_Launcher.Properties;
using File = System.IO.File;

// ReSharper disable IdentifierTypo

/* WARNING: This code has been made by a new developer with WinForms
 * and the quality of code is very bad. If you want to be able to get this working, ensure:
 * NuGet packages are working.
 * Both "code behinds" have been loaded up in th editor once.
 * In Debug tab the working directory has been set to the thcrap directory.
 */

namespace Universal_THCRAP_Launcher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region Global variables

        private const string CONFIG_FOLDER = @"..\config\";
        private const string CONFIG_FILE = CONFIG_FOLDER + @"utl_config.json";
        private const string FAVORITE_FILE = CONFIG_FOLDER + @"favorite.json";
        private const string GAMES_FILE = CONFIG_FOLDER + @"games.js";
        private const string DONATE_LINK = @"https://opencollective.com/thpatch/";
        private const string REPO = @"thpatch/Universal-THCRAP-Launcher";


        private readonly Image _custom = new Bitmap(Resources.Custom);
        private readonly Image _game = new Bitmap(Resources.Game);

        private readonly Image _gameAndCustom = new Bitmap(Resources.GameAndCustom);

        private readonly Image _sortAscending = new Bitmap(Resources.Sort_Ascending);
        private readonly Image _sortDescending = new Bitmap(Resources.Sort_Decending);

        private readonly Image _star = new Bitmap(Resources.Star);
        private readonly Image _starHollow = new Bitmap(Resources.Star_Hollow);

        private List<string> _jsFiles = new List<string>();
        private List<string> _thcrapFiles = new List<string>();

        private int[] _resizeConstants;
        private Dictionary<string, string> _gamesDictionary;
        public static Dictionary<string, string> GameFullNameDictionary = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _displayNameToThxxDictionary = new Dictionary<string, string>();
        private readonly List<string> _favoritesWithDisplayName = new List<string>();

        public static Configuration Configuration1 { get; private set; }
        private Favourites Favourites1 { get; set; } = new Favourites(new List<string>(), new List<string>());

        private readonly Log _log = new Log(@"..\logs\utl-log.txt");

        #endregion

        #region MainForm Events

        private void Form1_Load(object sender, EventArgs e)
        {
            InitData();
            CheckIfObsolote();
            DownloadCurrentLanguage();

            GetPatchList();
            SetDefaultSettings();

            UpdateDisplayStrings();

            LogConfiguration();
        }

        private void MainForm_Activated(object sender, EventArgs e) => FillJumpList();
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (ModifierKeys != Keys.None)
            {
                if (Configuration1.LastConfig != null) patchListBox.SelectedItem = Configuration1.LastConfig;
                gameListBox.SelectedItem = Configuration1.LastGame;
            }

            switch (e.KeyCode)
            {
                case Keys.F12:
                    throw new Exception("This exception is for Debug purposes. Please don't press F12.");
                case Keys.F3:
                    UpdateDisplayStrings();
                    break;
                case Keys.F2 when sender.GetType().FullName != "System.Windows.Forms.ListBox":
                case Keys.Enter when sender.GetType().FullName != "System.Windows.Forms.ListBox":
                    return;
                case Keys.Enter:
                    UpdateConfigFile();
                    _ = Task.Run(StartThcrap);
                    break;
                case Keys.F2:
                    AddFavorite((ListBox)sender);
                    UpdateConfigFile();
                    break;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                startButton.Size = new Size(Size.Width - _resizeConstants[0], startButton.Size.Height);
                splitContainer1.Size = new Size(Size.Width - _resizeConstants[1], Size.Height - _resizeConstants[2]);
                if (btn_sortAZ2.Location.X < btnDeletePatch.Location.X + btnDeletePatch.Width + 20)
                    splitContainer1.SplitterDistance = btnDeletePatch.Location.X + btnDeletePatch.Width + 4;
                if (btn_Random2.Location.X + btn_Random2.Width > splitContainer1.Location.X + splitContainer1.Width)
                    splitContainer1.SplitterDistance = startButton.Location.X + startButton.Width - btn_Random2.Width * 5 - _resizeConstants[4] * 4 + 25;
                UpdateSplitContainerReleatedGui();
            }
            catch (Exception ex) { _log.WriteLine($"{ex}"); }

            if (WindowState != FormWindowState.Minimized) return;
            Hide();
            if (Configuration1.MinimizeNotificationWasShown) return;
            notifyIcon1.BalloonTipTitle = I18N.LangResource.mainForm?.utl?.ToString();
            notifyIcon1.BalloonTipText = I18N.LangResource.mainForm?.hided?.ToString();
            notifyIcon1.ShowBalloonTip(1000);
            Configuration1.MinimizeNotificationWasShown = true;
        }

        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            UpdateConfigFile();
            _log.WriteLine("Program closed.");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            #region Create constants for resizing

            _resizeConstants = new int[6];
            _resizeConstants[0] = Size.Width - startButton.Width;
            _resizeConstants[1] = Size.Width - splitContainer1.Width;
            _resizeConstants[2] = Size.Height - splitContainer1.Height;
            _resizeConstants[3] = splitContainer1.Location.Y - btn_sortAZ1.Location.Y;
            _resizeConstants[4] = btn_sortAZ2.Location.X - patchListBox.Size.Width;
            _resizeConstants[5] = btn_filterFav1.Location.X - btn_sortAZ1.Location.X;

            #endregion

            #region Display

            //Change Form settings
            SetDesktopLocation(Configuration1.Window.Location[0], Configuration1.Window.Location[1]);
            Size = new Size(Configuration1.Window.Size[0], Configuration1.Window.Size[1]);
            WindowState = Configuration1.WindowState;

            #endregion
			
			splitContainer1.SplitterDistance = Configuration1.SplitterDistance;

            ReadConfig();

            PopulatePatchList();
            PopulateGames();

            UpdateConfigFile();
        }

        #endregion

        #region GUI Element Events

        private void SplitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            if (e.SplitX < btnDeletePatch.Location.X + btnDeletePatch.Width)
            {
                Cursor.Position = new Point(Location.X + btnDeletePatch.Location.X + btnDeletePatch.Width + _resizeConstants[4] + 4, Cursor.Position.Y);
            }
            if (e.SplitX > startButton.Location.X + startButton.Width - btn_Random2.Width * 5 - _resizeConstants[4] * 4 + 20)
                Cursor.Position = new Point(Location.X + startButton.Location.X + startButton.Width - btn_Random2.Width * 5 - _resizeConstants[4] * 4 + 37, Cursor.Position.Y);
        }
        private void SplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            try
            {
                UpdateSplitContainerReleatedGui();
            }
            catch (Exception ex)
            {
                _log.WriteLine("Couldn't update splitter related GUI:\n\t" + ex);
            }
        }
        private void startButton_Click(object sender, EventArgs e) => _ = Task.Run(StartThcrap);

        private void Btn_AddFavorite0_Click(object sender, EventArgs e) => AddFavorite(patchListBox);
        private void Btn_AddFavorite1_Click(object sender, EventArgs e) => AddFavorite(gameListBox);

        private void startButton_MouseHover(object sender, EventArgs e) =>
            startButton.BackgroundImage = Resources.Shinmera_Banner_5_mini_size_hover;

        private void startButton_MouseLeave(object sender, EventArgs e) =>
            startButton.BackgroundImage = Resources.Shinmera_Banner_5_mini_size;

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModifierKeys != Keys.None) return;
            var lb = (ListBox)sender;
            switch (lb.Name)
            {
                case "listBox1":
                    {
                        if (lb.SelectedIndex != -1) Configuration1.LastConfig = lb.SelectedItem.ToString().Replace(" ★", "");
                        break;
                    }

                case "listBox2":
                    {
                        if (lb.SelectedIndex != -1) Configuration1.LastGame = lb.SelectedItem.ToString().Replace(" ★", "");
                        break;
                    }
            }
        }

        private void Btn_Random1_Click(object sender, EventArgs e) => SelectRandomInListBox(patchListBox);
        private void Btn_Random2_Click(object sender, EventArgs e) => SelectRandomInListBox(gameListBox);

        private void BtnDeletePatch_Click(object sender, EventArgs e)
        {
            string s = patchListBox.SelectedItem.ToString().Replace(" ★", "");
            if (Configuration1.HidePatchExtension && _jsFiles.Contains(s)) s += ".js";
            if (Configuration1.HidePatchExtension && _thcrapFiles.Contains(s)) s += ".thcrap";
            File.Delete(s);
            _log.WriteLine($"Patch {s} has been deleted.");
            if (s.Contains(".js")) _jsFiles.Remove(s);
            if (s.Contains(".thcrap")) _thcrapFiles.Remove(s);
            PopulatePatchList();
        }

        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
            {
                Show();
                if (WindowState == FormWindowState.Minimized) WindowState = FormWindowState.Normal;
                Activate();
            }

            if ((e.Button & MouseButtons.Right) != 0)
            {
                contextMenuStrip1.Show(MousePosition, ToolStripDropDownDirection.AboveLeft);
            }
        }

        private void TryNewLauncherLabel_Click(object sender, EventArgs e)
        {
            TryNewLauncherLabel.Visible = false;
            TryNewLauncherLabel.Enabled = false;
        }

        #region Sorting/Filtering Button Click Methods

        private void sortAZButton1_Click(object sender, EventArgs e)
        {
            string[] isDesc = Configuration1.IsDescending;
            if (btn_sortAZ1.BackgroundImage.Equals(_sortDescending))
            {
                btn_sortAZ1.BackgroundImage = _sortAscending;
                isDesc[0] = "false";
            }
            else
            {
                isDesc[0] = "true";
                btn_sortAZ1.BackgroundImage = _sortDescending;
            }

            PopulatePatchList();
            Configuration1.IsDescending = isDesc;
            ReadConfig();
        }

        private void sortAZButton2_Click(object sender, EventArgs e)
        {
            string[] isDesc = Configuration1.IsDescending;
            if (btn_sortAZ2.BackgroundImage.Equals(_sortDescending))
            {
                btn_sortAZ2.BackgroundImage = _sortAscending;
                isDesc[1] = "false";
            }
            else
            {
                isDesc[1] = "true";
                btn_sortAZ2.BackgroundImage = _sortDescending;
            }

            PopulateGames();
            Configuration1.IsDescending = isDesc;
            ReadConfig();
        }

        private void filterButton1_Click(object sender, EventArgs e)
        {
            string[] onlyFav = Configuration1.OnlyFavorites;
            if (!btn_filterFav1.BackgroundImage.Equals(_star))
            {
                btn_filterFav1.BackgroundImage = _star;
                onlyFav[0] = "true";
            }
            else
            {
                btn_filterFav1.BackgroundImage = _starHollow;
                onlyFav[0] = "false";
            }
            PopulatePatchList();
            Configuration1.OnlyFavorites = onlyFav;
            ReadConfig();
        }

        private void filterButton2_Click(object sender, EventArgs e)
        {
            string[] onlyFav = Configuration1.OnlyFavorites;
            if (!btn_filterFav2.BackgroundImage.Equals(_star))
            {
                btn_filterFav2.BackgroundImage = _star;
                onlyFav[1] = "true";
            }
            else
            {
                btn_filterFav2.BackgroundImage = _starHollow;
                onlyFav[1] = "false";
            }

            PopulateGames();
            Configuration1.OnlyFavorites = onlyFav;
            ReadConfig();
        }

        private void filterByType_button_Click(object sender, EventArgs e)
        {
            if (btn_filterByType.BackgroundImage.Equals(_gameAndCustom))
            {
                btn_filterByType.BackgroundImage = _game;
                Configuration1.FilterExeType = 1;
                PopulateGames();
                return;
            }

            if (btn_filterByType.BackgroundImage.Equals(_game))
            {
                btn_filterByType.BackgroundImage = _custom;
                Configuration1.FilterExeType = 2;
                PopulateGames();
                return;
            }

            if (!btn_filterByType.BackgroundImage.Equals(_custom)) return;
            {
                btn_filterByType.BackgroundImage = _gameAndCustom;
                Configuration1.FilterExeType = 0;
                PopulateGames();
            }


        }

        #endregion

        #region Tool Strip Click Event Methods

        private void keyboardShortcutsTS_Click(object sender, EventArgs e) => ShowKeyboardShortcuts();
        private void restartTS_Click(object sender, EventArgs e) => RestartProgram();
        private void exitTS_Click(object sender, EventArgs e) => Application.Exit();

        private void bugReportTS_Click(object sender, EventArgs e) => Process.Start(
                                                                                    $"https://github.com/{REPO}/issues/" +
                                                                                    "new?assignees=&labels=bug&template=bug_report.md&title=%5BBUG%5D");

        private void otherTS_Click(object sender, EventArgs e) =>
            Process.Start($"https://github.com/{REPO}/issues/new");

        private void gitHubPageTS_Click(object sender, EventArgs e) =>
            Process.Start($"https://github.com/{REPO}");

        private void DonateToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start(DONATE_LINK);

        private void openConfigureTS_Click(object sender, EventArgs e)
        {
            MessageBox.Show(I18N.LangResource.popup.hideLauncher.text?.ToString(),
                            I18N.LangResource.popup.hideLauncher.caption?.ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
            Process p = Process.Start("thcrap_configure_v3.exe");
            if (p == null)
            {
                MessageBox.Show(I18N.LangResource.errors.oops?.ToString(), I18N.LangResource.errors.error?.ToString());
                return;
            }

            Hide();
            while (!p.HasExited) Thread.Sleep(1);
            Show();
        }

        private void openGamesListTS_Click(object sender, EventArgs e) => Process.Start(GAMES_FILE);
        private void openFolderTS_Click(object sender, EventArgs e) => Process.Start(Directory.GetCurrentDirectory());

        private void CreateShortcutDesktopTS_Click(object sender, EventArgs e) => CreateShortcut((object)"Desktop");

        private void CreateShortcutStartMenuTS_Click(object sender, EventArgs e) => CreateShortcut((object)"Programs");

        private void openSelectedPatchConfigurationTS_Click(object sender, EventArgs e)
        {
            if (patchListBox.SelectedIndex == -1)
            { 
                SystemSounds.Hand.Play();
                return;
            }

            string v;
            v = (I18N.LangResource.mainForm.vanilla.ToString() is null) ? @"VANILLA" : I18N.LangResource.mainForm.vanilla.ToString();
            if (patchListBox.SelectedItem.ToString().Contains(v))
            {
                SystemSounds.Hand.Play();
                return;
            }
            
            string path = Directory.GetCurrentDirectory() + @"\" + CONFIG_FOLDER +
                          patchListBox.SelectedItem.ToString().Replace(" ★", "");
            if (Configuration1.HidePatchExtension)
            {
                if (_jsFiles.Contains(patchListBox.SelectedItem.ToString().Replace(" ★", ""))) path += ".js";
                if (_thcrapFiles.Contains(patchListBox.SelectedItem.ToString().Replace(" ★", ""))) path += ".thcrap";
            }
            Process.Start(path);
        }
        private void settingsTS_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm(this);
            settingsForm.ShowDialog();
            UpdateDisplayStrings();
        }

        #endregion

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(sender + @": " + e.Data);
            _log.WriteLine(sender + @": " + e.Data);
        }

        #endregion

        #region Configuration Methods

        private void SetDefaultSettings()
        {
            //Default Configuration setting
            try
            {
                Configuration1 = Configuration1 ?? new Configuration();

                if (Configuration.Lang == null)
                {
                    Configuration.Lang = "en.json";
                    _log.WriteLine($"Configuration.Lang has been set to {Configuration.Lang}");
                }

                if (Configuration1.LastGame == null)
                {
                    Configuration1.LastGame = _displayNameToThxxDictionary.Count != 0 ? _displayNameToThxxDictionary.Keys.ElementAt(0) : _gamesDictionary.Keys.ElementAt(0);
                    _log.WriteLine(
                                  $"Configuration1.LastGame has been set to {Configuration1.LastGame}");
                }

                if (Configuration1.SplitterDistance == 0)
                {
                    Configuration1.SplitterDistance = splitContainer1.SplitterDistance;
                    _log.WriteLine($"Configuration1.SplitterDistance has been set to {Configuration1.SplitterDistance}");
                }

                if (Configuration1.LastConfig == null && _jsFiles.Count > 0)
                {
                    Configuration1.LastConfig = _jsFiles[0];
                    _log.WriteLine(
                                  $"Configuration1.LastConfig has been set to {Configuration1.LastConfig}");
                }

                if (Configuration1.IsDescending == null)
                {
                    string[] a = { "false", "false" };
                    Configuration1.IsDescending = a;
                    _log.WriteLine(
                                  $"Configuration1.IsDescending has been set to {Configuration1.IsDescending[0]}, " +
                                  Configuration1.IsDescending[1]);
                }

                if (Configuration1.OnlyFavorites == null)
                {
                    string[] a = { "false", "false" };
                    Configuration1.OnlyFavorites = a;
                    _log.WriteLine(
                                  $"Configuration1.OnlyFavorites has been set to {Configuration1.OnlyFavorites[0]}, " +
                                  Configuration1.OnlyFavorites[1]);
                }

                if (Configuration1.Window == null)
                {
                    var window = new Window
                    {
                        Size = new[] { Size.Width, Size.Height },
                        Location = new[] { Location.X, Location.Y }
                    };
                    Configuration1.Window = window;
                    _log.WriteLine(
                                  "Configuration1.Window has been set with the following properties:");
                    _log.WriteLine(
                                  $"Configuration1.Window.Size: {Configuration1.Window.Size[0]}, {Configuration1.Window.Size[1]}");
                    _log.WriteLine(
                                  $"Configuration1.Window.Location: {Configuration1.Window.Location[0]}, {Configuration1.Window.Location[1]}");
                }
                if (Configuration1.Window.Location[0] == -32000)
                {
                    for (var i = 0; i < 2; i++)
                        Configuration1.Window.Location[i] = 0;
                    _log.WriteLine("Configuration1.Window.Location has been set offscreen by that nasty bug. Resetting it.");
                }
                SetDefaultSorting();
                SetDefaultFavButtonState();
                SetDefaultExeFilterButtonState();

                Favourites1.Games = Favourites1.Games ?? new List<string>();
                Favourites1.Patches = Favourites1.Patches ?? new List<string>();
            }
            catch (Exception e)
            {
                MessageBox.Show($@"1. If you're a developer: Don't forget to set the working directory to thcrap's directory. Your current working directory is: {Environment.CurrentDirectory}
2. If you're a dev in the right working directory this is for you:{Environment.NewLine}====={Environment.NewLine}{e}{Environment.NewLine}=====
3. If you're an end user, try reinstalling again carefully following the instructions this time or you can ask in #support on Discord (https://discord.thpatch.net/).");
                Application.Exit();
            }
        }

        private void SetDefaultExeFilterButtonState()
        {
            //Default exe type button state
            btn_filterByType.BackgroundImage = _gameAndCustom;
            for (var i = 0; i < Configuration1.FilterExeType; i++)
            {
                _log.WriteLine("Configuration1.FilterExeType");
                filterByType_button_Click("DefaultSettings", EventArgs.Empty);
            }
        }

        private void SetDefaultFavButtonState()
        {
            for (var i = 0; i < 2; i++)
            {
                if (Configuration1.OnlyFavorites[i] == "true")
                {
                    _log.WriteLine(
                                  $"Configuration1.OnlyFavorites was true for listBox{i}");
                    if (i == 0)
                    {
                        btn_filterFav1.BackgroundImage = _star;
                        for (int n = patchListBox.Items.Count - 1; n >= 0; --n)
                        {
                            const string filterItem = "★";
                            if (!patchListBox.Items[n].ToString().Contains(filterItem))
                                patchListBox.Items.RemoveAt(n);
                        }
                    }
                    else
                    {
                        btn_filterFav2.BackgroundImage = _star;
                        for (int n = gameListBox.Items.Count - 1; n >= 0; --n)
                        {
                            const string filterItem = "★";
                            if (!gameListBox.Items[n].ToString().Contains(filterItem))
                                gameListBox.Items.RemoveAt(n);
                        }
                    }
                }
                else
                {
                    if (i == 0)
                        btn_filterFav1.BackgroundImage = _starHollow;
                    else
                        btn_filterFav2.BackgroundImage = _starHollow;
                }
            }
        }

        private void SetDefaultSorting()
        {
            for (var i = 0; i < 2; i++)
            {
                if (Configuration1.IsDescending[i] == "false")
                {

                    if (i == 0)
                    {
                        SortListBoxItems(ref patchListBox);
                        btn_sortAZ1.BackgroundImage = _sortAscending;
                    }
                    else
                    {
                        SortListBoxItems(ref gameListBox);
                        btn_sortAZ2.BackgroundImage = _sortAscending;
                    }
                }
                else if (i == 0)
                {

                    SortListBoxItemsDesc(ref patchListBox);
                    btn_sortAZ1.BackgroundImage = _sortDescending;
                }
                else
                {
                    SortListBoxItemsDesc(ref gameListBox);
                    btn_sortAZ2.BackgroundImage = _sortDescending;
                }
            }
        }

        /// <summary>
        ///     Selects the items based on the configuration
        /// </summary>
        private void ReadConfig()
        {
            string s;
            if (Configuration1.LastConfig != null)
            {
                s = Configuration1.LastConfig;
                if (Favourites1.Patches.Contains(s)) s += " ★";
                patchListBox.SelectedIndex = patchListBox.FindStringExact(s);
            }
            s = Configuration1.LastGame;

            if (Favourites1.Games.Contains(s)) s += " ★";

            gameListBox.SelectedIndex = gameListBox.FindStringExact(s);

            if (patchListBox.SelectedIndex == -1 && patchListBox.Items.Count > 0) patchListBox.SelectedIndex = 0;
            if (gameListBox.SelectedIndex == -1 && gameListBox.Items.Count > 0) gameListBox.SelectedIndex = 0;
        }

        /// <summary>
        ///     Updates the configuration and favorites list
        /// </summary>
        private void UpdateConfig()
        {
            Configuration1.SplitterDistance = splitContainer1.SplitterDistance;
            if (patchListBox.SelectedIndex == -1 && patchListBox.Items.Count > 0) patchListBox.SelectedIndex = 0;
            if (patchListBox.SelectedIndex != -1)
                Configuration1.LastConfig = ((string)patchListBox.SelectedItem).Replace(" ★", "");
            if (gameListBox.SelectedIndex == -1 && gameListBox.Items.Count > 0) gameListBox.SelectedIndex = 0;
            if (gameListBox.SelectedIndex != -1)
                Configuration1.LastGame = ((string)gameListBox.SelectedItem).Replace(" ★", "");

            Configuration1.WindowState = WindowState;
            if (WindowState != FormWindowState.Maximized)
            {
                var window = new Window
                {
                    Size = new[] { Size.Width, Size.Height },
                    Location = new[] { Location.X, Location.Y }
                };
                Configuration1.Window = window;
            }

            Favourites1.Patches.Clear();
            Favourites1.Games.Clear();

            foreach (string s in patchListBox.Items)
            {
                if (s.Contains("★"))
                {
                    string v = s.Replace(" ★", "");
                    if (v == $@"[{I18N.LangResource.mainForm.vanilla.ToString()}]") v = @"VANILLA";
                    Favourites1.Patches.Add(v);
                }
            }

            foreach (string s in gameListBox.Items)
            {
                if (s.Contains("★"))
                {
                    string v = s.Replace(" ★", "");
                    _displayNameToThxxDictionary.TryGetValue(v, out v);
                    Favourites1.Games.Add(v);
                }
            }
        }

        /// <summary>
        ///     Writes the configuration and favorites to file
        /// </summary>
        public void UpdateConfigFile()
        {
            if (Environment.CurrentDirectory == @"C:\Windows\system32") return;
            UpdateConfig();
            string output = JsonConvert.SerializeObject(Configuration1, Formatting.Indented, new JsonSerializerSettings());
            output = output.Remove(output.Length - 3);
            output += ",\n  \"Lang\": " +
                      JsonConvert.SerializeObject(Configuration.Lang, Formatting.Indented,
                                                  new JsonSerializerSettings()) + "\n}";

            string dirName = Path.GetDirectoryName(CONFIG_FILE);
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName ?? CONFIG_FOLDER);
            File.WriteAllText(CONFIG_FILE, output);

            output = JsonConvert.SerializeObject(Favourites1, Formatting.Indented);
            File.WriteAllText(FAVORITE_FILE, output);

            _log.WriteLine("Config file has been successfully updated.");
        }

        #endregion

        #region Methods Related to GUI

        private void CheckIfObsolote()
        {
            var url = (@"https://api.github.com/repos/" + REPO);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "request");



            try
            {
                string response = client.GetStringAsync(url).Result;
                dynamic json = JsonConvert.DeserializeObject(response);

                if (json.archived == "true")
                {
                    TryNewLauncherLabel.Visible = true;
                    TryNewLauncherLabel.Enabled = true;
                }
            }
            catch (Exception e)
            {
                _log.WriteLine($"Couldn't check if the launcher is obsolote:\n\t{e}");
            }
            
            
        }

        public void PopulateGames()
        {
            gameListBox.Items.Clear();
            _displayNameToThxxDictionary.Clear();
            _favoritesWithDisplayName.Clear();
            //Display executables
            foreach (KeyValuePair<string, string> item in _gamesDictionary)
            {
                try
                {
                    string name = GetPrettyTouhouName(item.Key, Configuration1.NamingForGames);
                    gameListBox.Items.Add(name ?? throw new InvalidOperationException());
                    _displayNameToThxxDictionary.Add(name, item.Key);
                    if (Favourites1.Games.Contains(item.Key))
                        _favoritesWithDisplayName.Add(name);
                }
                catch (Exception e)
                {
                    _log.WriteLine($"Couldn't add {item.Key} to the games listbox:\n\t{e}");
                }
            }

            AddStars(gameListBox, _favoritesWithDisplayName);

            if (bool.Parse(Configuration1.IsDescending[1])) SortListBoxItemsDesc(ref gameListBox);
            else SortListBoxItems(ref gameListBox);

            if (bool.Parse(Configuration1.OnlyFavorites[1])) FilterByFav(gameListBox);

            FilterByExeType();

            if (gameListBox.SelectedIndex == -1 && gameListBox.Items.Count > 0) gameListBox.SelectedIndex = 0;
            LogListBoxes();
            if (ActiveForm != null) FillJumpList();
        }

        private void GetPatchList()
        {
            _jsFiles.Clear();
            _thcrapFiles.Clear();
            patchListBox.Items.Clear();

            //Load patch stacks
            const string patchesFolder = @"\..\config\";
            _jsFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + patchesFolder, "*.js").ToList();
            _thcrapFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + patchesFolder, "*.thcrap").ToList();

            //Give error if there are no patch configurations
            if (_jsFiles.Count == 0 && _thcrapFiles.Count == 0) ErrorAndExit(I18N.LangResource.errors.missing.patchStacks);


            #region Fix patch stack list

            for (var i = 0; i < _jsFiles.Count; i++)
                _jsFiles[i] = _jsFiles[i].Replace(Directory.GetCurrentDirectory() + patchesFolder, "");
            _jsFiles.Remove("games.js");
            _jsFiles.Remove("config.js");
            if (Configuration1.HidePatchExtension)
            {
                for (var i = 0; i < _jsFiles.Count; i++)
                    _jsFiles[i] = _jsFiles[i].Replace(".js", "");
                for (var i = 0; i < _thcrapFiles.Count; i++)
                    _thcrapFiles[i] = _thcrapFiles[i].Replace(".thcrap", "");

            }

            #endregion
        }

        public void PopulatePatchList()
        {
            GetPatchList();

            //Display patch stacks
            if (Configuration1.ShowVanilla) patchListBox.Items.Add($"[{I18N.LangResource.mainForm.vanilla}]");
            foreach (string item in _jsFiles) patchListBox.Items.Add(item);
            foreach (string item in _thcrapFiles) patchListBox.Items.Add(item);

            if (Favourites1.Patches.Contains(@"VANILLA"))
            {
                Favourites1.Patches.Remove(@"VANILLA");
                Favourites1.Patches.Add($@"[{I18N.LangResource.mainForm.vanilla}]");
            }
            AddStars(patchListBox, Favourites1.Patches);
            if (Favourites1.Patches.Contains($@"[{I18N.LangResource.mainForm.vanilla}]"))
            {
                Favourites1.Patches.Remove($@"[{I18N.LangResource.mainForm.vanilla}]");
                Favourites1.Patches.Add(@"VANILLA");
            }

            if (bool.Parse(Configuration1.IsDescending[0])) SortListBoxItemsDesc(ref patchListBox);
            else SortListBoxItems(ref patchListBox);

            if (bool.Parse(Configuration1.OnlyFavorites[0])) FilterByFav(patchListBox);

            if (patchListBox.SelectedIndex == -1 && patchListBox.Items.Count > 0) patchListBox.SelectedIndex = 0;
            LogListBoxes();
            if (ActiveForm != null) FillJumpList();
        }
        private void UpdateSplitContainerReleatedGui()
        {
            patchListBox.Size = new Size(splitContainer1.Panel1.Width - 1, splitContainer1.Panel1.Height - 1);
            gameListBox.Size = new Size(splitContainer1.Panel2.Width - 1, splitContainer1.Panel2.Height - 1);
            btn_sortAZ1.Location =
                new Point(btn_sortAZ1.Location.X, splitContainer1.Location.Y - _resizeConstants[3]);
            btn_sortAZ2.Location =
                new Point(patchListBox.Size.Width + _resizeConstants[4],
                          splitContainer1.Location.Y - _resizeConstants[3]);
            btn_filterFav1.Location =
                new Point(btn_sortAZ1.Location.X + _resizeConstants[5],
                          splitContainer1.Location.Y - _resizeConstants[3]);
            btn_filterFav2.Location =
                new Point(btn_sortAZ2.Location.X + _resizeConstants[5],
                          splitContainer1.Location.Y - _resizeConstants[3]);
            btn_filterByType.Location =
                new Point(btn_filterFav2.Location.X + _resizeConstants[5],
                          splitContainer1.Location.Y - _resizeConstants[3]);
            btn_AddFavorite0.Location =
                new Point(btn_filterFav1.Location.X + _resizeConstants[5],
                          splitContainer1.Location.Y - _resizeConstants[3]);
            btn_AddFavorite1.Location =
                new Point(btn_filterByType.Location.X + _resizeConstants[5],
                          splitContainer1.Location.Y - _resizeConstants[3]);
            btn_Random1.Location =
                new Point(btn_AddFavorite0.Location.X + _resizeConstants[5],
                          splitContainer1.Location.Y - _resizeConstants[3]);
            btn_Random2.Location =
                new Point(btn_AddFavorite1.Location.X + _resizeConstants[5],
                          splitContainer1.Location.Y - _resizeConstants[3]);
            btnDeletePatch.Location =
                new Point(btn_Random1.Location.X + _resizeConstants[5],
                          splitContainer1.Location.Y - _resizeConstants[3]);
        }

        private void UpdateDisplayStrings()
        {
            dynamic objLangRes = I18N.LangResource.mainForm;

            Text = objLangRes.utl + @" " + Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf('.'));
            toolTip1.SetToolTip(startButton, objLangRes.tooltips.startButton?.ToString());
            toolTip1.SetToolTip(btn_sortAZ1, objLangRes.tooltips.sortAZ?.ToString());
            toolTip1.SetToolTip(btn_sortAZ2, objLangRes.tooltips.sortAZ?.ToString());
            toolTip1.SetToolTip(btn_filterFav1, objLangRes.tooltips.filterFav?.ToString());
            toolTip1.SetToolTip(btn_filterFav2, objLangRes.tooltips.filterFav?.ToString());
            toolTip1.SetToolTip(btn_filterByType, objLangRes.tooltips.filterByType?.ToString());
            toolTip1.SetToolTip(patchListBox, objLangRes.tooltips.patchLB?.ToString());
            toolTip1.SetToolTip(gameListBox, objLangRes.tooltips.gameLB?.ToString());
            toolTip1.SetToolTip(btn_AddFavorite0, objLangRes.tooltips.patchFav?.ToString());
            toolTip1.SetToolTip(btn_AddFavorite1, objLangRes.tooltips.gamesFav?.ToString());
            toolTip1.SetToolTip(btn_Random1, objLangRes.tooltips.random?.ToString());
            toolTip1.SetToolTip(btn_Random2, objLangRes.tooltips.random?.ToString());
            toolTip1.SetToolTip(btnDeletePatch, objLangRes.tooltips.delete?.ToString());

            // - TODO: Refactor this code
            menuStrip1.Items[0].Text = objLangRes.menuStrip[0][0];
            for (var i = 0; i < ((ToolStripMenuItem)menuStrip1.Items[0]).DropDownItems.Count; i++)
            {
                ((ToolStripMenuItem)menuStrip1.Items[0]).DropDownItems[i].Text = objLangRes.menuStrip[0][i + 1];
            }

            menuStrip1.Items[1].Text = objLangRes.menuStrip[1][0];
            for (var i = 0; i < ((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems.Count; i++)
            {
                if (objLangRes.menuStrip[1][i + 1] is JValue)
                {
                    ((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems[i].Text = objLangRes.menuStrip[1][i + 1];
                }

                if (objLangRes.menuStrip[1][i + 1] is JArray)
                {
                    ((ToolStripMenuItem)((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems[i]).Text =
                        objLangRes.menuStrip[1][i + 1][0];
                    for (var j = 0;
                         j < ((ToolStripMenuItem)((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems[i])
                            .DropDownItems.Count;
                         j++)
                    {
                        ((ToolStripMenuItem)((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems[i])
                           .DropDownItems[j].Text = objLangRes.menuStrip[1][i + 1][j + 1];
                    }
                }
            }

            menuStrip1.Items[2].Text = objLangRes.menuStrip[2][0];
            for (var i = 0; i < ((ToolStripMenuItem)menuStrip1.Items[2]).DropDownItems.Count; i++)
            {
                if (objLangRes.menuStrip[2][i + 1] is JValue)
                {
                    ((ToolStripMenuItem)menuStrip1.Items[2]).DropDownItems[i].Text = objLangRes.menuStrip[2][i + 1];
                }

                if (objLangRes.menuStrip[2][i + 1] is JArray)
                {
                    ((ToolStripMenuItem)((ToolStripMenuItem)menuStrip1.Items[2]).DropDownItems[i]).Text =
                        objLangRes.menuStrip[2][i + 1][0];
                    for (var j = 0;
                         j < ((ToolStripMenuItem)((ToolStripMenuItem)menuStrip1.Items[2]).DropDownItems[i])
                            .DropDownItems.Count;
                         j++)
                    {
                        ((ToolStripMenuItem)((ToolStripMenuItem)menuStrip1.Items[2]).DropDownItems[i])
                           .DropDownItems[j].Text = objLangRes.menuStrip[2][i + 1][j + 1];
                    }
                }
            }

            menuStrip1.Items[3].Text = objLangRes.menuStrip[3][0];
            for (var i = 0; i < ((ToolStripMenuItem)menuStrip1.Items[3]).DropDownItems.Count; i++)
            {
                ((ToolStripMenuItem)menuStrip1.Items[3]).DropDownItems[i].Text = objLangRes.menuStrip[3][i + 1];
            }

            // ---


        }

        private void AddStars(ListBox listBox, IEnumerable<string> list)
        {
            foreach (string variable in list)
            {
                string s = variable;
                if (Configuration1.HidePatchExtension)
                {
                    /*if (_jsFiles.Contains(s)) s += ".js";
                    if (_thcrapFiles.Contains(s)) s += ".thcrap";*/
                    s = s.Replace(".js", "").Replace(".thcrap", "");
                }
                int index = listBox.FindStringExact(s);
                if (index != -1) listBox.Items[index] += " ★";
            }
        }

        private static void SortListBoxItems(ref ListBox lb)
        {
            List<object> items = lb.Items.OfType<object>().ToList();
            lb.Items.Clear();
            lb.Items.AddRange(items.OrderBy(i => i).ToArray());
        }

        private static void SortListBoxItemsDesc(ref ListBox lb)
        {
            List<object> items = lb.Items.OfType<object>().ToList();
            lb.Items.Clear();
            lb.Items.AddRange(items.OrderByDescending(i => i).ToArray());
        }

        private void FilterByFav(IDisposable lb)
        {
            if (lb.Equals(patchListBox))
            {
                for (int n = patchListBox.Items.Count - 1; n >= 0; --n)
                {
                    const char filterItem = '★';
                    if (!patchListBox.Items[n].ToString().Contains(filterItem)) patchListBox.Items.RemoveAt(n);
                }
            }

            if (!lb.Equals(gameListBox)) return;
            {
                for (int n = gameListBox.Items.Count - 1; n >= 0; --n)
                {
                    const string filterItem = "★";
                    if (!gameListBox.Items[n].ToString().Contains(filterItem)) gameListBox.Items.RemoveAt(n);
                }
            }
        }

        private void FilterByExeType()
        {
            switch (Configuration1.FilterExeType)
            {
                case 0: break;
                case 1:
                    {
                        foreach (string item in _displayNameToThxxDictionary.Keys)
                        {
                            _displayNameToThxxDictionary.TryGetValue(item, out string s);
                            if (s != null && s.Contains("_custom"))
                                gameListBox.Items.Remove(_favoritesWithDisplayName.Contains(item) ? item + " ★" : item);
                        }
                        break;
                    }

                case 2:
                    {
                        foreach (string item in _displayNameToThxxDictionary.Keys)
                        {
                            _displayNameToThxxDictionary.TryGetValue(item, out string s);
                            if (s != null && !s.Contains("_custom"))
                                gameListBox.Items.Remove(_favoritesWithDisplayName.Contains(item) ? item + " ★" : item);
                        }

                        break;
                    }

                default: throw new InvalidOperationException();
            }
        }

        private static void RestartProgram()
        {
            Process.Start(Assembly.GetEntryAssembly()?.Location ?? throw new InvalidOperationException());
            Application.Exit();
        }

        private static void ShowKeyboardShortcuts() => MessageBox.Show(I18N.LangResource.popup.kbSh.text?.ToString(),
                                                                       I18N.LangResource.popup.kbSh.caption?.ToString(), MessageBoxButtons.OK,
                                                                       MessageBoxIcon.Information);

        private static void SelectRandomInListBox(ListBox lb)
        {
            var r = new Random();
            lb.SelectedIndex = r.Next(lb.Items.Count);
        }

        private void FillJumpList()
        {
            contextMenuStrip1.Items.Clear();
            var tsi = new ToolStripMenuItem(I18N.LangResource.mainForm?.utl?.ToString()) { Enabled = false };
            contextMenuStrip1.Items.Add(tsi);
            contextMenuStrip1.Items.Add(new ToolStripSeparator());
            foreach (string game in Favourites1.Games)
            {
                foreach (string patch in Favourites1.Patches)
                {
                    string fpatch = patch;
                    if (patch == @"VANILLA") continue;
                    if (Configuration1.HidePatchExtension && _jsFiles.Contains(patch)) fpatch = patch + ".js";
                    if (Configuration1.HidePatchExtension && _thcrapFiles.Contains(patch)) fpatch = patch + ".thcrap";
                    var jsi = new JumpListElement(game, fpatch);
                    jsi.Click += delegate
                    {
                        _ = Task.Run(delegate
                                     {
                                         StartThcrap(jsi.ToString(), game);
                                     }
                                    );
                    };
                    contextMenuStrip1.Items.Add(jsi);
                }
            }
            contextMenuStrip1.Items.Add(new ToolStripSeparator());
            tsi = new ToolStripMenuItem(I18N.LangResource.mainForm?.menuStrip?[0]?[6]?.ToString());
            tsi.Click += exitTS_Click;
            contextMenuStrip1.Items.Add(tsi);
        }

        #endregion

        #region Methods less releated to the GUI

        private void PrintLogsHeader(string exeDir) => _log.Write("\n――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――" +
                                                                 "\nUniversal THCRAP Launcher Log File" +
                                                                 "\nVersion: " + Application.ProductVersion +
                                                                 $"\nBuild Date: {Resources.BuildDate.Trim()}" +
                                                                 $"\nBuilt by: {Resources.BuildUser.Trim()}" +
                                                                 "\n++++++\nWorking Directory: " + Environment.CurrentDirectory +
                                                                 "\nDirectory of Exe: " + exeDir +
                                                                 "\nCurrent Date: " + DateTime.Now +
                                                                 "\nDo these files below exist:" +
                                                                 "\nthcrap_configure.exe\tNewtonsoft.Json.dll\tCONFIG_FILE\tFAVORITE_FILE\tGAMES_FILE ?" +
                                                                 $"\n{File.Exists("thcrap_configure.exe")}\t\t\t{File.Exists(exeDir + "Newtonsoft.Json.dll")}\t\t\t{File.Exists(CONFIG_FILE)}\t\t{File.Exists(FAVORITE_FILE)}\t\t{File.Exists(GAMES_FILE)}" +
                                                                 "\n――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――\n\n");

        private bool CheckForNewtonsoftJson(string exeDir)
        {
            //Give error if Newtonsoft.Json.dll isn't found.
            if (File.Exists(exeDir + "Newtonsoft.Json.dll"))
            {
                return true;
            }
            _log.WriteLine("Newtonsoft.Json.dll not found, exiting...");

            //Read parser-less, the error message.
            if (Configuration.Lang == null) Configuration.Lang = "en.json";

            var errorTitle = "Error";
            var errorMessage = "Newtonsoft.Json.dll is missing. Please make sure it has been copied over as well.";
            if (File.Exists(I18N.I18NDir + Configuration.Lang))
            {
                string[] lines = File.ReadAllLines(I18N.I18NDir + Configuration.Lang);
                foreach (string item in lines)
                {
                    if (item.Contains("\"error\""))
                        errorTitle = item.Split('"')[3];
                    if (item.Contains("\"jsonParser\""))
                        errorMessage = item.Split('"')[3];
                }
            }

            MessageBox.Show(errorMessage, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        private object LoadConfig()
        {
            if (File.Exists(CONFIG_FILE))
            {
                var settings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
                string raw = File.ReadAllText(CONFIG_FILE);
                Configuration1 = JsonConvert.DeserializeObject<Configuration>(raw, settings);
                if (Configuration1 != null)
                {
                    return JsonConvert.DeserializeObject(raw, settings);
                }
            }

            Configuration1 = new Configuration();
            return null;
        }

        /// <summary>
        ///     Does checks to make sure everything is in place before starting the program
        /// </summary>
        public bool InitChecks()
        {
            if (Environment.CurrentDirectory == @"C:\Windows\system32")
            {
                // TODO: Make jump-list actually work
                MessageBox.Show(@"The application was launched from Windows/system32. This was probably because you used the Windows jump-list.
If you know how to fix the jump-list, you're welcome to give a pull request. Otherwise, just right-click in the notification tray.", @"There's a bug in the code, that idk how to fix", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";

            PrintLogsHeader(exeDir);
            if (!CheckForNewtonsoftJson(exeDir))
            {
                return false;
            }

            dynamic dconfig = LoadConfig();

            string langCode = DownloadTranslation();
            if (langCode == null)
            {
                return false;
            }

            //Load language
            if (!string.IsNullOrEmpty((string)dconfig?.Lang?.Value))
            {
                Configuration.Lang = dconfig.Lang.Value;
                if (!File.Exists(I18N.I18NDir + Configuration.Lang))
                {
                    _log.WriteLine($"Language is set to {Configuration.Lang}, but it's not downloaded. This can definitely go wrong,\n" +
                                  "so let's set it back to English...");
                    Configuration.Lang = "en.json";
                }
            }
            else
            {
                Configuration.Lang = langCode + ".json";
            }


            I18N.UpdateLangResource(I18N.I18NDir + Configuration.Lang);

            /* Windows Vista style JumpList requirements. This feature has been chosen to be NOT supported by UTL.
            CheckForMissingFile("JumpListHelpers.dll");
            CheckForMissingFile("Microsoft.WindowsAPICodePack.dll");
            CheckForMissingFile("Microsoft.WindowsAPICodePack.Shell.dll");
            */

            //Give error if not next to thcrap_loader.exe
            if (!File.Exists("thcrap_loader.exe"))
            {
                MessageBox.Show(I18N.LangResource?.errors?.missing?.thcrap_loader.ToString());
                return false;
            }

            //Give error if no games file
            if (!File.Exists(GAMES_FILE))
            {
                MessageBox.Show(I18N.LangResource?.errors?.missing?.gamesJs.ToString());
                return false;
            }

            if (Configuration1.OnlyAllowOneUtl && Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show(I18N.LangResource?.errors?.alreadyRunning.ToString());
                return false;
            }

            CreateShortcut("..");

            return true;
        }

        /// <summary>
        ///     Tries to download the given language, if it fails, tries to download English.
        /// </summary>
        /// <returns>The two letter ISO code for the language successfully downloaded</returns>
        private string DownloadTranslation()
        {
            string langCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            if (File.Exists(I18N.I18NDir + @"\" + langCode + ".json"))
            {
                // Nothing to do
                return langCode;
            }

            if (!Directory.Exists(I18N.I18NDir))
            {
                Directory.CreateDirectory(I18N.I18NDir);
            }

            const string langUrlBase = "https://raw.githubusercontent.com/" + REPO + "/master/langs/";

            try
            {    
                try
                {
                    string langUrl = langUrlBase + langCode + ".json";
                    _log.WriteLine($"Downloading {langUrl}...");
                    string lang = ReadTextFromUrl(langUrl);
                    File.WriteAllText(I18N.I18NDir + @"\" + langCode + ".json", lang);
                }
                catch (WebException wex)
                {
                    _log.WriteLine($"Couldn't download the language file for {langCode}, due to {wex.Message} . Trying to download English...");
                    string lang =
                        ReadTextFromUrl(langUrlBase + "en.json");
                    File.WriteAllText(I18N.I18NDir + @"\en.json", lang);
                    langCode = "en";
                }
            }
            catch (Exception ex)
            {
                _log.WriteLine($"Couldn't connect to GitHub for pulling down English language file.\nReason: {ex}");
                MessageBox.Show($@"No language files found and couldn't connect to GitHub to download English language file. Either put one manually into {I18N.I18NDir} or find out why you can't connect to {langUrlBase}en.json . Or use an older version of the program ¯\_(ツ)_/¯.",
                                @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return langCode;
        }

        /// <summary>
        ///     Initialized data into the global variables and etc.
        /// </summary>
        private void InitData()
        {

            //Load favorites
            if (File.Exists(FAVORITE_FILE))
            {
                string file = File.ReadAllText(FAVORITE_FILE);
                Favourites1 = JsonConvert.DeserializeObject<Favourites>(file);
            }

            //Load full names for games
            if (File.Exists(@"..\repos\nmlgc\script_latin\stringdefs.js"))
            {
                string file = File.ReadAllText(@"..\repos\nmlgc\script_latin\stringdefs.js");
                _log.WriteLine(@"Found repos\nmlgc\script_latin\stringdefs.js!");
                var stringdef = JsonConvert.DeserializeObject<Dictionary<string, string>>(file);
                foreach (KeyValuePair<string, string> variable in stringdef)
                {
                    if (Regex.IsMatch(variable.Key, "^th[0-9]{2,3}$"))
                    {
                        GameFullNameDictionary.Add(variable.Key, variable.Value);
                    }
                    if (variable.Key.Equals("alcostg"))
                    {
                        GameFullNameDictionary.Add(variable.Key, variable.Value);
                    }
                }
            }
            else _log.WriteLine(@"repos\nmlgc\script_latin\stringdefs.js does not exists!");
            if (Directory.Exists(@"..\repos\nmlgc\base_tasofro"))
            {
                _log.WriteLine(@"Found repos\nmlgc\base_tasofro!");
                foreach (string file in Directory.EnumerateFiles(@"..\repos\nmlgc\base_tasofro"))
                {
                    string raw = File.ReadAllText(file);
                    if (!raw.Contains("title")) continue;
                    dynamic content = JsonConvert.DeserializeObject(raw);
                    if (content?.title is null) continue;
                    string title = content.title.ToString();
                    string key = file.Replace(".js", "").Replace(@"..\repos\nmlgc\base_tasofro\", "");
                    if (key.Equals("patch")) continue; // We don't need the name of the base_tasofro patch
                    GameFullNameDictionary.Add(key, title);
                }
            }
            else _log.WriteLine(@"repos\nmlgc\base_tasofro does not exists!");

            //Load executables
            string rawFile = File.ReadAllText(GAMES_FILE);
            _gamesDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(rawFile);
        }
        /// <summary>
        ///     Download the currently set language in Configuration
        /// </summary>
        private void DownloadCurrentLanguage()
        {
            try
            {
                string newlang =
                    ReadTextFromUrl($"https://raw.githubusercontent.com/{REPO}/master/langs/" +
                                    Configuration.Lang);
                File.WriteAllText(I18N.I18NDir + "\\" + Configuration.Lang, newlang);
            }
            catch (Exception ex)
            {
                _log.WriteLine($"Couldn't connect to GitHub for language update.\nReason: {ex}");
            }
        }

        private void LogListBoxes()
        {
            _log.WriteLine($"Selected Indices: {patchListBox.SelectedIndex} | {gameListBox.SelectedIndex}");
            _log.WriteLine("Listboxes:");
            _log.Write("\tPatches: ");
            foreach (object item in patchListBox.Items)
                _log.Write($"{item}, ");
            _log.Write("\n\tGames: ");
            foreach (object item in gameListBox.Items)
                _log.Write($"{item}, ");
            _log.Write("\n");
        }
        /// <summary>
        ///     Logs the current configuration to the console and log file
        /// </summary>
        private void LogConfiguration()
        {
            _log.WriteLine("MainForm Loaded with the following Configuration:");
            _log.WriteLine($"\tLastConfig: {Configuration1.LastConfig}");
            _log.WriteLine($"\tLastGame: {Configuration1.LastGame}");
            _log.WriteLine($"\tFilterExeType: {Configuration1.FilterExeType}");
            _log.WriteLine($"\tHidePatchExtension: {Configuration1.HidePatchExtension}");
            _log.WriteLine($"\tLang: {Configuration.Lang}");
            _log.WriteLine($"\tExitAfterStartup: {Configuration1.ExitAfterStartup}");
            _log.WriteLine($"\tOnlyAllowOneExecutable: {Configuration1.OnlyAllowOneExecutable}");
            _log.WriteLine($"\tOnlyAllowOneUtl: {Configuration1.OnlyAllowOneUtl}");
            _log.WriteLine($"\tSplitterDistance: {Configuration1.SplitterDistance}");
            _log.WriteLine($"\tShowGameId: {Configuration1.ShowGameId}");
            _log.WriteLine($"\tIsDescending: {Configuration1.IsDescending[0]} | {Configuration1.IsDescending[1]}");
            _log.WriteLine($"\tOnlyFavorites: {Configuration1.OnlyFavorites[0]} | {Configuration1.OnlyFavorites[1]}");
            _log.WriteLine($"\tWindowsState: {Configuration1.WindowState}");
            _log.WriteLine("\tWindow:");
            _log.WriteLine($"\t\tLocation: {Configuration1.Window.Location[0]}, {Configuration1.Window.Location[1]}");
            _log.WriteLine($"\t\tSize: {Configuration1.Window.Size[0]}, {Configuration1.Window.Size[1]}");
        }
        /// <summary>
        ///     Based on the given game id (TH???) it returns the game's name in the given GameNameType.
        /// </summary>
        /// <param name="id">The game id to get the name of.</param>
        /// <param name="nameType">The GameNameType to get the name in.</param>
        /// <returns></returns>
        private static string GetPrettyTouhouName(string id, GameNameType nameType = GameNameType.ShortName)
        {
            GameFullNameDictionary.TryGetValue(id.Replace("_custom", ""), out string name);
            if (name != null)
            {
                name = name.Replace("~", "-").Replace("～", "-");
                if (id.Contains("_custom")) name += " ~ " + I18N.LangResource.mainForm?.custom?.ToString();
            }
            switch (nameType)
            {
                case GameNameType.Initials:
                    if (name != null)
                    {
                        var initials = new Regex(@"(\b[a-zA-Z])[a-zA-Z]* ?");
                        name = initials.Replace(name.Contains('-') ? name.Split('-')[1] : name, "$1");
                        name = name.Replace("~", " ~").Trim();
                    }
                    else
                        name = id.Trim();
                    break;
                case GameNameType.ShortName:
                    if (name == null) name = id;
                    name = !name.Contains('-') ? name.Trim() : name.Split('-')[1].Trim();
                    break;
                case GameNameType.LongName:
                    name = name ?? id;
                    break;
                case GameNameType.None:
                    return id;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return Configuration1.ShowGameId ? id + ": " + name : name;
        }
        /// <summary>
        ///     Creates shortcut to a special folder. <see cref="Environment.SpecialFolder" /> for the list of special folders.
        /// </summary>
        /// <param name="targetSpecialFolder">The special folder to create the shortcut in.</param>
        private void CreateShortcut(object targetSpecialFolder)
        {
            var shell = new WshShell();

            var shortcutDir = (string)shell.SpecialFolders.Item(ref targetSpecialFolder);
            CreateShortcut(shortcutDir);
        }
        /// <summary>
        ///     Create a shortcut at the specified location.
        /// </summary>
        /// <param name="folder">The folder to create the shortcut in. Should NOT end with a path separator.</param>
        private void CreateShortcut(string folder)
        {
            try
            {
                var shell = new WshShell();

                string shortcutAddress = folder + "\\" +
                                         I18N.LangResource.shCreate.file?.ToString() + ".lnk";
                if (File.Exists(shortcutAddress))
                    return;

                var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
                shortcut.Description = I18N.LangResource.shCreate.desc?.ToString();
                shortcut.TargetPath = Assembly.GetEntryAssembly()?.Location;
                shortcut.WorkingDirectory = Directory.GetCurrentDirectory();
                _log.WriteLine(
                              $"==\nTrying to Create Shortcut:\nPath: {shortcutAddress}\nDescription: {shortcut.Description}\nTarget path: {shortcut.TargetPath}\nWorking directory: {shortcut.WorkingDirectory}\n==");
                shortcut.Save();
            }
            catch (Exception ex)
            {
                _log.WriteLine($"Couldn't create shortcut.\nReason: {ex}");
            }
        }
        /// <summary>
        ///     Starts thcrap with the selected patch stack and executable
        /// </summary>
        private void StartThcrap()
        {
            string id;
            if (patchListBox.SelectedIndex == -1 || gameListBox.SelectedIndex == -1)
            {
                MessageBox.Show(I18N.LangResource.errors.noneSelected?.ToString(),
                                I18N.LangResource.errors.error?.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Process process;
            if (patchListBox.SelectedItem.ToString() == $@"[{I18N.LangResource.mainForm.vanilla.ToString()}]")
            {
                _displayNameToThxxDictionary.TryGetValue(gameListBox.SelectedItem.ToString().Replace("★", "").Trim(), out string s1);
                _gamesDictionary.TryGetValue(s1 ?? throw new InvalidOperationException(), out string game);
                String gameDirectory = game.Substring(0, game.LastIndexOf('/'));
                id = s1;
                if (game == null)
                {
                    ErrorAndExit(I18N.LangResource.errors.oops?.ToString());
                    return;
                }
                process = new Process { StartInfo = { FileName = game, WorkingDirectory = gameDirectory, RedirectStandardOutput = true, UseShellExecute = false } };
                process.OutputDataReceived += Process_OutputDataReceived;
                _log.WriteLine($"Game {game} started without thcrap.");
            }
            else
            {
                id = "thcrap";
                string s = "\"" + patchListBox.SelectedItem.ToString().Replace(" ★", "");
                if (Configuration1.HidePatchExtension && _jsFiles.Contains(s.Replace("\"", ""))) s += ".js";
                if (Configuration1.HidePatchExtension && _thcrapFiles.Contains(s.Replace("\"", "")))
                    s += ".thcrap";
                s += "\" ";
                _displayNameToThxxDictionary.TryGetValue(gameListBox.SelectedItem.ToString().Replace("★", "").Trim(), out string s1);
                s += s1;
                s = s.Trim();
                process = new Process { StartInfo = { FileName = "thcrap_loader.exe", Arguments = s, RedirectStandardOutput = true, UseShellExecute = false } };
                process.OutputDataReceived += Process_OutputDataReceived;
                _log.WriteLine($"Starting thcrap with {s}");
            }

            process.Start();
            if (Configuration1.ExitAfterStartup) Application.Exit();
            var progress = new Progress<Action>();
            progress.ProgressChanged += ExecuteReportAction;
            var tasks = new List<Task> { Task.Factory.StartNew(() => ScanRunningProcess(process, progress, id)) };
            _displayNameToThxxDictionary.TryGetValue(gameListBox.SelectedItem.ToString().Replace(" ★", ""), out string gameName);
            if (patchListBox.SelectedItem.ToString() != $@"[{I18N.LangResource.mainForm.vanilla.ToString()}]") tasks.Add(Task.Factory.StartNew(() => ScanRunningTouhou(gameName, progress)));
            Task.WaitAll(tasks.ToArray());
            Enabled = true;
        }
        /// <summary>
        ///     Added to <see cref="Progress{Action}" />. It executes the returned action as progress.
        ///     <para>"This is tad more thread-safe."</para>
        /// </summary>
        /// <param name="sender">The sender object, you know the usual event listener stuff</param>
        /// <param name="action">The action to invoke when the progress reported even gets called</param>
        private void ExecuteReportAction(object sender, Action action) => action.Invoke();
        /// <summary>
        ///     Starts thcrap but with a given argument s and scans for the given game.
        /// </summary>
        /// <param name="s">Arguments for thcrap_loader.exe</param>
        /// <param name="game">The game id (th???) that will be scanned</param>
        /// <returns></returns>
        private void StartThcrap(string s, string game)
        {
            _log.WriteLine($"Starting thcrap with {s} as arguments.");
            var process = new Process { StartInfo = { FileName = "thcrap_loader.exe", Arguments = s, RedirectStandardOutput = true, UseShellExecute = false } };
            process.OutputDataReceived += Process_OutputDataReceived;
            process.Start();
            if (Configuration1.ExitAfterStartup) Application.Exit();
            var progress = new Progress<Action>();
            progress.ProgressChanged += ExecuteReportAction;
            var tasks = new List<Task>
            {
                Task.Factory.StartNew(() => ScanRunningProcess(process, progress, "thcrap")),
                Task.Factory.StartNew(() => ScanRunningTouhou(game, progress))
            };
            //MessageBox.Show("");

            Task.WaitAll(tasks.ToArray());

            Enabled = true;
            Thread.Sleep(1000);
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }
        /// <summary>
        ///     Scans the process passed in on how it's doing, and reports to <see cref="Progress{Action}" /> to change the title
        ///     of the window.
        /// </summary>
        /// <param name="process">Process to scan</param>
        /// <param name="progress">The progress reporter</param>
        /// <param name="id">The game id (th???) or "thcrap"</param>
        private void ScanRunningProcess(Process process, IProgress<Action> progress, string id)
        {
            if (Configuration1.OnlyAllowOneExecutable) Enabled = false;
            string gameName = GetPrettyTouhouName(id);
            do
            {
                Thread.Sleep(10);
                process.Refresh();
                if (process.HasExited) return;
            } while (id != "thcrap" && !process.MainWindowTitle.Contains(gameName) || process.MainWindowTitle.Contains("vpatch") || process.MainWindowTitle == "");
            process.WaitForInputIdle();
            _log.WriteLine($"{process.ProcessName} is running.");
            progress.Report(() => { Text += $@" | {I18N.LangResource.mainForm?.running?.ToString()} {process.ProcessName}"; });
            process.WaitForExit();
            progress.Report(() => { Text = Text.Replace($@" | {I18N.LangResource.mainForm?.running?.ToString()} {process.ProcessName}", ""); });
        }
        /// <summary>
        ///     Scans the running game (if that's not set in as a process in which case
        ///     <see cref="ScanRunningProcess(Process, IProgress{Action}, string)" /> might be better.
        /// </summary>
        /// <param name="gameName">The game id (th???)</param>
        /// <param name="progress">
        ///     A <see cref="Progress{Action}" /> that it will report to change <see cref="Form.Text" /> to
        ///     include the title of the process.
        /// </param>
        private void ScanRunningTouhou(string gameName, IProgress<Action> progress)
        {
            if (gameName == null) throw new ArgumentNullException(nameof(gameName));
            if (Configuration1.OnlyAllowOneExecutable) Enabled = false;
            Process gameProcess = null;
            _gamesDictionary.TryGetValue(gameName, out string gameFile);
            string[] splitted = gameFile?.Split('/');
            if (splitted != null) gameFile = splitted[splitted.Length - 1].Split('.')[0];
            if (!(gameFile is null) && gameFile.Equals(@"vpatch")) gameFile = gameName;
            var sw = new Stopwatch();
            sw.Start();
            do
            {
                try
                {
                    gameProcess = Process.GetProcessesByName(gameFile)[0];
                    foreach (Process item in Process.GetProcessesByName(gameFile)) _log.WriteLine($"Game Found for {gameFile} with ID: " + item.Id);
                    if (Process.GetProcessesByName(gameFile).Length > 1) _log.WriteLine(@"Looks like you're running two of the same game somehow. You're magic, but I am going to assume the first game.");
                }
                catch { Thread.Sleep(10); }
                if (sw.Elapsed.CompareTo(new TimeSpan(0, 1, 0)) > 0)
                {
                    _log.WriteLine("Finding game timed out. Couldn't find: " + gameFile);
                    sw.Stop();
                    return;
                }
            } while (gameProcess == null || gameName == "th07" && gameProcess.MainWindowTitle == ""); // Touhou 7 is a bit buggy and needs to be rescanned.
            sw.Stop();
            _log.WriteLine("Found game, took: " + sw.Elapsed + " // Can't redirect output of the game that's launched through thcrap, use thcrap.");
            progress.Report(() => { Text += $@" | {I18N.LangResource.mainForm?.running?.ToString()} {gameName}"; });
            gameProcess.WaitForExit();
            progress.Report(() => { Text = Text.Replace($@" | {I18N.LangResource.mainForm?.running?.ToString()} {gameName}", ""); });
        }
        /// <summary>
        ///     Add the selected item in the given listbox to the favorites.
        /// </summary>
        /// <param name="lb">Listbox that the item is selected in</param>
        private void AddFavorite(ListBox lb)
        {
            if (!lb.SelectedItem.ToString().Contains("★"))
            {
                if (lb.Equals(patchListBox))
                {
                    var s = lb.Items[lb.SelectedIndex].ToString();
                    if (Configuration1.HidePatchExtension)
                    {
                        if (_jsFiles.Contains(s)) s += ".js";
                        if (_thcrapFiles.Contains(s)) s += ".thcrap";
                    }

                    if (s == $@"[{I18N.LangResource.mainForm.vanilla.ToString()}]") s = @"VANILLA";
                    Favourites1.Patches.Add(s);
                    PopulatePatchList();
                }

                if (lb.Equals(gameListBox))
                {
                    var s = lb.Items[lb.SelectedIndex].ToString();
                    _displayNameToThxxDictionary.TryGetValue(s, out s);
                    Favourites1.Games.Add(s);
                    PopulateGames();
                }
            }
            else
            {
                if (lb.Equals(gameListBox))
                {
                    string display = lb.SelectedItem.ToString().Replace("★", "").Trim();
                    _favoritesWithDisplayName.Remove(display);
                    _displayNameToThxxDictionary.TryGetValue(display, out string s);
                    Favourites1.Games.Remove(s);
                    PopulateGames();
                }

                if (!lb.Equals(patchListBox)) return;
                {
                    string s = lb.SelectedItem.ToString().Replace("★", "").Trim();
                    if (Configuration1.ShowVanilla && lb.SelectedIndex == 0) s = @"VANILLA";
                    Favourites1.Patches.Remove(s);
                    PopulatePatchList();
                }
            }
        }

        /// <summary>
        ///     Downloads a website into a string.
        ///     <para>Thank you, StackOverflow.</para>
        /// </summary>
        /// <param name="url">The URL of the website to download.</param>
        /// <returns></returns>
        private static string ReadTextFromUrl(string url)
        {
            string data;

            //Set Security
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true;

            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                data = client.DownloadString(url);
            }
            return data;

        }
        /// <summary>
        ///     Displays a <see cref="MessageBox" /> with <seealso cref="MessageBoxButtons.OK" />,
        ///     <seealso cref="MessageBoxIcon.Error" /> and
        ///     localized caption using <see cref="I18N.LangResource" />.
        /// </summary>
        /// <param name="errorMessage">
        ///     The message that should displayed in the <see cref="MessageBox" />. Should come from
        ///     <see cref="I18N.LangResource" />.
        /// </param>
        private void ErrorAndExit(dynamic errorMessage)
        {
            MessageBox.Show(text: errorMessage?.ToString(), caption: I18N.LangResource?.errors?.error?.ToString(),
                            buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            _log.WriteLine($"{errorMessage?.ToString()}");
            Application.Exit();
        }

        #endregion
    }

    #region Helper Classes

    public class Configuration
    {
        public bool ExitAfterStartup { get; set; }
        public string LastConfig { get; set; }
        public string LastGame { get; set; }
        public string[] IsDescending { get; set; }
        public string[] OnlyFavorites { get; set; }
        public byte FilterExeType { get; set; }
        public Window Window { get; set; }
        public static string Lang { get; set; }
        public bool HidePatchExtension { get; set; }
        public bool ShowVanilla { get; set; }
        public bool OnlyAllowOneExecutable { get; set; }
        public GameNameType NamingForGames { get; set; } = GameNameType.ShortName;
        public bool MinimizeNotificationWasShown { get; set; }
        public bool OnlyAllowOneUtl { get; set; }
        public FormWindowState WindowState { get; set; }
        public int SplitterDistance { get; set; }
        public bool ShowGameId { get; set; }
    }

    public enum GameNameType
    {
        None = 0, Initials, ShortName,
        LongName
    }

    public class Window
    {
        public int[] Location { get; set; } = { 0, 0 };
        public int[] Size { get; set; } = { 350, 500 };
    }

    public class Favourites
    {
        public Favourites(List<string> patches, List<string> games)
        {
            Patches = patches;
            Games = games;
        }

        public List<string> Patches { get; set; }
        public List<string> Games { get; set; }
    }

    public sealed class JumpListElement : ToolStripMenuItem
    {
        private readonly string _exec;
        private readonly string _patch;
        public JumpListElement(string exec, string patch)
        {
            _exec = exec;
            _patch = patch;
            Text = ToStringPretty();
        }

        public string ToStringPretty()
        {
            MainForm.GameFullNameDictionary.TryGetValue(_exec.Replace("_custom", ""), out string display);
            if (_exec.Contains("_custom")) display += " ~ " + I18N.LangResource.mainForm?.custom?.ToString();
            return $"{(!string.IsNullOrEmpty(display) ? display : _exec)} ({_patch})";
        }

        public override string ToString() => "\"" + _patch + "\" " + _exec;
    }

    #endregion
}
