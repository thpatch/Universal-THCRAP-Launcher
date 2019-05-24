using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Universal_THCRAP_Launcher.Properties;
using File = System.IO.File;
using System.Net;

namespace Universal_THCRAP_Launcher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private static void ErrorAndExit(string errorMessage)
        {
            MessageBox.Show(text: errorMessage, caption: I18N.LangResource.errors.error, buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            Trace.WriteLine($"[{DateTime.Now.ToShortTimeString()}] {errorMessage}");
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Configuration1 = new Configuration();
            dynamic dconfig = null;

            //Load config
            if (File.Exists(ConfigFile))
            {
                var settings = new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                };
                var raw = File.ReadAllText(ConfigFile);
                Configuration1 = JsonConvert.DeserializeObject<Configuration>(raw, settings);
                dconfig = JsonConvert.DeserializeObject(raw, settings);
            }
            SetDefaultSettings();

            if (I18N.LangNumber() == 0)
            {
                string lang = ReadTextFromUrl("https://raw.githubusercontent.com/Tudi20/Universal-THCRAP-Launcher/i18n/langs/en.json");
                File.WriteAllText(I18N.I18NDir + @"\en.json", lang);
            }
            
            //Give error if Newtonsoft.Json.dll isn't found.
            if (!File.Exists("Newtonsoft.Json.dll"))
            {
                //Read parser-less, the error message.
                if (Configuration.Lang == null) Configuration.Lang = "en.json";
                var lines = File.ReadAllLines(I18N.I18NDir + Configuration.Lang);
                foreach (var item in lines)
                {
                    string error = "Error";
                    if (item.Contains("\"error\"")) error = item.Split('"')[3];
                    if (item.Contains("\"jsonParser\""))
                    {
                        MessageBox.Show(item.Split('"')[3], error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
                }
            }

            //Load language
            Configuration.Lang = dconfig?.Lang;
            if (Configuration.Lang == null) Configuration.Lang = "en.json";
            I18N.GetLangResource(I18N.I18NDir + Configuration.Lang);

            //Give error if not next to thcrap_loader.exe
            var fileExists = File.Exists("thcrap_loader.exe");
            if (!fileExists)
                ErrorAndExit(I18N.LangResource.errors.missing.thcrap_loader);

            //Give error if no games.js file
            if (!File.Exists("games.js")) ErrorAndExit(I18N.LangResource.errors.missing.gamesJs);

            DeleteOutdatedConfig();

            #region Load data from files

            //Load patch stacks
            _jsFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.js").ToList();

            //Give error if there are no patch configurations
            if (_jsFiles.Count == 0) ErrorAndExit(I18N.LangResource.errors.missing.patchStacks);

            //Load executables
            var file = File.ReadAllText("games.js");
            var games = JsonConvert.DeserializeObject<Dictionary<string, string>>(file);

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

            #region _resizeConstants setter

            //Create constants for resizing
            _resizeConstants = new int[7];
            _resizeConstants[0] = Size.Width - startButton.Width;
            _resizeConstants[1] = Size.Width - splitContainer1.Width;
            _resizeConstants[2] = Size.Height - splitContainer1.Height;
            _resizeConstants[3] = splitContainer1.Location.Y - sortAZButton1.Location.Y;
            _resizeConstants[4] = sortAZButton2.Location.X - patchListBox.Size.Width;
            _resizeConstants[5] = filterFavButton2.Location.X - sortAZButton2.Location.X;

            #endregion

            #region Display

            //Display patch stacks
            foreach (var item in _jsFiles)
                patchListBox.Items.Add(item);
            //Display executables
            foreach (var item in games)
            {
                _gamesList.Add(item.Key);
                gameListBox.Items.Add(item.Key);
            }

            //Change Form settings
            SetDesktopLocation(Configuration1.Window.Location[0], Configuration1.Window.Location[1]);
            Size = new Size(Configuration1.Window.Size[0], Configuration1.Window.Size[1]);

            //Update Display favourites
            AddStars(patchListBox, Favourites1.Patches);
            AddStars(gameListBox, Favourites1.Games);

            #endregion


            if (menuStrip1 == null) return;
            menuStrip1.Items.OfType<ToolStripMenuItem>().ToList().ForEach(x =>
                x.MouseHover += (obj, arg) => ((ToolStripDropDownItem) obj).ShowDropDown());

            string newlang = ReadTextFromUrl("https://raw.githubusercontent.com/Tudi20/Universal-THCRAP-Launcher/i18n/langs/" + Configuration.Lang);
            File.WriteAllText(I18N.I18NDir + @"\en.json", newlang);
            

            UpdateLanguage();

            Trace.WriteLine($"[{DateTime.Now}] Form1 Loaded");
        }

        private void UpdateLanguage()
        {
            var objLangRes = I18N.LangResource.mainForm;

            Text = objLangRes.utl;
            toolTip1.SetToolTip(startButton, objLangRes.tooltips.startButton.ToString());
            toolTip1.SetToolTip(sortAZButton1, objLangRes.tooltips.sortAZ.ToString());
            toolTip1.SetToolTip(sortAZButton2, objLangRes.tooltips.sortAZ.ToString());
            toolTip1.SetToolTip(filterFavButton1, objLangRes.tooltips.filterFav.ToString());
            toolTip1.SetToolTip(filterFavButton2, objLangRes.tooltips.filterFav.ToString());
            toolTip1.SetToolTip(filterByType_button, objLangRes.tooltips.filterByType.ToString());
            toolTip1.SetToolTip(patchListBox, objLangRes.tooltips.patchLB.ToString());
            toolTip1.SetToolTip(gameListBox, objLangRes.tooltips.gameLB.ToString());

            // - TODO: Refactor this code
            menuStrip1.Items[0].Text = objLangRes.menuStrip[0][0];
            for (var i = 0; i < ((ToolStripMenuItem) menuStrip1.Items[0]).DropDownItems.Count; i++)
            {
                ((ToolStripMenuItem) menuStrip1.Items[0]).DropDownItems[i].Text = objLangRes.menuStrip[0][i + 1];
            }
            menuStrip1.Items[1].Text = objLangRes.menuStrip[1][0];
            for (var i = 0; i < ((ToolStripMenuItem) menuStrip1.Items[1]).DropDownItems.Count; i++)
            {
                if (objLangRes.menuStrip[1][i + 1] is JValue)
                {
                    ((ToolStripMenuItem) menuStrip1.Items[1]).DropDownItems[i].Text = objLangRes.menuStrip[1][i + 1];
                    
                }
                if (objLangRes.menuStrip[1][i + 1] is JArray)
                {
                    ((ToolStripMenuItem) ((ToolStripMenuItem) menuStrip1.Items[1]).DropDownItems[i]).Text =
                        objLangRes.menuStrip[1][i + 1][0];
                    for (var j = 0;
                        j < ((ToolStripMenuItem) ((ToolStripMenuItem) menuStrip1.Items[1]).DropDownItems[i])
                        .DropDownItems.Count;
                        j++)
                    {
                        ((ToolStripMenuItem) ((ToolStripMenuItem) menuStrip1.Items[1]).DropDownItems[i])
                            .DropDownItems[j].Text = objLangRes.menuStrip[1][i + 1][j + 1];
                    }
                }
            }
            menuStrip1.Items[2].Text = objLangRes.menuStrip[2][0];
            for (var i = 0; i < ((ToolStripMenuItem) menuStrip1.Items[2]).DropDownItems.Count; i++)
            {
                if (objLangRes.menuStrip[2][i + 1] is JValue)
                {
                    ((ToolStripMenuItem) menuStrip1.Items[2]).DropDownItems[i].Text = objLangRes.menuStrip[2][i + 1];
                    
                }
                if (objLangRes.menuStrip[2][i + 1] is JArray)
                {
                    ((ToolStripMenuItem) ((ToolStripMenuItem) menuStrip1.Items[2]).DropDownItems[i]).Text =
                        objLangRes.menuStrip[2][i + 1][0];
                    for (var j = 0;
                        j < ((ToolStripMenuItem) ((ToolStripMenuItem) menuStrip1.Items[2]).DropDownItems[i])
                        .DropDownItems.Count;
                        j++)
                    {
                        ((ToolStripMenuItem) ((ToolStripMenuItem) menuStrip1.Items[2]).DropDownItems[i])
                            .DropDownItems[j].Text = objLangRes.menuStrip[2][i + 1][j + 1];
                    }
                }
            }
            menuStrip1.Items[3].Text = objLangRes.menuStrip[3][0];
            for (var i = 0; i < ((ToolStripMenuItem) menuStrip1.Items[3]).DropDownItems.Count; i++)
            {
                ((ToolStripMenuItem) menuStrip1.Items[3]).DropDownItems[i].Text = objLangRes.menuStrip[3][i + 1];
            }
            // TODO END
        }

        

        private void SetDefaultSettings()
        {
            Trace.WriteLine($"[{DateTime.Now.ToShortTimeString()}] Entered SetDefaultSettings()");
            //Default Configuration setting
            if (Configuration1 == null)
            {
                Configuration1 = new Configuration();
                Trace.WriteLine($"[{DateTime.Now.ToShortTimeString()}] Configuration1 was null. Reinitializing it.");
            }

            if (Configuration.Lang == null)
            {
                Configuration.Lang = "en.json";
                Trace.WriteLine($"[{DateTime.Now.ToShortTimeString()}] Configuration.Lang has been set to {Configuration.Lang}");
            }

            if (Configuration1.LastGame == null)
            {
                Configuration1.LastGame = _gamesList[0];
                Trace.WriteLine(
                    $"[{DateTime.Now.ToShortTimeString()}] Configuration1.LastGame has been set to {Configuration1.LastGame}");
            }

            if (Configuration1.LastConfig == null)
            {
                Configuration1.LastConfig = _jsFiles[0];
                Trace.WriteLine(
                    $"[{DateTime.Now.ToShortTimeString()}] Configuration1.LastConfig has been set to {Configuration1.LastConfig}");
            }

            if (Configuration1.IsDescending == null)
            {
                string[] a = {"false", "false"};
                Configuration1.IsDescending = a;
                Trace.WriteLine(
                    $"[{DateTime.Now.ToShortTimeString()}] Configuration1.IsDescending has been set to {Configuration1.IsDescending[0]}, " +
                    Configuration1.IsDescending[1]);
            }

            if (Configuration1.OnlyFavourites == null)
            {
                string[] a = {"false", "false"};
                Configuration1.OnlyFavourites = a;
                Trace.WriteLine(
                    $"[{DateTime.Now.ToShortTimeString()}] Configuration1.OnlyFavourites has been set to {Configuration1.OnlyFavourites[0]}, " +
                    Configuration1.OnlyFavourites[1]);
            }

            if (Configuration1.Window == null)
            {
                var window = new Window
                    {Size = new[] {Size.Width, Size.Height}, Location = new[] {Location.X, Location.Y}};
                Configuration1.Window = window;
                Trace.WriteLine(
                    $"[{DateTime.Now.ToShortTimeString()}] Configuration1.Window has been set with the following properties:");
                Trace.WriteLine(
                    $"[{DateTime.Now.ToShortTimeString()}] Configuration1.Window.Size: {Configuration1.Window.Size[0]}, {Configuration1.Window.Size[1]}");
                Trace.WriteLine(
                    $"[{DateTime.Now.ToShortTimeString()}] Configuration1.Window.Location: {Configuration1.Window.Location[0]}, {Configuration1.Window.Location[1]}");
            }


            //Default sort
            for (var i = 0; i < 2; i++)
                if (Configuration1.IsDescending[i] == "false")
                {
                    Trace.WriteLine(
                        $"[{DateTime.Now.ToShortTimeString()}] Configuration1.IsDescending was false for listBox{i}");
                    if (i == 0)
                    {
                        SortListBoxItems(ref patchListBox);
                        sortAZButton1.BackgroundImage = _sortAscending;
                    }
                    else
                    {
                        SortListBoxItems(ref gameListBox);
                        sortAZButton2.BackgroundImage = _sortAscending;
                    }
                }
                else if (i == 0)
                {
                    Trace.WriteLine(
                        $"[{DateTime.Now.ToShortTimeString()}] Configuration1.IsDescending was true for listBox{i}");
                    SortListBoxItemsDesc(ref patchListBox);
                    sortAZButton1.BackgroundImage = _sortDescending;
                }
                else
                {
                    SortListBoxItemsDesc(ref gameListBox);
                    sortAZButton2.BackgroundImage = _sortDescending;
                }

            //Default favourite button state
            for (var i = 0; i < 2; i++)
                if (Configuration1.OnlyFavourites[i] == "true")
                {
                    Trace.WriteLine(
                        $"[{DateTime.Now.ToShortTimeString()}] Configuration1.OnlyFavourites was true for listBox{i}");
                    if (i == 0)
                    {
                        filterFavButton1.BackgroundImage = _star;
                        for (var n = patchListBox.Items.Count - 1; n >= 0; --n)
                        {
                            const string filterItem = "★";
                            if (!patchListBox.Items[n].ToString().Contains(filterItem))
                                patchListBox.Items.RemoveAt(n);
                        }
                    }
                    else
                    {
                        filterFavButton2.BackgroundImage = _star;
                        for (var n = gameListBox.Items.Count - 1; n >= 0; --n)
                        {
                            const string filterItem = "★";
                            if (!gameListBox.Items[n].ToString().Contains(filterItem))
                                gameListBox.Items.RemoveAt(n);
                        }
                    }
                }
                else
                {
                    Trace.WriteLine(
                        $"[{DateTime.Now.ToShortTimeString()}] Configuration1.OnlyFavourites was false for listBox{i}");
                    if (i == 0) filterFavButton1.BackgroundImage = _starHollow;
                    else filterFavButton2.BackgroundImage = _starHollow;
                }

            //Default exe type button state
            filterByType_button.BackgroundImage = _gameAndCustom;
            for (var i = 0; i < Configuration1.FilterExeType; i++)
            {
                Trace.WriteLine($"[{DateTime.Now.ToShortTimeString()}] Configuration1.FilterExeType");
                filterByType_button_Click("DefaultSettings", new EventArgs());
            }

            Trace.WriteLine($"[{DateTime.Now.ToShortTimeString()}] Exited SetDefaultSettings()");
        }
        
        private void Form1_Shown(object sender, EventArgs e)
        {
            ReadConfig();
            //Set default selection index
            if (patchListBox.SelectedIndex == -1 && patchListBox.Items.Count > 0)
                patchListBox.SelectedIndex = 0;

            if (gameListBox.SelectedIndex == -1 && gameListBox.Items.Count > 0)
                gameListBox.SelectedIndex = 0;

            UpdateConfigFile();
        }

        private static void DeleteOutdatedConfig()
        {
            if (File.Exists("uthcrapl_config.js")) File.Delete("uthcrapl_config.js");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            startButton.Size = new Size(Size.Width - _resizeConstants[0], startButton.Size.Height);
            splitContainer1.Size = new Size(Size.Width - _resizeConstants[1], Size.Height - _resizeConstants[2]);
            patchListBox.Size = new Size(splitContainer1.Panel1.Width - 1, splitContainer1.Panel1.Height - 1);
            gameListBox.Size = new Size(splitContainer1.Panel2.Width - 1, splitContainer1.Panel2.Height - 1);
            sortAZButton1.Location =
                new Point(sortAZButton1.Location.X, splitContainer1.Location.Y - _resizeConstants[3]);
            sortAZButton2.Location = new Point(patchListBox.Size.Width + _resizeConstants[4],
                splitContainer1.Location.Y - _resizeConstants[3]);
            filterFavButton1.Location =
                new Point(filterFavButton1.Location.X, splitContainer1.Location.Y - _resizeConstants[3]);
            filterFavButton2.Location = new Point(sortAZButton2.Location.X + _resizeConstants[5],
                splitContainer1.Location.Y - _resizeConstants[3]);
            filterByType_button.Location = new Point(
                filterFavButton2.Location.X + _resizeConstants[5], splitContainer1.Location.Y - _resizeConstants[3]);
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

            var s = Configuration1.LastConfig;
            if (Favourites1.Patches.Contains(s))
                s += " ★";
            patchListBox.SelectedIndex = patchListBox.FindStringExact(s);
            s = Configuration1.LastGame;

            if (Favourites1.Games.Contains(s))
                s += " ★";

            gameListBox.SelectedIndex = gameListBox.FindStringExact(s);

            if (patchListBox.SelectedIndex == -1 && patchListBox.Items.Count > 0)
                patchListBox.SelectedIndex = 0;
            if (gameListBox.SelectedIndex == -1 && gameListBox.Items.Count > 0)
                gameListBox.SelectedIndex = 0;
        }

        /// <summary>
        ///     Updates the configuration and favourites list
        /// </summary>
        private void UpdateConfig()
        {
            if (patchListBox.SelectedIndex == -1 && patchListBox.Items.Count > 0)
                patchListBox.SelectedIndex = 0;
            if (patchListBox.SelectedIndex != -1)
                Configuration1.LastConfig = ((string) patchListBox.SelectedItem).Replace(" ★", "");
            if (gameListBox.SelectedIndex == -1 && gameListBox.Items.Count > 0)
                gameListBox.SelectedIndex = 0;
            if (gameListBox.SelectedIndex != -1)
                Configuration1.LastGame = ((string) gameListBox.SelectedItem).Replace(" ★", "");

            var window = new Window {Size = new[] {Size.Width, Size.Height}, Location = new[] {Location.X, Location.Y}};
            Configuration1.Window = window;

            Favourites1.Patches.Clear();
            Favourites1.Games.Clear();

            foreach (string s in patchListBox.Items)
                if (s.Contains("★"))
                {
                    var v = s.Replace(" ★", "");
                    Favourites1.Patches.Add(v);
                }

            foreach (string s in gameListBox.Items)
                if (s.Contains("★"))
                {
                    var v = s.Replace(" ★", "");
                    Favourites1.Games.Add(v);
                }
        }

        /// <summary>
        ///     Writes the configuration and favourites to file
        /// </summary>
        private void UpdateConfigFile([CallerMemberName] string caller = "")
        {
            UpdateConfig();
            var output = JsonConvert.SerializeObject(Configuration1, Formatting.Indented, new JsonSerializerSettings());
            output = output.Remove(output.Length - 3);
            output += ",\n  \"Lang\": " + JsonConvert.SerializeObject(Configuration.Lang, Formatting.Indented, new JsonSerializerSettings()) + "\n}";
            File.WriteAllText(ConfigFile, output);

            output = JsonConvert.SerializeObject(Favourites1, Formatting.Indented);
            File.WriteAllText("favourites.js", output);

            Trace.WriteLine(
                $"[{DateTime.Now.ToShortTimeString()}] Config file has been successfully updated. Caller method was " +
                caller);
        }

        /// <summary>
        ///     Starts thcrap with the selected patch stack and executable
        /// </summary>
        private void StartThcrap()
        {
            if (patchListBox.SelectedIndex == -1 || gameListBox.SelectedIndex == -1)
            {
                MessageBox.Show(I18N.LangResource.errors.noneSelected, I18N.LangResource.errors.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var s = "";
            s += patchListBox.SelectedItem;
            s += " ";
            s += gameListBox.SelectedItem;
            s = s.Replace(" ★", "");
            //MessageBox.Show(args);
            var process = new Process {StartInfo = {FileName = "thcrap_loader.exe", Arguments = s}};
            process.Start();
            Debug.WriteLine("Starting thcrap with {0}", s);
            if (Configuration1.ExitAfterStartup)
                Application.Exit();
        }

        private void startButton_Click(object sender, EventArgs e) => StartThcrap();

        private void startButton_MouseHover(object sender, EventArgs e) =>
            startButton.BackgroundImage = Resources.Shinmera_Banner_5_mini_size_hover;

        private void startButton_MouseLeave(object sender, EventArgs e) =>
            startButton.BackgroundImage = Resources.Shinmera_Banner_5_mini_size;

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

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (ModifierKeys != Keys.None)
            {
                patchListBox.SelectedItem = Configuration1.LastConfig;
                gameListBox.SelectedItem = Configuration1.LastGame;
            }

            switch (e.KeyCode)
            {
                case Keys.F3:
                    UpdateLanguage();
                    break;
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
                        if (lb.Equals(patchListBox))
                            Favourites1.Patches.Add(lb.Items[lb.SelectedIndex].ToString());

                        if (lb.Equals(gameListBox))
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
            MessageBox.Show(I18N.LangResource.popup.kbSh.text,
                I18N.LangResource.popup.kbSh.caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_Closing(object sender, FormClosingEventArgs e) => UpdateConfigFile();

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

        public static Configuration Configuration1 { get; set; }
        private Favourites Favourites1 { get; set; } = new Favourites(new List<string>(), new List<string>());

        #endregion

        #region Sorting/Filtering Button functions

        private void sortAZButton1_Click(object sender, EventArgs e)
        {
            var isDesc = Configuration1.IsDescending;
            if (sortAZButton1.BackgroundImage.Equals(_sortDescending))
            {
                SortListBoxItems(ref patchListBox);
                sortAZButton1.BackgroundImage = _sortAscending;
                isDesc[0] = "false";
            }
            else
            {
                SortListBoxItemsDesc(ref patchListBox);
                isDesc[0] = "true";
                sortAZButton1.BackgroundImage = _sortDescending;
            }

            Configuration1.IsDescending = isDesc;
            ReadConfig();
        }

        private void sortAZButton2_Click(object sender, EventArgs e)
        {
            var isDesc = Configuration1.IsDescending;
            if (sortAZButton2.BackgroundImage.Equals(_sortDescending))
            {
                SortListBoxItems(ref gameListBox);
                sortAZButton2.BackgroundImage = _sortAscending;
                isDesc[1] = "false";
            }
            else
            {
                SortListBoxItemsDesc(ref gameListBox);
                isDesc[1] = "true";
                sortAZButton2.BackgroundImage = _sortDescending;
            }

            Configuration1.IsDescending = isDesc;
            ReadConfig();
        }

        private void filterButton1_Click(object sender, EventArgs e)
        {
            var onlyFav = Configuration1.OnlyFavourites;
            if (!filterFavButton1.BackgroundImage.Equals(_star))
            {
                filterFavButton1.BackgroundImage = _star;
                for (var n = patchListBox.Items.Count - 1; n >= 0; --n)
                {
                    const char filterItem = '★';
                    if (!patchListBox.Items[n].ToString().Contains(filterItem))
                        patchListBox.Items.RemoveAt(n);
                }

                onlyFav[0] = "true";
            }
            else
            {
                filterFavButton1.BackgroundImage = _starHollow;
                patchListBox.Items.Clear();
                foreach (var s in _jsFiles) patchListBox.Items.Add(s);

                AddStars(patchListBox, Favourites1.Patches);
                onlyFav[0] = "false";
            }

            Configuration1.OnlyFavourites = onlyFav;
            ReadConfig();
        }

        private void filterButton2_Click(object sender, EventArgs e)
        {
            var onlyFav = Configuration1.OnlyFavourites;
            if (!filterFavButton2.BackgroundImage.Equals(_star))
            {
                filterFavButton2.BackgroundImage = _star;
                for (var n = gameListBox.Items.Count - 1; n >= 0; --n)
                {
                    const string filterItem = "★";
                    if (!gameListBox.Items[n].ToString().Contains(filterItem))
                        gameListBox.Items.RemoveAt(n);
                }

                onlyFav[1] = "true";
            }
            else
            {
                filterFavButton2.BackgroundImage = _starHollow;
                gameListBox.Items.Clear();
                foreach (var s in _gamesList) gameListBox.Items.Add(s);

                AddStars(gameListBox, Favourites1.Games);
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
                gameListBox.Items.Clear();
                foreach (var item in _gamesList)
                    if (!item.Contains("_custom"))
                        gameListBox.Items.Add(item);
                AddStars(gameListBox, Favourites1.Games);
                if (sender != "DefaultSettings") Configuration1.FilterExeType = 1;
                return;
            }

            if (filterByType_button.BackgroundImage.Equals(_game))
            {
                filterByType_button.BackgroundImage = _custom;
                gameListBox.Items.Clear();
                foreach (var item in _gamesList)
                    if (item.Contains("_custom"))
                        gameListBox.Items.Add(item);
                AddStars(gameListBox, Favourites1.Games);
                if (sender != "DefaultSettings") Configuration1.FilterExeType = 2;
                return;
            }

            if (!filterByType_button.BackgroundImage.Equals(_custom)) return;
            {
                filterByType_button.BackgroundImage = _gameAndCustom;
                gameListBox.Items.Clear();
                foreach (var item in _gamesList) gameListBox.Items.Add(item);
                AddStars(gameListBox, Favourites1.Games);
                if (sender != "DefaultSettings") Configuration1.FilterExeType = 0;
            }
        }

        #endregion

        #region Tool Strip functions

        private void keyboardShortcutsTS_Click(object sender, EventArgs e) => ShowKeyboardShortcuts();

        private void restartTS_Click(object sender, EventArgs e) => RestartProgram();

        private void exitTS_Click(object sender, EventArgs e) => Application.Exit();

        private void bugReportTS_Click(object sender, EventArgs e) => Process.Start(
            "https://github.com/Tudi20/Universal-THCRAP-Launcher/issues/" +
            "new?assignees=&labels=bug&template=bug_report.md&title=%5BBUG%5D");

        private void featureRequestTS_Click(object sender, EventArgs e) => Process.Start(
            "https://github.com/Tudi20/Universal-THCRAP-Launcher/issues/" +
            "new?assignees=&labels=enhancement&template=feature_request.md&title=%5BFEATURE%5D");

        private void otherTS_Click(object sender, EventArgs e) =>
            Process.Start("https://github.com/Tudi20/Universal-THCRAP-Launcher/issues/new");

        private void gitHubPageTS_Click(object sender, EventArgs e) =>
            Process.Start("https://github.com/Tudi20/Universal-THCRAP-Launcher");

        private void openConfigureTS_Click(object sender, EventArgs e)
        {
            MessageBox.Show(I18N.LangResource.popup.hideLauncher.text,
                I18N.LangResource.popup.hideLauncher.caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            var p = Process.Start("thcrap_configure.exe");
            if (p == null)
            {
                MessageBox.Show(I18N.LangResource.errors.oops, I18N.LangResource.errors.error);
                return;
            }

            Hide();
            while (!p.HasExited) Thread.Sleep(1);
            Show();
        }

        private void openGamesListTS_Click(object sender, EventArgs e) => Process.Start("games.js");

        private void openFolderTS_Click(object sender, EventArgs e) =>
            Process.Start(Directory.GetCurrentDirectory());

        private void createShortcutTS_Click(object sender, EventArgs e)
        {
            var shDesktop = (object) "Desktop";
            var shell = new WshShell();
            var shortcutAddress = (string) shell.SpecialFolders.Item(ref shDesktop) + I18N.LangResource.shCreate.file + ".lnk";
            var shortcut = (IWshShortcut) shell.CreateShortcut(shortcutAddress);
            shortcut.Description = I18N.LangResource.shCreate.desc;
            shortcut.TargetPath = Assembly.GetEntryAssembly().Location;
            shortcut.WorkingDirectory = Directory.GetCurrentDirectory();
            shortcut.Save();
        }

        private void openSelectedPatchConfigurationTS_Click(object sender, EventArgs e) =>
            Process.Start(Directory.GetCurrentDirectory() + @"/" + patchListBox.SelectedItem.ToString().Replace(" ★", ""));
        

        private void settingsTS_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
            UpdateLanguage();
        }
        #endregion

        static string ReadTextFromUrl(string url)
        {
            // Assume UTF8, but detect BOM - could also honor response charset I suppose
            using (var client = new WebClient())
            using (var stream = client.OpenRead(url))
            using (var textReader = new StreamReader(stream, System.Text.Encoding.UTF8, true))
            {
                return textReader.ReadToEnd();
            }
        }
    }

    public static class I18N
    {
        public static readonly string I18NDir = Directory.GetCurrentDirectory() + @"\i18n\utl\";

        public static dynamic LangResource { get; private set; }
        

        public static int LangNumber()
        {
            if (Directory.Exists(I18NDir))
                return Directory.GetFiles(I18NDir).Length;

            return 0;
        }

        public static void GetLangResource(string filePath)
        {
            string raw = File.ReadAllText(filePath);
            try
            {
                LangResource = JsonConvert.DeserializeObject(raw);
                Configuration.Lang = filePath.Replace(I18NDir, "");
            }
            catch (JsonReaderException e)
            {
                MessageBox.Show(e.Message, @"JSON Parser Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
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
        public static string Lang { get; set; }
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

