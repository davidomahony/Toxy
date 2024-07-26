using System.Security.Cryptography;
using System.Text;
using TokenizationService.Enums.Configuration;

namespace TokenizationService.Core.API.Services.EncryptionServices
{
    public class AesEncryptionService : IEncryptionService
    {
        public EncryptionType Identifier => EncryptionType.AES;

        public string DecryptString(string decryptMe, string key, string salt)
        {
            var aesProvider = new AesCryptoServiceProvider();

            aesProvider.Key = ConvertToByteArray(key, 32);
            aesProvider.Mode = CipherMode.CBC;
            aesProvider.Padding = PaddingMode.PKCS7;
            // Set the block size to 128 bits for AES
            aesProvider.BlockSize = 128;
            aesProvider.GenerateIV();

            var ivString = decryptMe.Substring(0, 24); // IV is 16 bytes long, but 24 characters in base64
            var encryptedDataString = decryptMe.Substring(24);

            var iv = Convert.FromBase64String(ivString);
            var encryptedData = Convert.FromBase64String(encryptedDataString);

            using (var decryptor = aesProvider.CreateDecryptor(aesProvider.Key, iv))
            using (var ms = new MemoryStream(encryptedData))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }

        public string EncryptString(string encryptMe, string key, string salt)
        {
            var aesProvider = new AesCryptoServiceProvider();

            aesProvider.Key = ConvertToByteArray(key, 32);
            aesProvider.Mode = CipherMode.CBC;
            aesProvider.Padding = PaddingMode.PKCS7;
            // Set the block size to 128 bits for AES
            aesProvider.BlockSize = 128;
            aesProvider.GenerateIV();
            var iv = aesProvider.IV;
            using (var encryptor = aesProvider.CreateEncryptor(aesProvider.Key, iv))
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(encryptMe);
                }
                var encryptedData = ms.ToArray();
                var result = Convert.ToBase64String(iv) + Convert.ToBase64String(encryptedData);
                return result;
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
