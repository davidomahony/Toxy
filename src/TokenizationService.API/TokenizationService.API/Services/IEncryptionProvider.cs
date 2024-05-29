namespace TokenizationService.Core.API.Services
{
    public interface IEncryptionProvider
    {
        Task<string> EncryptString(string input, string tokenType, string clientId);

        Task<string> DecryptString(string input, string tokenTypeIdentifier, string clientId);
    }
}
