namespace TokenizationService.Core.API.Models
{
    public class DetokenizationRequest
    {
        public DetokenizationInformation[] DetokenizationRequestInformation { get; set; }
    }

    public class DetokenizationInformation
    {
        public string Value { get; set; }

        public int Identifier { get; set; }
    }
}
