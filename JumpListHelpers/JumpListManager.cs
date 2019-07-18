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

   THIS FILE IS MODIFIED TO FIT THE PURPOSE OF THIS PROGRAM.
*/

using System;
using System.Collections.Generic;
using System.Reflection;

namespace JumpListHelpers
{
    public static class JumpListManager
    {
        private static Microsoft.WindowsAPICodePack.Taskbar.JumpList list;
        private static Dictionary<string, Microsoft.WindowsAPICodePack.Taskbar.JumpListCustomCategory> categories;

        public static bool AutoRefresh { get; set; }

        static JumpListManager()
        {
            list = Microsoft.WindowsAPICodePack.Taskbar.JumpList.CreateJumpList();
            list.ClearAllUserTasks();
            list.Refresh();
            categories = new Dictionary<string, Microsoft.WindowsAPICodePack.Taskbar.JumpListCustomCategory>();
        }

        #region AddUserTask overloads

        public static void AddTaskLink(string title, string path)
        {
            AddTaskLink(title, path, null, null, 0);
        }

        public static void AddTaskLink(string title, string path, string arguments, string iconPath)
        {
            AddTaskLink(title, path, arguments, iconPath, 0);
        }

        public static void AddTaskLink(string title, string path, string iconPath, int iconNumber)
        {
            AddTaskLink(title, path, null, iconPath, iconNumber);
        }

        public static void AddTaskLink(string title, string path, string iconPath)
        {
            AddTaskLink(title, path, null, iconPath, 0);
        }

        public static void AddTaskLink(string title, string path, string arguments, string iconPath, int iconNumber)
        {
            var icon = CreateIconReference(iconPath, iconNumber);
            AddTaskLink(title, path, arguments, icon);
        }

        private static void AddTaskLink(string title, string path, string arguments, Microsoft.WindowsAPICodePack.Shell.IconReference? icon)
        {
            var task = CreateJumpListLink(title, path, arguments, icon);
            list.AddUserTasks(task);

            if (AutoRefresh)
                list.Refresh();
        }

        #endregion

        #region Add Custom Links

        public static void AddCategoryLink(string categoryName, string title, string path)
        {
            AddCategoryLink(categoryName, title, path, null, null, 0);
        }

        public static void AddCategoryLink(string categoryName, string title, string path, string arguments, string iconPath)
        {
            AddCategoryLink(categoryName, title, path, arguments, iconPath, 0);
        }

        public static void AddCategoryLink(string categoryName, string title, string path, string iconPath, int iconNumber)
        {
            AddCategoryLink(categoryName, title, path, null, iconPath, iconNumber);
        }

        public static void AddCategoryLink(string categoryName, string title, string path, string iconPath)
        {
            AddCategoryLink(categoryName, title, path, null, iconPath, 0);
        }

        public static void AddCategoryLink(string categoryName, string title, string path, string arguments, string iconPath, int iconNumber)
        {
            var icon = CreateIconReference(iconPath, iconNumber);
            AddCategoryLink(categoryName, title, path, arguments, icon);
        }

        private static void AddCategoryLink(string categoryName, string title, string path, string arguments, Microsoft.WindowsAPICodePack.Shell.IconReference? icon)
        {
            var task = CreateJumpListLink(title, path, arguments, icon);
            Microsoft.WindowsAPICodePack.Taskbar.JumpListCustomCategory category;
            if (!categories.TryGetValue(categoryName, out category))
            {
                category = new Microsoft.WindowsAPICodePack.Taskbar.JumpListCustomCategory(categoryName);
                categories[categoryName] = category;
                list.AddCustomCategories(category);
            }

            category.AddJumpListItems(task);

            if (AutoRefresh)
                list.Refresh();
        }

        #endregion

        #region Add Category Self Links

        public static void AddCategorySelfLink(string categoryName, string title, string commandName)
        {
            AddCategorySelfLink(categoryName, title, commandName, null, 0);
        }

        public static void AddCategorySelfLink(string categoryName, string title, string commandName, string iconPath)
        {
            AddCategorySelfLink(categoryName, title, commandName, iconPath, 0);
        }

