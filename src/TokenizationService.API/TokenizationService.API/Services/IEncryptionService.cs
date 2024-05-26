namespace TokenizationService.Core.API.Services
{
    public interface IEncryptionService
    {
        string Identifier { get; }

        string EncryptString(string encryptMe, string tokenType);

        string DecryptString(string decryptMe, string tokenType);
    }
}
