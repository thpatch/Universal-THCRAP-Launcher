using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Universal_THCRAP_Launcher
{
    public static class I18N
    {
        public static readonly string I18NDir = Directory.GetCurrentDirectory() + @"\..\i18n\utl\";

        public static dynamic LangResource { get; private set; }


        public static int LangNumber()
        {
            if (Directory.Exists(I18NDir)) return Directory.GetFiles(I18NDir).Length;

            return 0;
        }

        private static dynamic GetLangResource(string filePath)
        {
            string raw = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject(raw);
        }

        public static void UpdateLangResource(string filePath)
        {
            try
            {
                LangResource = GetLangResource(filePath);
                Configuration.Lang = filePath.Replace(I18NDir, "");
            }
            catch (JsonReaderException e)
            {
                MessageBox.Show(e.Message, @"JSON Parser Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}
