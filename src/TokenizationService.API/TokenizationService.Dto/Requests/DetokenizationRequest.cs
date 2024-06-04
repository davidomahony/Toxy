namespace TokenizationService.Core.API.Models
{
    public class DetokenizationRequest
    {
        public DetokenizationInformation[] DetokenizationRequestInformation { get; set; }

        public string ClientId { get; set; }
    }

    public class DetokenizationInformation
    {
        public string Token { get; set; }

        public string TokenIdentifier { get; set; }
    }
}
