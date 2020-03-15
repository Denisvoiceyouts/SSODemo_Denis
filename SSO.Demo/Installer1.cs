using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSO.Demo
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        public Installer1()
        {
            InitializeComponent();
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {

            base.OnBeforeInstall(savedState);
        }
        protected override void OnCommitted(IDictionary savedState)
        {
            base.OnCommitted(savedState);
            var loc = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var KeyTest = Registry.LocalMachine.OpenSubKey("Software", true).OpenSubKey("Classes", true);
            //MessageBox.Show(KeyTest.ToString());
            RegistryKey key = KeyTest.CreateSubKey("MyApp");
            key.SetValue("URL Protocol", "MyApp");
            key.CreateSubKey(@"shell\open\command").SetValue("", loc + " %1");
        }
        protected override void OnBeforeRollback(IDictionary savedState)
        {
            base.OnBeforeRollback(savedState);
        }
        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);
            //string keyName = @"Software\Microso\Windows\CurrentVersion\Run";
            //using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName, true))
            //{
            //    if (key == null)
            //    {
            //        // Key doesn't exist. Do whatever you want to handle
            //        // this case
            //    }
            //    else
            //    {
            //        key.DeleteValue("MyApp");
            //    }
            //}
        }
        //public override void Uninstall(IDictionary savedState)
        //{
        //    string data = "";
        //    Form1 form = new Form1(data);
        //    form.DeleteAllSubKeys();
        //    Process application = null;
        //    foreach (var process in Process.GetProcesses())
        //    {
        //        if (!process.ProcessName.ToLower().Contains("Zoi2FA")) continue;
        //        application = process;
        //        break;
        //    }
        //    if (application != null && application.Responding)
        //    {
        //        MessageBox.Show("Uninstalling your system");

        //        application.Kill();
        //        base.Uninstall(savedState);
        //    }
        //}
    }
}
