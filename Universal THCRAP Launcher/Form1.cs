using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Newtonsoft.Json;
using Universal_THCRAP_Launcher.Properties;
using File = System.IO.File;

namespace Universal_THCRAP_Launcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static void ErrorAndExit(string errorMessage)
        {
            MessageBox.Show(errorMessage, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Configuration1 = new Configuration();
            //Give error if not next to thcrap_loader.exe
            const string msgError1 =
                "thcrap_loader.exe couldn't be found.\nMake sure you put the application next to it!";
            var fileExists = File.Exists("thcrap_loader.exe");
            if (!fileExists)
                ErrorAndExit(msgError1);

            //Give error if no games.js file
            const string msgError2 =
                "games.js couldn't be found.\nMake sure you run thcrap_configure.exe first!";
            if (!File.Exists("games.js")) ErrorAndExit(msgError2);

            DeleteOutdatedConfig();

            #region Load data from files

            //Load patch stacks
            _jsFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.js").ToList();

            //Give error if there are no patch configurations
            const string msgError3 =
                "No config files could be found.\nMake sure you run thcrap_configure.exe first!";
            if (_jsFiles.Count == 0) ErrorAndExit(msgError3);

            //Give error if Newtonsoft.Json.dll isn't found.
            const string msgError4 =
                @"Newtonsoft.Json.dll is missing. Please make sure you have copied it over as well.";
            if (!File.Exists("Newtonsoft.Json.dll")) ErrorAndExit(msgError4);

            //Load executables
            var file = File.ReadAllText("games.js");
            var games = JsonConvert.DeserializeObject<Dictionary<string, string>>(file);

            //Load config

            if (File.Exists(ConfigFile))
            {
                var settings = new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                };
                file = File.ReadAllText(ConfigFile);
                Configuration1 = JsonConvert.DeserializeObject<Configuration>(file, settings);
            }

            //Load favourites
            if (File.Exists("favourites.js"))
            {
                file = File.ReadAllText("favourites.js");
                Favourites1 = JsonConvert.DeserializeObject<Favourites>(file);
            }

            #endregion

            #region  Fix patch stack list

            for (var i = 0; i < _jsFiles.Count; i++)
                _jsFiles[i] = _jsFiles[i].Replace(Directory.GetCurrentDirectory() + "\\", "");
            _jsFiles.Remove("games.js");
            _jsFiles.Remove("config.js");
            _jsFiles.Remove("favourites.js");
            _jsFiles.Remove(ConfigFile);

            #endregion

            #region Set stuff

            //Create constants for resizing
            _resizeConstants = new int[7];
            _resizeConstants[0] = Size.Width - button1.Width;
            _resizeConstants[1] = Size.Width - splitContainer1.Width;
            _resizeConstants[2] = Size.Height - splitContainer1.Height;
            _resizeConstants[3] = Size.Height - checkBox1.Location.Y;
            _resizeConstants[4] = splitContainer1.Location.Y - sort_az_button1.Location.Y;
            _resizeConstants[5] = sort_az_button2.Location.X - listBox1.Size.Width;
            _resizeConstants[6] = star_button2.Location.X - sort_az_button2.Location.X;

            #endregion

            #region Display

            //Display patch stacks
            foreach (var item in _jsFiles)
                listBox1.Items.Add(item);
            //Display executables
            foreach (var item in games)
            {
                _gamesList.Add(item.Key);
                listBox2.Items.Add(item.Key);
            }

            SetDefaultSettings();

            //Change Form settings
            SetDesktopLocation(Configuration1.Window.Location[0], Configuration1.Window.Location[1]);
            Size = new Size(Configuration1.Window.Size[0], Configuration1.Window.Size[1]);

            checkBox1.Checked = Configuration1.ExitAfterStartup;

            //Update Display favourites
            AddStars(listBox1, Favourites1.Patches);
            AddStars(listBox2, Favourites1.Games);

            #endregion


            if (menuStrip1 == null) return;
            menuStrip1.Items.OfType<ToolStripMenuItem>().ToList().ForEach(x =>
                x.MouseHover += (obj, arg) => ((ToolStripDropDownItem) obj).ShowDropDown());

            Debug.WriteLine("Form1 Loaded");
        }

        private void SetDefaultSettings()
        {
            //Default Configuration setting
            if (Configuration1.LastGame == null) Configuration1.LastGame = _gamesList[0];
            if (Configuration1.LastConfig == null) Configuration1.LastConfig = _jsFiles[0];
            if (Configuration1.IsDescending == null)
            {
                string[] a = {"false", "false"};
                Configuration1.IsDescending = a;
            }

            if (Configuration1.OnlyFavourites == null)
            {
                string[] a = {"false", "false"};
                Configuration1.OnlyFavourites = a;
            }

            if (Configuration1.Window == null)
            {
                var window = new Window
                    {Size = new[] {Size.Width, Size.Height}, Location = new[] {Location.X, Location.Y}};
                Configuration1.Window = window;
            }


            //Default sort
            for (var i = 0; i < 2; i++)
                if (Configuration1.IsDescending[i] == "false")
                {
                    if (i == 0)
                    {
                        SortListBoxItems(ref listBox1);
                        sort_az_button1.BackgroundImage = _sortAscending;
                    }
                    else
                    {
                        SortListBoxItems(ref listBox2);
                        sort_az_button2.BackgroundImage = _sortAscending;
                    }
                }
                else if (i == 0)
                {
                    SortListBoxItemsDesc(ref listBox1);
                    sort_az_button1.BackgroundImage = _sortDescending;
                }
                else
                {
                    SortListBoxItemsDesc(ref listBox2);
                    sort_az_button2.BackgroundImage = _sortDescending;
                }

            //Default favourite button state
            for (var i = 0; i < 2; i++)
                if (Configuration1.OnlyFavourites[i] == "true")
                {
                    if (i == 0)
                    {
                        star_button1.BackgroundImage = _star;
                        for (var n = listBox1.Items.Count - 1; n >= 0; --n)
                        {
                            const string filterItem = "★";
                            if (!listBox1.Items[n].ToString().Contains(filterItem))
                                listBox1.Items.RemoveAt(n);
                        }
                    }
                    else
                    {
                        star_button2.BackgroundImage = _star;
                        for (var n = listBox2.Items.Count - 1; n >= 0; --n)
                        {
                            const string filterItem = "★";
                            if (!listBox2.Items[n].ToString().Contains(filterItem))
                                listBox2.Items.RemoveAt(n);
                        }
                    }
                }
                else
                {
                    if (i == 0) star_button1.BackgroundImage = _starHollow;
                    else star_button2.BackgroundImage = _starHollow;
                }

            //Default exe type button state
            filterByType_button.BackgroundImage = _gameAndCustom;
            for (var i = 0; i < Configuration1.FilterExeType; i++) filterByType_button_Click(null, new EventArgs());
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            ReadConfig();
            //Set default selection index
            if (listBox1.SelectedIndex == -1 && listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;

            if (listBox2.SelectedIndex == -1 && listBox2.Items.Count > 0)
                listBox2.SelectedIndex = 0;

            UpdateConfigFile();
        }

        private static void DeleteOutdatedConfig()
        {
            if (File.Exists("uthcrapl_config.js")) File.Delete("uthcrapl_config.js");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            button1.Size = new Size(Size.Width - _resizeConstants[0], button1.Size.Height);
            splitContainer1.Size = new Size(Size.Width - _resizeConstants[1], Size.Height - _resizeConstants[2]);
            listBox1.Size = new Size(splitContainer1.Panel1.Width - 1, splitContainer1.Panel1.Height - 1);
            listBox2.Size = new Size(splitContainer1.Panel2.Width - 1, splitContainer1.Panel2.Height - 1);
            checkBox1.Location = new Point(checkBox1.Location.X, Size.Height - _resizeConstants[3]);
            sort_az_button1.Location =
                new Point(sort_az_button1.Location.X, splitContainer1.Location.Y - _resizeConstants[4]);
            sort_az_button2.Location = new Point(listBox1.Size.Width + _resizeConstants[5],
                splitContainer1.Location.Y - _resizeConstants[4]);
            star_button1.Location =
                new Point(star_button1.Location.X, splitContainer1.Location.Y - _resizeConstants[4]);
            star_button2.Location = new Point(sort_az_button2.Location.X + _resizeConstants[6],
                splitContainer1.Location.Y - _resizeConstants[4]);
            filterByType_button.Location = new Point(
                star_button2.Location.X + _resizeConstants[6], splitContainer1.Location.Y - _resizeConstants[4]);
        }

        private static void AddStars(ListBox listBox, IEnumerable<string> list)
        {
            foreach (var variable in list)
            {
                var index = listBox.FindStringExact(variable);
                if (index != -1) listBox.Items[index] += " ★";
            }
        }

        private static void SortListBoxItems(ref ListBox lb)
        {
            var items = lb.Items.OfType<object>().ToList();
            lb.Items.Clear();
            lb.Items.AddRange(items.OrderBy(i => i).ToArray());
        }

        private static void SortListBoxItemsDesc(ref ListBox lb)
        {
            var items = lb.Items.OfType<object>().ToList();
            lb.Items.Clear();
            lb.Items.AddRange(items.OrderByDescending(i => i).ToArray());
        }

        /// <summary>
        ///     Selects the items based on the configuration
        /// </summary>
        private void ReadConfig()
        {
            checkBox1.Checked = Configuration1.ExitAfterStartup;

            var s = Configuration1.LastConfig;
            if (Favourites1.Patches.Contains(s))
                s += " ★";
            listBox1.SelectedIndex = listBox1.FindStringExact(s);
            s = Configuration1.LastGame;

            if (Favourites1.Games.Contains(s))
                s += " ★";

            listBox2.SelectedIndex = listBox2.FindStringExact(s);

            if (listBox1.SelectedIndex == -1 && listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;
            if (listBox2.SelectedIndex == -1 && listBox2.Items.Count > 0)
                listBox2.SelectedIndex = 0;
        }

        /// <summary>
        ///     Updates the configuration and favourites list
        /// </summary>
        private void UpdateConfig()
        {
            if (listBox1.SelectedIndex == -1 && listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;
            if (listBox1.SelectedIndex != -1)
                Configuration1.LastConfig = ((string) listBox1.SelectedItem).Replace(" ★", "");
            if (listBox2.SelectedIndex == -1 && listBox2.Items.Count > 0)
                listBox2.SelectedIndex = 0;
            if (listBox2.SelectedIndex != -1)
                Configuration1.LastGame = ((string) listBox2.SelectedItem).Replace(" ★", "");

            var window = new Window {Size = new[] {Size.Width, Size.Height}, Location = new[] {Location.X, Location.Y}};
            Configuration1.Window = window;

            Favourites1.Patches.Clear();
            Favourites1.Games.Clear();

            foreach (string s in listBox1.Items)
                if (s.Contains("★"))
                {
                    var v = s.Replace(" ★", "");
                    Favourites1.Patches.Add(v);
                }

            foreach (string s in listBox2.Items)
                if (s.Contains("★"))
                {
                    var v = s.Replace(" ★", "");
                    Favourites1.Games.Add(v);
                }
        }

        /// <summary>
        ///     Writes the configuration and favourites to file
        /// </summary>
        private void UpdateConfigFile()
        {
            UpdateConfig();
            var output = JsonConvert.SerializeObject(Configuration1, Formatting.Indented, new JsonSerializerSettings());
            File.WriteAllText(ConfigFile, output);

            output = JsonConvert.SerializeObject(Favourites1, Formatting.Indented);
            File.WriteAllText("favourites.js", output);

            Debug.WriteLine("Config file Updated!");
        }

        /// <summary>
        ///     Starts thcrap with the selected patch stack and executable
        /// </summary>
        private void StartThcrap()
        {
            if (listBox1.SelectedIndex == -1 || listBox2.SelectedIndex == -1)
            {
                const string error =
                    "No run configuration (patch stack) or game (executable) selected!\nPlease select one!";
                MessageBox.Show(error, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var s = "";
            s += listBox1.SelectedItem;
            s += " ";
            s += listBox2.SelectedItem;
            s = s.Replace(" ★", "");
            //MessageBox.Show(args);
            var process = new Process {StartInfo = {FileName = "thcrap_loader.exe", Arguments = s}};
            process.Start();
            Debug.WriteLine("Starting thcrap with {0}", s);
            if (checkBox1.Checked)
                Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e) => StartThcrap();

        private void button1_MouseHover(object sender, EventArgs e) =>
            button1.BackgroundImage = Resources.Shinmera_Banner_5_mini_size_hover;

        private void button1_MouseLeave(object sender, EventArgs e) =>
            button1.BackgroundImage = Resources.Shinmera_Banner_5_mini_size;

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModifierKeys != Keys.None) return;
            var lb = (ListBox) sender;
            switch (lb.Name)
            {
                case "listBox1":
                    if (lb.SelectedIndex != -1)
                        Configuration1.LastConfig = lb.SelectedItem.ToString().Replace(" ★", "");
                    break;
                case "listBox2":
                    if (lb.SelectedIndex != -1)
                        Configuration1.LastGame = lb.SelectedItem.ToString().Replace(" ★", "");
                    break;
                default:
                    Debug.WriteLine("Invalid ListBox!");
                    break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) =>
            Configuration1.ExitAfterStartup = checkBox1.Checked;

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (ModifierKeys != Keys.None)
            {
                listBox1.SelectedItem = Configuration1.LastConfig;
                listBox2.SelectedItem = Configuration1.LastGame;
            }

            switch (e.KeyCode)
            {
                case Keys.F2 when sender.GetType().FullName != "System.Windows.Forms.ListBox":
                case Keys.Enter when sender.GetType().FullName != "System.Windows.Forms.ListBox":
                    return;
                case Keys.Enter:
                    UpdateConfigFile();
                    StartThcrap();
                    break;
                case Keys.F2:
                    var lb = (ListBox) sender;
                    if (!lb.SelectedItem.ToString().Contains("★"))
                    {
                        if (lb.Equals(listBox1))
                            Favourites1.Patches.Add(lb.Items[lb.SelectedIndex].ToString());

                        if (lb.Equals(listBox2))
                            Favourites1.Games.Add(lb.Items[lb.SelectedIndex].ToString());
                        lb.Items[lb.SelectedIndex] += " ★";
                    }
                    else
                    {
                        lb.Items[lb.SelectedIndex] = lb.Items[lb.SelectedIndex].ToString().Replace(" ★", "");
                    }

                    UpdateConfigFile();
                    break;
            }
        }

        private static void RestartProgram()
        {
            Process.Start(Assembly.GetEntryAssembly().Location);
            Application.Exit();
        }

        private static void ShowKeyboardShortcuts()
        {
            MessageBox.Show(Resources.KeyboardShortcuts,
                @"Keyboard Shortcuts", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) => UpdateConfigFile();

        #region Global variables

        private const string ConfigFile = "utl_config.js";
        private readonly Image _custom = new Bitmap(Resources.Custom);
        private readonly Image _game = new Bitmap(Resources.Game);

        private readonly Image _gameAndCustom = new Bitmap(Resources.GameAndCustom);
        private readonly List<string> _gamesList = new List<string>();

        private readonly Image _sortAscending = new Bitmap(Resources.Sort_Ascending);
        private readonly Image _sortDescending = new Bitmap(Resources.Sort_Decending);

        private readonly Image _star = new Bitmap(Resources.Star);
        private readonly Image _starHollow = new Bitmap(Resources.Star_Hollow);

        private List<string> _jsFiles = new List<string>();

        private int[] _resizeConstants;

        private Configuration Configuration1 { get; set; }
        private Favourites Favourites1 { get; set; } = new Favourites(new List<string>(), new List<string>());

        #endregion

        #region Sorting/Filtering Button functions

        private void sort_az_button1_Click(object sender, EventArgs e)
        {
            var isDesc = Configuration1.IsDescending;
            if (sort_az_button1.BackgroundImage.Equals(_sortDescending))
            {
                SortListBoxItems(ref listBox1);
                sort_az_button1.BackgroundImage = _sortAscending;
                isDesc[0] = "false";
            }
            else
            {
                SortListBoxItemsDesc(ref listBox1);
                isDesc[0] = "true";
                sort_az_button1.BackgroundImage = _sortDescending;
            }

            Configuration1.IsDescending = isDesc;
            ReadConfig();
        }

        private void sort_az_button2_Click(object sender, EventArgs e)
        {
            var isDesc = Configuration1.IsDescending;
            if (sort_az_button2.BackgroundImage.Equals(_sortDescending))
            {
                SortListBoxItems(ref listBox2);
                sort_az_button2.BackgroundImage = _sortAscending;
                isDesc[1] = "false";
            }
            else
            {
                SortListBoxItemsDesc(ref listBox2);
                isDesc[1] = "true";
                sort_az_button2.BackgroundImage = _sortDescending;
            }

            Configuration1.IsDescending = isDesc;
            ReadConfig();
        }

        private void star_button1_Click(object sender, EventArgs e)
        {
            var onlyFav = Configuration1.OnlyFavourites;
            if (!star_button1.BackgroundImage.Equals(_star))
            {
                star_button1.BackgroundImage = _star;
                for (var n = listBox1.Items.Count - 1; n >= 0; --n)
                {
                    const char filterItem = '★';
                    if (!listBox1.Items[n].ToString().Contains(filterItem))
                        listBox1.Items.RemoveAt(n);
                }

                onlyFav[0] = "true";
            }
            else
            {
                star_button1.BackgroundImage = _starHollow;
                listBox1.Items.Clear();
                foreach (var s in _jsFiles) listBox1.Items.Add(s);

                AddStars(listBox1, Favourites1.Patches);
                onlyFav[0] = "false";
            }

            Configuration1.OnlyFavourites = onlyFav;
            ReadConfig();
        }

        private void star_button2_Click(object sender, EventArgs e)
        {
            var onlyFav = Configuration1.OnlyFavourites;
            if (!star_button2.BackgroundImage.Equals(_star))
            {
                star_button2.BackgroundImage = _star;
                for (var n = listBox2.Items.Count - 1; n >= 0; --n)
                {
                    const string filterItem = "★";
                    if (!listBox2.Items[n].ToString().Contains(filterItem))
                        listBox2.Items.RemoveAt(n);
                }

                onlyFav[1] = "true";
            }
            else
            {
                star_button2.BackgroundImage = _starHollow;
                listBox2.Items.Clear();
                foreach (var s in _gamesList) listBox2.Items.Add(s);

                AddStars(listBox2, Favourites1.Games);
                onlyFav[1] = "false";
            }

            Configuration1.OnlyFavourites = onlyFav;
            ReadConfig();
        }

        private void filterByType_button_Click(object sender, EventArgs e)
        {
            if (filterByType_button.BackgroundImage.Equals(_gameAndCustom))
            {
                filterByType_button.BackgroundImage = _game;
                listBox2.Items.Clear();
                foreach (var item in _gamesList)
                    if (!item.Contains("_custom"))
                        listBox2.Items.Add(item);
                AddStars(listBox2, Favourites1.Games);
                if (sender != null) Configuration1.FilterExeType = 1;
                return;
            }

            if (filterByType_button.BackgroundImage.Equals(_game))
            {
                filterByType_button.BackgroundImage = _custom;
                listBox2.Items.Clear();
                foreach (var item in _gamesList)
                    if (item.Contains("_custom"))
                        listBox2.Items.Add(item);
                AddStars(listBox2, Favourites1.Games);
                if (sender != null) Configuration1.FilterExeType = 2;
                return;
            }

            if (!filterByType_button.BackgroundImage.Equals(_custom)) return;
            {
                filterByType_button.BackgroundImage = _gameAndCustom;
                listBox2.Items.Clear();
                foreach (var item in _gamesList) listBox2.Items.Add(item);
                AddStars(listBox2, Favourites1.Games);
                if (sender != null) Configuration1.FilterExeType = 0;
            }
        }

        #endregion

        #region Tool Strip functions

        private void keyboardShortcutsToolStripMenuItem_Click(object sender, EventArgs e) => ShowKeyboardShortcuts();

        private void restartToolStripMenuItem_Click(object sender, EventArgs e) => RestartProgram();

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void releasesToolStripMenuItem_Click(object sender, EventArgs e) =>
            Process.Start("https://github.com/Tudi20/Universal-THCRAP-Launcher/releases");

        private void reportBugToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start(
            "https://github.com/Tudi20/Universal-THCRAP-Launcher/issues/" +
            "new?assignees=&labels=bug&template=bug_report.md&title=%5BBUG%5D");

        private void requestAFeatureToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start(
            "https://github.com/Tudi20/Universal-THCRAP-Launcher/issues/" +
            "new?assignees=&labels=enhancement&template=feature_request.md&title=%5BFEATURE%5D");

        private void requestToolStripMenuItem_Click(object sender, EventArgs e) =>
            Process.Start("https://github.com/Tudi20/Universal-THCRAP-Launcher/issues/new");

        private void gitHubToolStripMenuItem_Click(object sender, EventArgs e) =>
            Process.Start("https://github.com/Tudi20/Universal-THCRAP-Launcher");

        private void openTHCRAPConfigureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"The Launcher will disappear until you're configuring...",
                @"Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var p = Process.Start("thcrap_configure.exe");
            if (p == null)
            {
                MessageBox.Show(@"Something went wrong...", @"Error");
                return;
            }

            Hide();
            while (!p.HasExited) Thread.Sleep(1);
            Show();
        }

        private void openGamesListToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start("games.js");

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e) =>
            Process.Start(Directory.GetCurrentDirectory());

        private void createShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var shDesktop = (object) "Desktop";
            var shell = new WshShell();
            var shortcutAddress = (string) shell.SpecialFolders.Item(ref shDesktop) + @"\Universal THCRAP Launcher.lnk";
            var shortcut = (IWshShortcut) shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "Shortcut for UTL";
            shortcut.TargetPath = Assembly.GetEntryAssembly().Location;
            shortcut.WorkingDirectory = Directory.GetCurrentDirectory();
            shortcut.Save();
        }

        private void openSelectedPatchConfigurationToolStripMenuItem_Click(object sender, EventArgs e) =>
            Process.Start(Directory.GetCurrentDirectory() + @"/" + listBox1.SelectedItem.ToString().Replace(" ★", ""));

        #endregion
    }

    public class Configuration
    {
        public bool ExitAfterStartup { get; set; }
        public string LastConfig { get; set; }
        public string LastGame { get; set; }
        public string[] IsDescending { get; set; }
        public string[] OnlyFavourites { get; set; }
        public byte FilterExeType { get; set; }
        public Window Window { get; set; }
    }

    public class Window
    {
        public int[] Location { get; set; } = {0, 0};
        public int[] Size { get; set; } = {350, 500};
    }

    public class Favourites
    {
        public Favourites(List<string> patches, List<string> games)
        {
            Patches = patches;
            Games = games;
        }

        public List<string> Patches { get; }
        public List<string> Games { get; }
    }
}