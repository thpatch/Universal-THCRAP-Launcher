using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

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

        Configuration configuration = new Configuration();
        
        private void Form1_Load(object sender, EventArgs e)
        {
            const string msg_Error1 = "thcrap_loader.exe couldn't be found.\nMake sure you put the application next to it!";
            const string msg_Error2 = "games.js couldn't be found.\nMake sure you run thcrap_configure.exe first!";
            const string msg_Error3 = "No config files could be found.\nMake sure you run thcrap_configure.exe first!";

            if (!File.Exists("thcrap_loader.exe")) ErrorAndExit(msg_Error1);
            if (!File.Exists("games.js")) ErrorAndExit(msg_Error2);

            List<string> jsFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.js").ToList();

            for (int i = 0; i < jsFiles.Count; i++)
            {
                jsFiles[i] = jsFiles[i].Replace(Directory.GetCurrentDirectory() + "\\", "");
            }

            if (jsFiles.Contains("games.js"))
                jsFiles.Remove("games.js");

            if (jsFiles.Contains("config.js"))
                jsFiles.Remove("config.js");

            if (jsFiles.Count == 0) ErrorAndExit(msg_Error3);

            foreach (var item in jsFiles)
                listBox1.Items.Add(item);

            FileStream fs = new FileStream("games.js", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string file = sr.ReadToEnd();
            sr.Close();
            fs.Close();

            Dictionary<string, string> games = JsonConvert.DeserializeObject<Dictionary<string, string>>(file);

            foreach (var item in games)
                listBox2.Items.Add(item.Key);

            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;

            //MessageBox.Show((string)listBox1.SelectedItem);

            configuration.uthcrapl_last_config = (string)listBox1.SelectedItem;
            configuration.uthcrapl_last_game = (string)listBox2.SelectedItem;

            if (File.Exists("config.js")) 
            {
                file = File.ReadAllText("config.js");
                configuration = JsonConvert.DeserializeObject<Configuration>(file);
                checkBox1.Checked = configuration.uthcrapl_exit_after_startup;
                listBox1.SelectedIndex = listBox1.FindString(configuration.uthcrapl_last_config);
                listBox2.SelectedIndex = listBox2.FindString(configuration.uthcrapl_last_game);
            }

            UpdateConfigFile();
        }

        private void UpdateConfigFile()
        {
            configuration.uthcrapl_last_config = (string)listBox1.SelectedItem;
            configuration.uthcrapl_last_game = (string)listBox2.SelectedItem;
            string output = JsonConvert.SerializeObject(configuration, Formatting.Indented);
            File.WriteAllText("config.js", output);
        }
        
        private void StartThcrap()
        {
            string s = "";
            s += listBox1.SelectedItem;
            s += " ";
            s += listBox2.SelectedItem;
            //MessageBox.Show(args);
            Process process = new Process();
            process.StartInfo.FileName = "thcrap_loader.exe";
            process.StartInfo.Arguments = s;
            process.Start();
            if (checkBox1.Checked)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartThcrap();
        }

        private new void KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                StartThcrap();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateConfigFile();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.Shinmera_Banner_5_mini_size_hover;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.Shinmera_Banner_5_mini_size;
        }
    }


    internal class Configuration
    {
#pragma warning disable IDE1006 // Naming Styles
        public bool background_updates { get; set; } = true;
        public int time_between_updates { get; set; } = 5;
        public bool update_at_exit { get; set; } = false;
        public bool update_others { get; set; } = true;
        public bool uthcrapl_exit_after_startup { get; set; } = true;
        public string uthcrapl_last_config { get; set; } = "";
        public string uthcrapl_last_game { get; set; } = "";
#pragma warning restore IDE1006 // Naming Styles
    }

}
