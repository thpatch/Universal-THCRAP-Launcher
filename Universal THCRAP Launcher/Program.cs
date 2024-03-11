using System;
using System.Windows.Forms;

namespace Universal_THCRAP_Launcher
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var mainForm = new MainForm();
                if (!mainForm.InitChecks())
                {
                    return;
                }

                Application.Run(mainForm);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
