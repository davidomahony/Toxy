using System.Security.Cryptography;
using System.Text;
using TokenizationService.Enums.Configuration;

namespace TokenizationService.Core.API.Services.EncryptionServices
{
    public class DesEncryptionService : IEncryptionService
    {
        public EncryptionType Identifier => EncryptionType.DES;

        public string DecryptString(string decryptMe, string key, string salt)
        {
            try
            {
                using (var des = new DESCryptoServiceProvider())
                {
                    des.Key = ConvertToByteArray(key, 8);
                    des.IV = ConvertToByteArray(salt, 8);

                    byte[] encryptedBytes = Convert.FromBase64String(decryptMe);

                    ICryptoTransform decryptor = des.CreateDecryptor();

                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                    return Encoding.UTF8.GetString(decryptedBytes);
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
                using (var des = new DESCryptoServiceProvider())
                {
                    des.Key = ConvertToByteArray(key, 8);
                    des.IV = ConvertToByteArray(salt, 8);

                    byte[] inputBytes = Encoding.UTF8.GetBytes(encryptMe);

                    ICryptoTransform encryptor = des.CreateEncryptor();

                    byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                    return Convert.ToBase64String(encryptedBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during encryption: {ex.Message}");
                throw;
            }
        }


        public static byte[] ConvertToByteArray(string str, int size)
        {
            byte[] bytes = new byte[size];
            bytes = Encoding.UTF8.GetBytes(str.PadRight(bytes.Length));
            return bytes.Take(size).ToArray();
        }
    }
}
