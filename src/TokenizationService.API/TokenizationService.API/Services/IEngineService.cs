﻿using TokenizationService.API.Repositories;
using TokenizationService.Core.API.Models;

namespace TokenizationService.Core.API.Services
{
    public interface IEngineService
    {
        Task<TokenizationInformation[]> GenerateTokens(TokenizationInformation[] values);

        Task<DetokenizationInformation[]> FetchTokenValuesAsync(DetokenizationInformation[] tokens);
    }
}
