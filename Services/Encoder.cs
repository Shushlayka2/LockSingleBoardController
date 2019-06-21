using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LockSingleBoardController.Services
{
    public class Encoder : IEncoder
    {
        private readonly Rfc2898DeriveBytes _DeriveBytes;
        private readonly byte[] _InitVectorBytes;
        private readonly byte[] _KeyBytes;

        private const string InitVector = "T=A4rAzu94ez-dra";
        private const int PasswordIterations = 1000;
        private const string SaltValue = "d=?ustAF=UstenAr3B@pRu8=ner5sW&h59_Xe9P2za-eFr2fa&ePHE@ras!a+uc@";
        private const string Password = "asehan57";

        public Encoder()
        {
            var passwordBytes = Encoding.UTF8.GetBytes(Password);
            var saltValueBytes = Encoding.UTF8.GetBytes(SaltValue);

            _DeriveBytes = new Rfc2898DeriveBytes(passwordBytes, saltValueBytes, PasswordIterations);
            _InitVectorBytes = Encoding.UTF8.GetBytes(InitVector);
            _KeyBytes = _DeriveBytes.GetBytes(32);
        }

        public string Decrypt(string encryptedText)
        {
            var encryptedTextBytes = Convert.FromBase64String(encryptedText);
            var plainText = "";
            var rijndaelManaged = new RijndaelManaged { Mode = CipherMode.CBC };

            using (var decryptor = rijndaelManaged.CreateDecryptor(_KeyBytes, _InitVectorBytes))
            {
                using (var memoryStream = new MemoryStream(encryptedTextBytes))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        var plainTextBytes = new byte[encryptedTextBytes.Length];
                        var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                    }
                }
            }
            return plainText;
        }

        public string Encrypt(string plainText)
        {
            string encryptedText;
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var rijndaelManaged = new RijndaelManaged { Mode = CipherMode.CBC };

            using (var encryptor = rijndaelManaged.CreateEncryptor(_KeyBytes, _InitVectorBytes))
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();

                        var cipherTextBytes = memoryStream.ToArray();
                        encryptedText = Convert.ToBase64String(cipherTextBytes);
                    }
                }
            }
            return encryptedText;
        }
    }
}
