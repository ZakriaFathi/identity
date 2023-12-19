

using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Server.Constants;
using Server.Model;

namespace Server.Validator
{
    public class UserCredentialsValidator : IExtensionGrantValidator
    {
        private readonly ILogger<UserCredentialsValidator> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCredentialsValidator(ILogger<UserCredentialsValidator> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public string GrantType => IdentityConstant.GrantType.UserCredentials;

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var email = context.Request.Raw.Get("email");
            var password = context.Request.Raw.Get("password");
            var client_id = context.Request.Raw.Get("client_id");
            var granttype = context.Request.Raw.Get("grant_type");

            if (!granttype.Equals(GrantType))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                _logger.LogInformation("Grant type is incorrect");
                return;
            }

            if (string.IsNullOrEmpty(email))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                _logger.LogInformation("Email is empty.");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                _logger.LogInformation("passeord is empty.");
                return;
            }

            if (string.IsNullOrEmpty(client_id))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                _logger.LogInformation("clientId is empty.");
                return;
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest);
                _logger.LogInformation($"Unable to find user {email}");
                return;
            }

            if (await _userManager.CheckPasswordAsync(user, password) == false)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest);
                _logger.LogInformation($"Unable to find user {password}");
                return;
            }

            context.Result = new GrantValidationResult(
                subject: user.Id,
                authenticationMethod: GrantType,
                   customResponse: new Dictionary<string, object> { }
                );
        }
    }
}
