namespace LockSingleBoardController.Services
{
    public interface IEncoder
    {
        string Decrypt(string encryptedText);

        string Encrypt(string plainText);
    }
}
