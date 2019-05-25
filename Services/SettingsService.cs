using System.Configuration;

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
    }
}
