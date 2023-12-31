
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Server.Model;

namespace Server.Validator
{
    public class RegistrationResponse : ICustomTokenRequestValidator
    {
        public async Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            var c = context;
            var validatedRequest = c.Result.ValidatedRequest;
            var email = context.Result.ValidatedRequest.Raw.Get("email");
            var password = context.Result.ValidatedRequest.Raw.Get("password");

            var response = new Dictionary<string, object>()
            {
                { "email", email},
                { "password", password },
            };

            context.Result.CustomResponse = response;

        }
    }
}
