namespace LockSingleBoardController.Services
{
    public interface IGPIOControlService
    {
        void InitLEDLight();
        bool ChangeLockState();
    }
}
