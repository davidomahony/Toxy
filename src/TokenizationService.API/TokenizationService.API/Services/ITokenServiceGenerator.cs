namespace TokenizationService.Core.API.Services
{
    public interface ITokenServiceGenerator
    {
        string Identifier { get; }

        Task<string> GenerateNewToken(string valueToTokenize, string tokenType);
    }
}
