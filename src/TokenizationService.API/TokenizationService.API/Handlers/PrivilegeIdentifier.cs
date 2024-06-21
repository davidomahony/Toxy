using Microsoft.AspNetCore.Authorization;

namespace TokenizationService.Core.API.Handlers
{
    public class PrivilegeIdentifier : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Scope { get; }

        public PrivilegeIdentifier(string scope, string issuer)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }
}