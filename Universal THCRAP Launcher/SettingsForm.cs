using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Universal_THCRAP_Launcher
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            languageComboBox.SelectedIndex = 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) =>
            MainForm.Configuration1.ExitAfterStartup = closeOnExitCheckBox.Checked;

        private void languageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException("Language changing is currently not implemented, yet."); 
        }


    }
}
