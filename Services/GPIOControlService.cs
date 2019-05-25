using LockSingleBoardController.Extensions;
using System;
using System.IO;
using System.Text;

namespace LockSingleBoardController.Services
{
    public class GPIOControlService : IGPIOControlService
    {
        //protected int GPIO = 7;

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
            using (StreamReader sr = new StreamReader("Scripts\\OpenLedScript.txt", Encoding.Default))
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
            using (StreamReader sr = new StreamReader("Scripts\\CloseLedScript.txt", Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line.Bash();
                }
            }
            settingsService.State = "Closed";
        }

        //public void Test()
        //{
        //    using (var controller = new GpioController())
        //    {
        //        controller.OpenPin(GPIO, PinMode.Output);

        //        Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
        //        {
        //            controller.Dispose();
        //        };

        //        while (true)
        //        {
        //            controller.Write(GPIO, PinValue.High);
        //            Thread.Sleep(1000);
        //            controller.Write(GPIO, PinValue.Low);
        //            Thread.Sleep(200);
        //        }

        //    }
        //}
    }
}
