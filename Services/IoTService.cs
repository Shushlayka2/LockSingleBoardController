using Microsoft.Azure.Devices.Client;
using System.Text;
using System.Threading.Tasks;

namespace LockSingleBoardController.Services
{
    public class IoTService
    {
        private DeviceClient deviceClient;
        private SettingsService settingsService;

        protected GPIOControlService GPIOControlService;

        public IoTService(SettingsService settingsService, GPIOControlService _GPIOControlService)
        {
            this.settingsService = settingsService;
            GPIOControlService = _GPIOControlService;
            deviceClient = DeviceClient.CreateFromConnectionString(this.settingsService.DeviceConnectionString, TransportType.Mqtt);
            deviceClient.SetMethodHandlerAsync("SendLockState", SendLockState, null).Wait();
            deviceClient.SetMethodHandlerAsync("ChangeLockState", ChangeLockState, null).Wait();
        }

        protected Task<MethodResponse> ChangeLockState(MethodRequest methodRequest, object userContext)
        {
            var isExecuted = GPIOControlService.ChangeLockState();
            if (isExecuted)
            {
                var result = "{\"result\":\"" + settingsService.State + "\"}";
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));
            }
            else
            {
                var result = "{\"result\":\"Internal exception\"}";
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 500));
            }
        }

        protected Task<MethodResponse> SendLockState(MethodRequest methodRequest, object userContext)
        {
            var result = "{\"result\":\"" + settingsService.State + "\"}";
            return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));
        }
    }
}
