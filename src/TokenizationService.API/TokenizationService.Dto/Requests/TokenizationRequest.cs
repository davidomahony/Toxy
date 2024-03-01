namespace TokenizationService.Core.API.Models
{
    public class TokenizationRequest
    {
        public TokenizationInformation[] TokenizationRequestInformation { get; set; }
    }

    public class TokenizationInformation
    {
        public string Value { get; set; }

        public string Identifier { get; set; }
    }
}
