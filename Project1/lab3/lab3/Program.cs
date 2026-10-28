using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;

namespace lab3
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            EnableModernBrowserMode();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        /// <summary>
        /// Controlul WebBrowser foloseste implicit un motor foarte vechi (IE7).
        /// Il setam pe modul IE11 pentru ca site-urile moderne sa se afiseze corect.
        /// </summary>
        private static void EnableModernBrowserMode()
        {
            try
            {
                string exeName = System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION"))
                {
                    if (key != null)
                        key.SetValue(exeName, 11001, RegistryValueKind.DWord);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
