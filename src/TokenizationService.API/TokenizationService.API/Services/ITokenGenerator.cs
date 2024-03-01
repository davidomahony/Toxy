namespace TokenizationService.Core.API.Services
{
    public interface ITokenGenerator
    {
        Task<string> GenerateNewToken(string valueToTokenize, string tokenType);
    }
}
