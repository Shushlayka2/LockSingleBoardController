using LockSingleBoardController.Services;

namespace LockSingleBoardController
{
    public class Startup : IStartup
    {
        private SettingsService settingsService;

        protected IoTService IoTService;

        protected IGPIOControlService GPIOControlService;

        public Startup(IoTService _IoTService, IGPIOControlService _GPIOControlService, SettingsService settingsService)
        {
            IoTService = _IoTService;
            this.settingsService = settingsService;
        }

        public void Start()
        {
            GPIOControlService.InitLEDLight();
            while (true)
            {
            }
        }
    }
}
