//using System;
using Microsoft.Win32;
using System.Diagnostics;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.SqlServer.Server;
//using static System.Net.Mime.MediaTypeNames;

namespace Power_Plan_Unlocker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            { // Unlocker
                // Power plans
                //get ultimate plan
                Process.Start("powercfg", "-duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61");
                RegistryKey key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Power\\User\\PowerSchemes", false);
                string[] subkeys = key.GetSubKeyNames();
                foreach (string i in subkeys)
                { // checks if subkey is an actual scheme
                    key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Power\\User\\PowerSchemes\\" + i, false);
                    string[] subsubkeys = key.GetSubKeyNames();
                    if (subsubkeys.Length != 0)
                        foreach (string j in subsubkeys)
                        { // checks if subkeys of each power scheme have subkeys of their own, if so, set them so the scheme can be visible
                            key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Power\\User\\PowerSchemes\\" + i + "\\" + j, true);
                            string[] subsubsubkeys = key.GetSubKeyNames();
                            if (subsubsubkeys.Length == 0)
                            {
                                //actually set the values
                                key.SetValue("ACSettingIndex", 2, RegistryValueKind.DWord);
                                key.SetValue("DCSettingIndex", 2, RegistryValueKind.DWord);
                            }
                        }
                }

                //power options
                key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Power\\PowerSettings", false);
                subkeys = key.GetSubKeyNames();
                foreach (string i in subkeys)
                { // checks if subkey is an actual scheme
                    key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Power\\PowerSettings\\" + i, false);
                    string[] subsubkeys = key.GetSubKeyNames();
                    foreach (string j in subsubkeys)
                    { // checks if subkeys of each power scheme have subkeys of their own, if so, set them so the scheme can be visible
                        key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Power\\PowerSettings\\" + i + "\\" + j, true);
                        string[] subsubsubkeys = key.GetSubKeyNames();
                        if (subsubsubkeys.Length >= 0)
                        {
                            //actually set the values
                            key.SetValue("Attributes", 2, RegistryValueKind.DWord);
                        }
                    }
                }
            }
        }
    }
}
