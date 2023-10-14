using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Universal_THCRAP_Launcher
{
    internal class Log
    {
        private readonly FileStream _fs;
        private readonly StreamWriter _sw;

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

            // Delete log file if larger than 1 MB
            if (File.Exists(logFile) && new FileInfo(logFile).Length >= 0x100000)
                File.Delete(logFile);

            string dirName = Path.GetDirectoryName(logFile);
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            _fs = new FileStream(logFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            _sw = new StreamWriter(_fs);
        }

        public void WriteLine(object text)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] " + text);
            if (_sw == null) return;
            _sw.WriteLine($"[{DateTime.Now.ToLongTimeString()}] " + text);
            _sw.Flush();
            _fs.Flush();
        }

        public void Write(object text)
        {
            Console.Write(text);
            if (_sw == null) return;
            _sw.Write(text);
            _sw.Flush();
            _fs.Flush();
        }

        ~Log()
        {
            if (_sw != null)
            {
                _sw.Flush();
                _sw.Close();
                _sw.Dispose();
            }
            if (_fs != null)
            {
                _fs.Flush();
                _fs.Close();
                _fs.Dispose();
            }
        }
    }
}
