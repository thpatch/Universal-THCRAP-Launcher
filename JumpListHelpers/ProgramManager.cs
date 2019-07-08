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
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace JumpListHelpers
{
    public static class ProgramManager
    {
        public static void Run(Type formType, string mainFormTitle)
        {
            JumpListHelpers.WindowsMessageHelper.MainFormName = mainFormTitle;
            using (SingleProgramInstance spi = new SingleProgramInstance())
            {                    
                if (spi.IsSingleInstance)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Form form = (Form)Activator.CreateInstance(formType);
                    JumpListMainFormBase mainForm = form as JumpListMainFormBase;
                    if (mainForm != null)
                    {
                        var args = string.Join(" ", Environment.GetCommandLineArgs().Skip(1).ToArray());
                        mainForm.OnStartupInstance(new StartupEventArgs(true, args));
                    }
                    Application.Run(form);
                }
                else
                {
                    // The program has already been started, so pass the arguments to it.
                    IntPtr handle = spi.RaiseOtherProcess();    
                    HandleCommand(handle);
                }
            }
        }

        private static void HandleCommand(IntPtr handle)
        {
            var commandLineArgs = Environment.GetCommandLineArgs();
            if (commandLineArgs.Length > 1 && commandLineArgs[1].StartsWith(WindowsMessageHelper.COMMAND_PREFIX))
            {              
                // It is a Jump List command.
                string temp = commandLineArgs[1].Split(':').LastOrDefault();
                int commandNumber;
                if (int.TryParse(temp, out commandNumber))
                    WindowsMessageHelper.SendMessage(handle, commandNumber);
            }
            else
            {
                var args = string.Join(" ", commandLineArgs.Skip(1).ToArray());
                WindowsMessageHelper.SendMessage(handle, args);
            }
        }
    }
}
