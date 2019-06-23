using LockSingleBoardController.Services;

namespace LockSingleBoardController
{
    public class Startup : IStartup
    {
        protected IoTService IoTService;

        protected IGPIOControlService GPIOControlService;

        public Startup(IoTService _IoTService, IGPIOControlService _GPIOControlService)
        {
            IoTService = _IoTService;
            GPIOControlService = _GPIOControlService;
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