        public static void AddCategorySelfLink(string categoryName, string title, string commandName, string iconPath, int iconNumber)
        {
            if (string.IsNullOrWhiteSpace(commandName))
                throw new ArgumentNullException("commandName");

            var icon = CreateIconReference(iconPath, iconNumber);

            // Register the command and get the associated number.
            int command = WindowsMessageHelper.RegisterCommand(commandName);
            AddCategoryLink(categoryName, title, Assembly.GetEntryAssembly().Location, WindowsMessageHelper.COMMAND_PREFIX + command.ToString(), icon);
        }

        #endregion

        #region Add Task Self Links

        public static void AddTaskSelfLink(string title, string commandName)
        {
            AddTaskSelfLink(title, commandName, null, 0);
        }

        public static void AddTaskSelfLink(string title, string commandName, string iconPath)
        {
            AddTaskSelfLink(title, commandName, iconPath, 0);
        }

        public static void AddTaskSelfLink(string title, string commandName, string iconPath, int iconNumber)
        {
            if (string.IsNullOrWhiteSpace(commandName))
                throw new ArgumentNullException("commandName");

            var icon = CreateIconReference(iconPath, iconNumber);

            // Register the command and get the associated number.
            int command = WindowsMessageHelper.RegisterCommand(commandName);
            AddTaskLink(title, Assembly.GetEntryAssembly().Location, WindowsMessageHelper.COMMAND_PREFIX + command.ToString(), icon);
        }

        #endregion

        public static void AddTaskSeparator()
        {
            list.AddUserTasks(new Microsoft.WindowsAPICodePack.Taskbar.JumpListSeparator());
        }

        private static Microsoft.WindowsAPICodePack.Taskbar.JumpListLink CreateJumpListLink(string title, string path, string arguments, Microsoft.WindowsAPICodePack.Shell.IconReference? icon)
        {
            var task = new Microsoft.WindowsAPICodePack.Taskbar.JumpListLink(path, title);
            if (icon.HasValue)
                task.IconReference = icon.Value;
            task.Arguments = arguments;

            return task;
        }

        private static Microsoft.WindowsAPICodePack.Shell.IconReference? CreateIconReference(string iconPath, int iconNumber)
        {
            Microsoft.WindowsAPICodePack.Shell.IconReference? icon = null;
            if (!string.IsNullOrEmpty(iconPath))
                icon = new Microsoft.WindowsAPICodePack.Shell.IconReference(iconPath, iconNumber);

            return icon;
        }

        public static void Refresh()
        {
            list.Refresh();
        }

        public static void Clear()
        {
            list = Microsoft.WindowsAPICodePack.Taskbar.JumpList.CreateJumpList();
            list.ClearAllUserTasks();
            list.Refresh();
            categories = new Dictionary<string, Microsoft.WindowsAPICodePack.Taskbar.JumpListCustomCategory>();
        }

        public static void AddToRecent(string path)
        {
            Microsoft.WindowsAPICodePack.Taskbar.JumpList.AddToRecent(path);
        }

        public static bool ShowRecentFiles
        {
            get
            {
                if (list.KnownCategoryToDisplay == Microsoft.WindowsAPICodePack.Taskbar.JumpListKnownCategoryType.Recent)
                    return true;

                return false;
            }
            set
            {
                if (value)
                    list.KnownCategoryToDisplay = Microsoft.WindowsAPICodePack.Taskbar.JumpListKnownCategoryType.Recent;
                else
                    list.KnownCategoryToDisplay = Microsoft.WindowsAPICodePack.Taskbar.JumpListKnownCategoryType.Neither;
            }
        }

        public static bool ShowFrequentFiles
        {
            get
            {
                if (list.KnownCategoryToDisplay == Microsoft.WindowsAPICodePack.Taskbar.JumpListKnownCategoryType.Frequent)
                    return true;

                return false;
            }
            set
            {
                if (value)
                    list.KnownCategoryToDisplay = Microsoft.WindowsAPICodePack.Taskbar.JumpListKnownCategoryType.Frequent;
                else
                    list.KnownCategoryToDisplay = Microsoft.WindowsAPICodePack.Taskbar.JumpListKnownCategoryType.Neither;
            }
        }
    }
}
