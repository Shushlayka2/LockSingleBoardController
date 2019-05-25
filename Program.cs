using LockSingleBoardController.Extensions;
using Unity;

namespace LockSingleBoardController
{
    public class Program
    {
        public IUnityContainer Container { get; } = new UnityContainer();

        static void Main(string[] args)
        {
            new Program().Run();
        }

        public Program()
        {
            Container.AddExtension(new ContainerExtension());
        }

        public void Run()
        {
            Container.Resolve<IStartup>().Start();
        }
    }
}
