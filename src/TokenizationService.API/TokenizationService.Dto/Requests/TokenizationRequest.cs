﻿namespace TokenizationService.Core.API.Models
{
    public class TokenizationRequest
    {
        public TokenizationInformation[] TokenizationRequestInformation { get; set; }

        public string ClientId { get; set; }
    }

    public class TokenizationInformation
    {
        public string TokenValue { get; set; }

        public string TokenIdentifier { get; set; }
    }
}
