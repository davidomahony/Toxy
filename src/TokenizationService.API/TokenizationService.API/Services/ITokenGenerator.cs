namespace TokenizationService.Core.API.Services
{
    public interface ITokenGenerator
    {
        Task<string> GenerateNewToken(string token, string tokenType);
    }
}
