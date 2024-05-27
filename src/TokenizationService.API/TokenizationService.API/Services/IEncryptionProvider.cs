namespace TokenizationService.Core.API.Services
{
    public interface IEncryptionProvider
    {
        Task<string> EncryptString(string input, string tokenType, string clientId);
    }
}
