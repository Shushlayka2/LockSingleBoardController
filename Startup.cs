using LockSingleBoardController.Extensions;
using LockSingleBoardController.Services;
using System.IO;
using System.Text;

namespace LockSingleBoardController
{
    public class Startup : IStartup
    {
        private SettingsService settingsService;

        protected IoTService IoTService;

        public Startup(IoTService _IoTService, SettingsService settingsService)
        {
            IoTService = _IoTService;
            this.settingsService = settingsService;
        }

        public void Start()
        {
            InitLEDLight();
            while (true)
            {
            }
        }

        private void InitLEDLight()
        {
            using (StreamReader sr = new StreamReader("Scripts/InitLedScript.txt", Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line.Bash();
                }
            }
            if (settingsService.State == "Opened")
            {
                using (StreamReader sr = new StreamReader("Scripts/OpenLockScript.txt", Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line.Bash();
                    }
                }
            }
            else if (settingsService.State == "Closed")
            {
                using (StreamReader sr = new StreamReader("Scripts/CloseLockScript.txt", Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line.Bash();
                    }
                }
            }
        }
    }
}
