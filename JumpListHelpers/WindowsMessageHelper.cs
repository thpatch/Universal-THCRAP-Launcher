/* Copyright 2012 Marco Minerva, marco.minerva@gmail.com

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace JumpListHelpers
{
    internal class WindowsMessageHelper
    {
        #region Platform Invoke

        internal const int WM_COPYDATA = 0x004A;

        private struct CopyDataStruct : IDisposable
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;

            public void Dispose()
            {
                if (lpData != IntPtr.Zero)
                {
                    LocalFree(this.lpData);
                    lpData = IntPtr.Zero;
                }
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref CopyDataStruct lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern int RegisterWindowMessage(string msgName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LocalAlloc(int flag, int size);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LocalFree(IntPtr p);

        #endregion

        public static string MainFormName { get; set; }
        
        public static Dictionary<int, string> WindowMessages { get; set; }
        internal const string COMMAND_PREFIX = "__JumpListCommand:";

        static WindowsMessageHelper()
        {
            WindowMessages = new Dictionary<int, string>();
        }

        public static int RegisterCommand(string commandName)
        {
            int command = RegisterWindowMessage(commandName);
            WindowMessages[command] = commandName;
            return command;
        }

        public static bool SendMessage(IntPtr handle, int msgId)
        {
            if (handle == IntPtr.Zero)
            {
                handle = FindWindow(null, MainFormName);
                if (handle == IntPtr.Zero) return false;
            }

            long result = SendMessage(handle, msgId, IntPtr.Zero, IntPtr.Zero);

            if (result == 0) return true;
            else return false;
        }

        public static bool SendMessage(IntPtr handle, string args)
        {
            if (handle == IntPtr.Zero)
            {
                handle = FindWindow(null, MainFormName);
                if (handle == IntPtr.Zero) return false;
            }

            WindowsMessageHelper.CopyDataStruct cds = new WindowsMessageHelper.CopyDataStruct();
            try
            {
                cds.cbData = (args.Length + 1) * 2;
                cds.lpData = WindowsMessageHelper.LocalAlloc(0x40, cds.cbData);
                Marshal.Copy(args.ToCharArray(), 0, cds.lpData, args.Length);
                cds.dwData = (IntPtr)1;
                WindowsMessageHelper.SendMessage(handle, WindowsMessageHelper.WM_COPYDATA, IntPtr.Zero, ref cds);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                cds.Dispose();
            }
        }

        public static string GetArguments(IntPtr lParam)
        {
            string arguments = null;
            try
            {
                CopyDataStruct st = (CopyDataStruct)Marshal.PtrToStructure(lParam, typeof(CopyDataStruct));
                arguments = Marshal.PtrToStringUni(st.lpData);
            }
            catch { }

            return arguments;
        }
    }
}
