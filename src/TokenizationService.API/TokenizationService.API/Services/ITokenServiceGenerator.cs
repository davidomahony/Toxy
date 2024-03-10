namespace TokenizationService.Core.API.Services
{
    public interface ITokenServiceGenerator
    {
        Task<string> GenerateNewToken(string valueToTokenize, string tokenType);
    }
}
