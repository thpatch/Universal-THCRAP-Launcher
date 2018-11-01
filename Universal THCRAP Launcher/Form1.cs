using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Universal_THCRAP_Launcher.Properties;

namespace Universal_THCRAP_Launcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void ErrorAndExit(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

         List<string> _jsFiles = new List<string>();
         List<string> _gamesList = new List<string>();

        public Configuration Configuration1 { get; private set; } =
            new Configuration(true, 5, false, true, true, ".js", "th");

        public Favourites Favourites1 { get; set; } = new Favourites(new List<string>(), new List<string>());

        private int[] _resizeConsts;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Give error if not next to thcrap_loader.exe
            const string msgError1 =
                "thcrap_loader.exe couldn't be found.\nMake sure you put the application next to it!";
            bool fileExists = !File.Exists("thcrap_loader.exe");
            if (fileExists)
                ErrorAndExit(msgError1);

            //Give error if no games.js file
            const string msgError2 =
                "games.js couldn't be found.\nMake sure you run thcrap_configure.exe first!";
            if (!File.Exists("games.js")) ErrorAndExit(msgError2);

            #region Load data from files

            //Load patch stacks
            _jsFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.js").ToList();

            //Give error if there are no .js files apart from games.js and config.js
            const string msgError3 =
                "No config files could be found.\nMake sure you run thcrap_configure.exe first!";
            if (_jsFiles.Count == 0) ErrorAndExit(msgError3);

            //Load executables
            string file = File.ReadAllText("games.js");
            Dictionary<string, string> games = JsonConvert.DeserializeObject<Dictionary<string, string>>(file);

            //Load config
            if (File.Exists("config.js"))
            {
                file = File.ReadAllText("config.js");
                Configuration1 = JsonConvert.DeserializeObject<Configuration>(file);
            }

            //Load favourites
            if (File.Exists("favourites.js"))
            {
                file = File.ReadAllText("favourites.js");
                Favourites1 = JsonConvert.DeserializeObject<Favourites>(file);
            }

            #endregion
            #region  Fix patch stack list
            for (int i = 0; i < _jsFiles.Count; i++)
                _jsFiles[i] = _jsFiles[i].Replace(Directory.GetCurrentDirectory() + "\\", "");
            _jsFiles.Remove("games.js");
            _jsFiles.Remove("config.js");
            _jsFiles.Remove("favourites.js");
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
            
            //Display config
            checkBox1.Checked = Configuration1.UthcraplExitAfterStartup;

            

            //Update Display favourites
            foreach (var VARIABLE in Favourites1.Patches)
            {
                int index = listBox1.FindStringExact(VARIABLE);
                listBox1.Items[index] += " ★";
            }
            foreach (var VARIABLE in Favourites1.Games)
            {
                int index = listBox2.FindStringExact(VARIABLE);
                listBox2.Items[index] += " ★";
            }
            #endregion
            #region Set stuff

            //Create const for resizing
            _resizeConsts = new int[8];
            _resizeConsts[0] = Size.Width - button1.Width;
            _resizeConsts[1] = Size.Width - splitContainer1.Width;
            _resizeConsts[2] = Size.Height - splitContainer1.Height;
            _resizeConsts[3] = Size.Height - checkBox1.Location.Y;
            _resizeConsts[4] = Size.Height - label1.Location.Y;
            _resizeConsts[5] = splitContainer1.Location.Y - sort_az_button1.Location.Y;
            _resizeConsts[6] = sort_az_button2.Location.X - listBox1.Size.Width;
            _resizeConsts[7] = star_button2.Location.X - sort_az_button2.Location.X;

            #endregion

            //Default sort
            SortListBoxItems(ref listBox1);
            SortListBoxItems(ref listBox2);
            Debug.WriteLine("Form1 Loaded");
        }

        

        private void SortListBoxItems(ref ListBox lb)
        {
            List<object> items;
            items = lb.Items.OfType<object>().ToList();
            lb.Items.Clear();
            lb.Items.AddRange(items.OrderBy(i => i).ToArray());
        }

        private void SortListBoxItemsDesc(ref ListBox lb)
        {
            List<object> items;
            items = lb.Items.OfType<object>().ToList();
            lb.Items.Clear();
            lb.Items.AddRange(items.OrderByDescending(i => i).ToArray());
        }

        /// <summary>
        /// Updates the configuration and favourites list
        /// </summary>
        private void UpdateConfig()
        {
            if (listBox1.SelectedIndex == -1 && listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;
            if (listBox1.SelectedIndex != -1)
            Configuration1.UthcraplLastConfig = ((string)listBox1.SelectedItem).Replace(" ★","");
            if (listBox2.SelectedIndex == -1 && listBox2.Items.Count > 0)
                listBox2.SelectedIndex = 0;
            if (listBox2.SelectedIndex != -1)
            Configuration1.UthcraplLastGame = ((string)listBox2.SelectedItem).Replace(" ★", "");
            
            

            Favourites1.Patches.Clear();
            Favourites1.Games.Clear();

            foreach (string s in listBox1.Items)
                if (s.Contains("★"))
                {
                    string v = s.Replace(" ★", "");
                    Favourites1.Patches.Add(v);
                }

            foreach (string s in listBox2.Items)
                if (s.Contains("★"))
                {
                    string v = s.Replace(" ★", "");
                    Favourites1.Games.Add(v);
                }
        }

        /// <summary>
        /// Selects the items based on the configuration
        /// </summary>
        private void ReadConfig()
        {
            string s = Configuration1.UthcraplLastConfig;
            if (Favourites1.Patches.Contains(s))
                s += " ★";
            listBox1.SelectedIndex = listBox1.FindStringExact(s);
            s = Configuration1.UthcraplLastGame;

            if (Favourites1.Games.Contains(s))
                s += " ★";

            listBox2.SelectedIndex = listBox2.FindStringExact(s);

            if (listBox1.SelectedIndex == -1)
                listBox1.SelectedIndex = 0;
            if (listBox2.SelectedIndex == -1)
                listBox2.SelectedIndex = 0;
        }

        /// <summary>
        /// Writes the configuration and favourites to file
        /// </summary>
        private void UpdateConfigFile()
        {
            UpdateConfig();
            string output = JsonConvert.SerializeObject(Configuration1, Formatting.Indented);
            File.WriteAllText("config.js", output);

            output = JsonConvert.SerializeObject(Favourites1,Formatting.Indented);
            File.WriteAllText("favourites.js", output);
            
            Debug.WriteLine("Config file Updated!");
        }

        /// <summary>
        /// Starts thcrap with the selected patch stack and executable
        /// </summary>
        private void StartThcrap()
        {
            if (listBox1.SelectedIndex == -1 || listBox2.SelectedIndex == -1)
            {
                const string error = "No run configuration (patch stack) or game (executable) selected!\nPlease select one!";
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string s = "";
            s += listBox1.SelectedItem;
            s += " ";
            s += listBox2.SelectedItem;
            s = s.Replace(" ★", "");
            //MessageBox.Show(args);
            Process process = new Process {StartInfo = {FileName = "thcrap_loader.exe", Arguments = s}};
            process.Start();
            Debug.WriteLine("Starting thcrap with {0}", s);
            if (checkBox1.Checked)
                Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e) => StartThcrap();

        /// <summary>
        /// Handles starting thcrap with enter and favouriting when pressing f
        /// </summary>
        private new void KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
                StartThcrap();
            if (e.KeyChar == 'f' || e.KeyChar == 'F')
                if (sender.GetType().FullName == "System.Windows.Forms.ListBox")
                {
                    ListBox lb = (ListBox) sender;
                    if (!lb.SelectedItem.ToString().Contains("★"))
                    {
                        if (lb.Equals(listBox1))
                            Favourites1.Patches.Add(lb.Items[lb.SelectedIndex].ToString());

                        if (lb.Equals(listBox2))
                            Favourites1.Games.Add(lb.Items[lb.SelectedIndex].ToString());
                        lb.Items[lb.SelectedIndex] += " ★";
                    }
                    else
                        lb.Items[lb.SelectedIndex] = lb.Items[lb.SelectedIndex].ToString().Replace(" ★", "");
                }
            UpdateConfigFile();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) => UpdateConfigFile();

        private void button1_MouseHover(object sender, EventArgs e) => button1.BackgroundImage = Resources.Shinmera_Banner_5_mini_size_hover;

        private void button1_MouseLeave(object sender, EventArgs e) => button1.BackgroundImage = Resources.Shinmera_Banner_5_mini_size;

        private void Form1_Resize(object sender, EventArgs e)
        {
            button1.Size = new Size(Size.Width - _resizeConsts[0], button1.Size.Height);
            splitContainer1.Size = new Size(Size.Width - _resizeConsts[1], Size.Height - _resizeConsts[2]);
            listBox1.Size = new Size(splitContainer1.Panel1.Width - 1, splitContainer1.Panel1.Height - 1);
            listBox2.Size = new Size(splitContainer1.Panel2.Width - 1, splitContainer1.Panel2.Height - 1);
            checkBox1.Location = new Point(checkBox1.Location.X, Size.Height - _resizeConsts[3]);
            label1.Location = new Point(label1.Location.X, Size.Height - _resizeConsts[4]);
            sort_az_button1.Location = new Point(sort_az_button1.Location.X, splitContainer1.Location.Y - _resizeConsts[5]);
            sort_az_button2.Location = new Point(listBox1.Size.Width + _resizeConsts[6], splitContainer1.Location.Y - _resizeConsts[5]);
            star_button1.Location = new Point(star_button1.Location.X, splitContainer1.Location.Y - _resizeConsts[5]);
            star_button2.Location = new Point(sort_az_button2.Location.X + _resizeConsts[7], splitContainer1.Location.Y - _resizeConsts[5]);
        }

        private void label1_Click(object sender, EventArgs e) => Process.Start("https://github.com/Tudi20/Universal-THCRAP-Launcher");

        private readonly Image _sortAscending = new Bitmap(Resources.Sort_Ascending);
        private readonly Image _sortDescending = new Bitmap(Resources.Sort_Decending);

        private void sort_az_button1_Click(object sender, EventArgs e)
        {
            if (sort_az_button1.BackgroundImage.Equals(_sortAscending))
            {
                SortListBoxItems(ref listBox1);
                sort_az_button1.BackgroundImage = _sortDescending;
            }
            else
            {
                SortListBoxItemsDesc(ref listBox1);
                sort_az_button1.BackgroundImage = _sortAscending;
            }
            ReadConfig();
        }

        private void sort_az_button2_Click(object sender, EventArgs e)
        {
            if (sort_az_button2.BackgroundImage.Equals(_sortAscending))
            {
                SortListBoxItems(ref listBox2);
                sort_az_button2.BackgroundImage = _sortDescending;
            }
            else
            {
                SortListBoxItemsDesc(ref listBox2);
                sort_az_button2.BackgroundImage = _sortAscending;
            }
            ReadConfig();
        }

        private readonly Image _star = new Bitmap(Resources.Star);
        private readonly Image _starHollow = new Bitmap(Resources.Star_Hollow);

        private void star_button1_Click(object sender, EventArgs e)
        {
            if (!star_button1.BackgroundImage.Equals(_starHollow))
            {
                star_button1.BackgroundImage = _starHollow;
                for (int n = listBox1.Items.Count - 1; n >= 0; --n)
                {
                    string filterItem = "★";
                    if (!listBox1.Items[n].ToString().Contains(filterItem))
                        listBox1.Items.RemoveAt(n);
                }
            }
            else
            {
                star_button1.BackgroundImage = _star;
                listBox1.Items.Clear();
                foreach (string s in _jsFiles)
                {
                    listBox1.Items.Add(s);
                }
                foreach (var VARIABLE in Favourites1.Patches)
                {
                    int index = listBox1.FindStringExact(VARIABLE);
                    listBox1.Items[index] += " ★";
                    
                }
                ReadConfig();
            }
        }

        private void star_button2_Click(object sender, EventArgs e)
        {
            if (!star_button2.BackgroundImage.Equals(_starHollow))
            {
                star_button2.BackgroundImage = _starHollow;
                for (int n = listBox2.Items.Count - 1; n >= 0; --n)
                {
                    string filterItem = "★";
                    if (!listBox2.Items[n].ToString().Contains(filterItem))
                        listBox2.Items.RemoveAt(n);
                }
            }
            else
            {
                star_button2.BackgroundImage = _star;
                listBox2.Items.Clear();
                foreach (string s in _gamesList)
                {
                    listBox2.Items.Add(s);
                }
                foreach (var VARIABLE in Favourites1.Games)
                {
                    int index = listBox2.FindStringExact(VARIABLE);
                    listBox2.Items[index] += " ★";
                }
                ReadConfig();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            ReadConfig();
            //Set default selection index
            if (listBox1.SelectedIndex == -1)
                listBox1.SelectedIndex = 0;

            if (listBox2.SelectedIndex == -1)
                listBox2.SelectedIndex = 0;

            UpdateConfigFile();
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = (ListBox) sender;
            switch (lb.Name)
            {
                case "listBox1":
                    if (lb.SelectedIndex != -1)
                    Configuration1.UthcraplLastConfig = lb.SelectedItem.ToString().Replace(" ★","");
                    break;
                case "listBox2":
                    if (lb.SelectedIndex != -1)
                    Configuration1.UthcraplLastGame = lb.SelectedItem.ToString().Replace(" ★", "");
                    break;
            }
        }
    }

#pragma warning disable IDE1006 // Naming Styles
    public class Configuration
    {
        public Configuration(bool background_updates, int time_between_updates, bool update_at_exit, bool update_others,
            bool uthcraplExitAfterStartup, string uthcraplLastConfig, string uthcraplLastGame)
        {
            this.background_updates = background_updates;
            this.time_between_updates = time_between_updates;
            this.update_at_exit = update_at_exit;
            this.update_others = update_others;
            UthcraplExitAfterStartup = uthcraplExitAfterStartup;
            UthcraplLastConfig = uthcraplLastConfig;
            UthcraplLastGame = uthcraplLastGame;
        }

        public bool background_updates { get; set; }
        public int time_between_updates { get; set; }
        public bool update_at_exit { get; set; }
        public bool update_others { get; set; }
        public bool UthcraplExitAfterStartup { get; set; }
        public string UthcraplLastConfig { get; set; }
        public string UthcraplLastGame { get; set; }
#pragma warning restore IDE1006 // Naming Styles
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
}