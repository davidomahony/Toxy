namespace TokenizationService.Core.API.Models
{
    public class TokenizationRequest
    {
        public TokenizationInformation[] TokenizationRequestInformation { get; set; }
    }

    public class TokenizationInformation
    {
        public string Value { get; set; }

        public int Identifier { get; set; }
    }
}
