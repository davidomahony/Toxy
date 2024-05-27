using System.Security.Cryptography;
using System.Text;
using TokenizationService.Enums.Configuration;

namespace TokenizationService.Core.API.Services
{
    public class RsaEncryptionService : IEncryptionService
    {
        public EncryptionType Identifier => throw new NotImplementedException();

        public string DecryptString(string decryptMe, string key, string salt)
        {
            try
            {
                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(key);

                    byte[] encryptedBytes = Convert.FromBase64String(decryptMe);

                    byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, false);

                    int saltLength = Encoding.UTF8.GetByteCount(salt);
                    byte[] originalBytes = new byte[decryptedBytes.Length - saltLength];
                    Array.Copy(decryptedBytes, saltLength, originalBytes, 0, originalBytes.Length);

                    return Encoding.UTF8.GetString(originalBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during decryption: {ex.Message}");
                throw;
            }
        }

        public string EncryptString(string encryptMe, string key, string salt)
        {
            try
            {
                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(key);

                    byte[] inputBytes = Encoding.UTF8.GetBytes(encryptMe);

                    byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
                    byte[] combinedBytes = new byte[inputBytes.Length + saltBytes.Length];
                    Array.Copy(inputBytes, combinedBytes, inputBytes.Length);
                    Array.Copy(saltBytes, 0, combinedBytes, inputBytes.Length, saltBytes.Length);

                    byte[] encryptedBytes = rsa.Encrypt(combinedBytes, false);

                    return Convert.ToBase64String(encryptedBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during encryption: {ex.Message}");
                throw;
            }
        }
    }
}
