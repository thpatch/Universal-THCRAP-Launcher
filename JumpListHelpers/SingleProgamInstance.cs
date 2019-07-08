/* Copyright 2012 Marco Minerva, marco.minerva@gmail.com

   From code written by Michael Potter
   More information here: http://www.codeproject.com/KB/cs/cssingprocess.aspx  
  
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
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace JumpListHelpers
{
    /// <summary>
    /// <strong>SingleProgamInstance</strong> uses a mutex synchronization object
    /// to ensure that only one copy of process is running at
    /// a particular time. It also allows for UI identification
    /// of the intial process by bring that window to the foreground.
    /// </summary>
    internal sealed class SingleProgramInstance : IDisposable
    {
        #region Platform Invoke

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        private const int SW_RESTORE = 9;

        #endregion

        private Mutex processSync = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleProgramInstance"/> class.
        /// </summary>
        public SingleProgramInstance()
            : this(string.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleProgramInstance"/> class using the specified string as the process identifier.
        /// </summary>
        /// <param name="identifier">The string that represents the process identifier.</param>
        public SingleProgramInstance(string identifier)
        {
            processSync = new Mutex(false,
                Assembly.GetExecutingAssembly().GetName().Name + identifier);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is single instance.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is single instance; otherwise, <c>false</c>.
        /// </value>
        public bool IsSingleInstance
        {
            get
            {
                if (processSync.WaitOne(0, false))
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// If another instance of the application is already running, raises the other process.
        /// </summary>
        public IntPtr RaiseOtherProcess()
        {
            Process proc = Process.GetCurrentProcess();
            // Using Process.ProcessName does not function properly when
            // the name exceeds 15 characters. Using the assembly name
            // takes care of this problem and is more accruate than other
            // work arounds.
            string assemblyName = Assembly.GetEntryAssembly().GetName().Name;
            foreach (Process otherProc in Process.GetProcessesByName(assemblyName))
            {
                //ignore this process
                if (proc.Id != otherProc.Id)
                {
                    // Found a "same named process".
                    // Assume it is the one we want brought to the foreground.
                    // Use the Win32 API to bring it to the foreground.
                    IntPtr hWnd = otherProc.MainWindowHandle;
                    if (IsIconic(hWnd))
                        ShowWindowAsync(hWnd, SW_RESTORE);
                    SetForegroundWindow(hWnd);
                    return hWnd;
                }
            }

            return IntPtr.Zero;
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="SingleProgramInstance"/> is reclaimed by garbage collection.
        /// </summary>
        ~SingleProgramInstance()
        {
            //Release mutex (if necessary) 
            //This should have been accomplished using Dispose() 
            this.FreeResources();
        }

        private void FreeResources()
        {
            try
            {
                if (processSync.WaitOne(0, false))
                {
                    //If we own the mutex than release it so that
                    //other "same" processes can now start.
                    processSync.ReleaseMutex();
                }
                processSync.Close();
            }
            catch { }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            //release mutex (if necessary) and notify 
            //the garbage collector to ignore the destructor
            this.FreeResources();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
