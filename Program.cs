using System;
using System.Threading;
using System.Device.Gpio;

namespace LockSingleBoardController
{
    public class Program
    {
        public int GPIO = 7;

        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            using (var controller = new GpioController())
            {
                controller.OpenPin(GPIO, PinMode.Output);

                Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
                {
                    controller.Dispose();
                };

                while (true)
                {
                    controller.Write(GPIO, PinValue.High);
                    Thread.Sleep(1000);
                    controller.Write(GPIO, PinValue.Low);
                    Thread.Sleep(200);
                }

            }
        }
    }
}
