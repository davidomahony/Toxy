using TokenizationService.Enums.Configuration;

namespace TokenizationService.Core.API.Services
{
    public interface IEncryptionService
    {
        EncryptionType Identifier { get; }

        string EncryptString(string encryptMe, string key, string salt);

        string DecryptString(string decryptMe, string key, string salt);
    }
}
