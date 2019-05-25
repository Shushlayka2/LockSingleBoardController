using LockSingleBoardController.Services;
using Unity;
using Unity.Extension;

namespace LockSingleBoardController.Extensions
{
    public class ContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
#if DEBUG
            Container.AddExtension(new Diagnostic());
#endif
            Container.RegisterType<IGPIOControlService, GPIOControlService>();
            Container.RegisterType<SettingsService>();
            Container.RegisterType<IoTService>();
            Container.RegisterType<IStartup, Startup>();
        }
    }
}
