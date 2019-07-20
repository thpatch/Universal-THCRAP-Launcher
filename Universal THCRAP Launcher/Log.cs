using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Permissions;
using System.Security;
using System.Windows.Forms;

namespace Universal_THCRAP_Launcher
{
    class Log
    {
        private StreamWriter sw;

        public Log(string logFile)
        {
            var permissionSet = new PermissionSet(PermissionState.None);
            var writePermission = new FileIOPermission(FileIOPermissionAccess.Write, Environment.CurrentDirectory + "\\" + logFile);
            permissionSet.AddPermission(writePermission);

            if (!permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
            {
                Console.WriteLine($"No write permission for {logFile}.");
                MessageBox.Show($"No write permission for {logFile}.");
                return;
            }

            sw = new StreamWriter(logFile, true, Encoding.UTF8);
        }

        public void WriteLine(object text)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] " + text);
            sw.WriteLine($"[{DateTime.Now.ToLongTimeString()}] " + text);
            sw.Flush();
        }

        public void Write(object text)
        {
            Console.Write(text);
            sw.Write(text);
            sw.Flush();
        }

        ~Log()
        {
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }
}
