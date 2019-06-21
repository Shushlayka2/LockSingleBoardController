using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LockSingleBoardController.Services
{
    public class IoTService
    {
        private DeviceClient deviceClient;
        private SettingsService settingsService;

        protected GPIOControlService GPIOControlService;
        protected IEncoder Encoder;

        public IoTService(SettingsService settingsService, GPIOControlService _GPIOControlService, IEncoder encoder)
        {
            this.settingsService = settingsService;
            GPIOControlService = _GPIOControlService;
            Encoder = encoder;
            deviceClient = DeviceClient.CreateFromConnectionString(this.settingsService.DeviceConnectionString, TransportType.Mqtt);
            deviceClient.SetMethodHandlerAsync("RegisterDevice", RegisterDevice, null).Wait();
            deviceClient.SetMethodHandlerAsync("SendLockState", SendLockState, null).Wait();
            deviceClient.SetMethodHandlerAsync("ChangeLockState", ChangeLockState, null).Wait();
        }

        protected Task<MethodResponse> RegisterDevice(MethodRequest methodRequest, object userContext)
        {
            string result = null;
            try
            {
                var deviceId = JObject.Parse(methodRequest.DataAsJson)["deviceId"].ToString();
                settingsService.TrustedDevices.Add(deviceId);
                using (var sw = new StreamWriter("list.txt", false, Encoding.Default))
                {
                    var listText = string.Join("\n", settingsService.TrustedDevices);
                    listText = Encoder.Encrypt(listText);
                    sw.Write(listText);
                }
                result = "{\"result\":\"true\"}";
            }
            catch (Exception ex)
            {
                result = "{\"result\":\"false\"}";
            }
            return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));
        }

        protected Task<MethodResponse> ChangeLockState(MethodRequest methodRequest, object userContext)
        {
            var deviceId = JObject.Parse(methodRequest.DataAsJson)["deviceId"].ToString();
            var result = "{\"result\":\"Unauthorized exception\"}";
            if (settingsService.TrustedDevices.Contains(deviceId))
            {
                var isExecuted = GPIOControlService.ChangeLockState();
                if (isExecuted)
                {
                    result = "{\"result\":\"" + settingsService.State + "\"}";
                    return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));
                }
                else
                {
                    result = "{\"result\":\"Internal exception\"}";
                    return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 500));
                }
            }
            return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 401));
        }

        protected Task<MethodResponse> SendLockState(MethodRequest methodRequest, object userContext)
        {
            var deviceId = JObject.Parse(methodRequest.DataAsJson)["deviceId"].ToString();
            var result = "{\"result\":\"Unauthorized exception\"}";
            result = "{\"result\":\"" + settingsService.State + "\"}";
            if (settingsService.TrustedDevices.Contains(deviceId))
            {
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));
            }
            return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 401));
        }
    }
}
