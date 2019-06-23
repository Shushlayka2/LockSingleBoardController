using LockSingleBoardController.Extensions;
using System;
using System.IO;
using System.Text;

namespace LockSingleBoardController.Services
{
    public class GPIOControlService : IGPIOControlService
    {
        private SettingsService settingsService;

        public GPIOControlService(SettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public bool ChangeLockState()
        {
            var isExecuted = false;
            try
            {
                if (settingsService.State == "Opened")
                {
                    Close();
                }
                else if (settingsService.State == "Closed")
                {
                    Open();
                }
                isExecuted = true;
            }
            catch (Exception ex)
            {
            }
            return isExecuted;
        }

        protected void Open()
        {
            using (StreamReader sr = new StreamReader("Scripts/OpenLockScript.txt", Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line.Bash();
                }
            }
            settingsService.State = "Opened";
        }

        protected void Close()
        {
            using (StreamReader sr = new StreamReader("Scripts/CloseLockScript.txt", Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line.Bash();
                }
            }
            settingsService.State = "Closed";
        }
    }
}
