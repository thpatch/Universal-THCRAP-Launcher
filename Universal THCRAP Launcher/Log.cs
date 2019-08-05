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
        private FileStream fs;
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

            fs = new FileStream(logFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            sw = new StreamWriter(fs);
        }

        public void WriteLine(object text)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] " + text);
            if (sw == null) return;
            sw.WriteLine($"[{DateTime.Now.ToLongTimeString()}] " + text);
            sw.Flush();
            fs.Flush();
        }

        public void Write(object text)
        {
            Console.Write(text);
            if (sw == null) return;
            sw.Write(text);
            sw.Flush();
            fs.Flush();
        }

        ~Log()
        {
            sw.Flush();
            fs.Flush();
            sw.Close();
            fs.Close();
            sw.Dispose();
            fs.Dispose();
        }
    }
}
