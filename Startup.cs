using LockSingleBoardController.Extensions;
using LockSingleBoardController.Services;
using System.IO;
using System.Text;

namespace LockSingleBoardController
{
    public class Startup : IStartup
    {
        protected IoTService IoTService;

        public Startup(IoTService ioTService)
        {
            IoTService = ioTService;
        }

        public void Start()
        {
            using (StreamReader sr = new StreamReader("Scripts/InitLedScript.txt", Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line.Bash();
                }
            }
            while (true)
            {
            }
        }
    }
}
