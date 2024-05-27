﻿namespace TokenizationService.Core.API.Models
{
    public class DetokenizationRequest
    {
        public DetokenizationInformation[] DetokenizationRequestInformation { get; set; }

        public string ClientId;
    }

    public class DetokenizationInformation
    {
        public string Value { get; set; }

        public string Identifier { get; set; }
    }
}
