using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSO.Demo
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        public Form1(string data)
        {
            InitializeComponent();
            label1.Text = data + Environment.NewLine;

            //string chk = label1.Text;
            //string file1 = chk.Replace("myapp://", "");
            string filename = "AnyDesk";
            if (filename == "")
            {
                MessageBox.Show("Error");
            }
            else
            {
                try
                {
                    string cmd = filename;
                    MessageBox.Show(cmd);
                    string path = GetApplictionInstallPath(cmd);
                    MessageBox.Show(path);
                    if (path == null || path == "")
                    {
                        Process.Start(cmd);
                    }
                    else
                    {
                        string file = path.Replace("\"", string.Empty).Trim() + "\\" + cmd.Replace("/", string.Empty) + ".exe";
                        ProcessStartInfo info = new ProcessStartInfo(file);
                        MessageBox.Show(info.FileName);
                        Process.Start(info.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void DeleteAllSubKeys()
        {
            RegistryKey parentKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey softwareKey = parentKey.OpenSubKey("SOFTWARE", true).OpenSubKey("Classes",true);
            RegistryKey subKey = softwareKey.CreateSubKey("MyApp");
            var key = subKey.GetSubKeyNames();
            foreach (string i in key)
            {
                subKey.DeleteSubKey(i);
            }
            parentKey.Close();
            softwareKey.Close();
            subKey.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public string GetApplictionInstallPath(string nameOfAppToFind)
        {
            //IntPtr ptr = new IntPtr();
            //bool isWow64FsRedirectionDisabled = Wow64DisableWow64FsRedirection(ref ptr);
            //string a = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "dberr.txt");
            //MessageBox.Show(a);
            //string filecontent = File.ReadAllText(a);
            //MessageBox.Show(filecontent);
            //if (File.Exists(a))
            //{
            //    MessageBox.Show("Accessed successfull");
            //}
            //else
            //{
            //    MessageBox.Show("Access denied");
            //}
            string installedPath;
            string keyName;

            // search in: CurrentUser
            keyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            installedPath = ExistsInSubKey(Registry.CurrentUser, keyName, "DisplayName", nameOfAppToFind);
            if (!string.IsNullOrEmpty(installedPath))
            {
                return installedPath;
            }

            // search in: LocalMachine_32
            keyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            installedPath = ExistsInSubKey(Registry.LocalMachine, keyName, "DisplayName", nameOfAppToFind);
            if (!string.IsNullOrEmpty(installedPath))
            {
                return installedPath;
            }
            // search in: LocalMachine_64
            keyName = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            installedPath = ExistsInSubKey(Registry.LocalMachine, keyName, "DisplayName", nameOfAppToFind);
            if (!string.IsNullOrEmpty(installedPath))
            {
                return installedPath;
            }

            return string.Empty;
        }

        private string ExistsInSubKey(RegistryKey root, string subKeyName, string attributeName, string nameOfAppToFind)
        {
            RegistryKey subkey;
            string displayName;

            using (RegistryKey key = root.OpenSubKey(subKeyName))
            {
                IntPtr ptr = new IntPtr();
                bool isWow64FsRedirectionDisabled = Wow64DisableWow64FsRedirection(ref ptr);
                if (key != null)
                {
                    foreach (string kn in key.GetSubKeyNames())
                    {
                        using (subkey = key.OpenSubKey(kn))
                        {
                            displayName = subkey.GetValue(attributeName) as string;
                            if (nameOfAppToFind.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
                            {
                                return subkey.GetValue("InstallLocation") as string;
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
