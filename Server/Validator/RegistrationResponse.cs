
using IdentityServer4.Validation;

namespace Server.Validator
{
    public class RegistrationResponse : ICustomTokenRequestValidator
    {
        public async Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            var c = context;
            var t = c.Result;
            var customResponse = c.Result.CustomResponse;
            var validatedRequest = c.Result.ValidatedRequest;
            var error = c.Result.Error;
            var isError = c.Result.IsError;
            var response = new Dictionary<string, object>()
        {
            { "name", "zakria"},
            { "age", "25" },
        };

            context.Result.CustomResponse = response;

        }
    }
}
