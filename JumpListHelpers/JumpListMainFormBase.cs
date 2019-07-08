using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JumpListHelpers
{
    public partial class JumpListMainFormBase : Form
    {
        public JumpListMainFormBase()
        {
            InitializeComponent();
        }

        public event EventHandler<CommandEventArgs> JumpListCommandReceived;
        public event EventHandler<StartupEventArgs> StarupInstance;

        internal void OnStartupInstance(StartupEventArgs e)
        {
            if (StarupInstance != null)
                StarupInstance(this, e);
        }

        internal void OnJumpListCommandReceived(CommandEventArgs e)
        {
            if (JumpListCommandReceived != null)
                JumpListCommandReceived(this, e);
        }

        protected override void WndProc(ref Message m)
        {
            if (WindowsMessageHelper.WindowMessages.ContainsKey(m.Msg))
            {
                OnJumpListCommandReceived(new CommandEventArgs(WindowsMessageHelper.WindowMessages[m.Msg]));
            }
            else if (m.Msg == WindowsMessageHelper.WM_COPYDATA)
            {
                string arguments = WindowsMessageHelper.GetArguments(m.LParam);
                OnStartupInstance(new StartupEventArgs(false, arguments));
            }
            else
            {
                base.WndProc(ref m);
            }
        }

    }
}
