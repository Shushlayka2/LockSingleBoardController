using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace LockSingleBoardController.Services
{
    public class SettingsService
    {
        public string State
        {
            get
            {
                return ConfigurationManager.AppSettings["LockState"];
            }
            set
            {
                ConfigurationManager.AppSettings["LockState"] = value;
            }
        }

        public string DeviceConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["DeviceConnectionString"];
            }
        }

        public List<string> TrustedDevices { get; set; }

        public SettingsService(IEncoder encoder)
        {
            if (File.Exists("list.txt"))
            {
                using (var sr = new StreamReader("list.txt"))
                {
                    var list = sr.ReadToEnd();
                    list = encoder.Decrypt(list);
                    TrustedDevices = list.Split("\n").ToList();
                }
            }
            else
            {
                TrustedDevices = new List<string>();
            }
        }
    }
}
