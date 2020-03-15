using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSO.Demo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ////get running location
            //var loc = System.Reflection.Assembly.GetExecutingAssembly().Location;
            ////MARK: Register location in registry
            //var KeyTest = Registry.LocalMachine.OpenSubKey("Software", true).OpenSubKey("Classes", true);
            //RegistryKey key = KeyTest.CreateSubKey("MyApp");
            //key.SetValue("URL Protocol", "MyApp");
            //string p1 = System.IO.Path.GetDirectoryName(loc);
            //key.CreateSubKey(@"shell\open\command").SetValue("", p1 + "/SSO.Demo.exe %1");
            Uri uri = null;
            if (args.Length > 0)
            {
                // a URI was passed and needs to be handled
                try
                {
                    uri = new Uri(args[0].Trim());
                    Application.Run(new Form1(uri.ToString()));
                }
                catch (UriFormatException)
                {
                    Console.WriteLine("Invalid URI.");
                }
            }
            else
            {
                Application.Run(new Form1(""));
            }
        }
    }
}
