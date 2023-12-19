

using IdentityServer4.AspNetIdentity;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Server.Model;
using System.Security.Claims;

namespace Server.Validator
{
    public class ApplicationProfileService : ProfileService<ApplicationUser>
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;

        public ApplicationProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory) : base(userManager, claimsFactory)
        {
            _claimsFactory = claimsFactory;
        }

        protected override async Task GetProfileDataAsync(ProfileDataRequestContext context, ApplicationUser user)
        {
            var ip = context.ValidatedRequest?.Raw["Age"];
            var deviceId = context.ValidatedRequest?.Raw["Name"];

            var newclin = await _claimsFactory.CreateAsync(user);
            context.IssuedClaims.Add(new Claim("Name", user.UserName));
            
            var newclina = await _claimsFactory.CreateAsync(user);
            context.IssuedClaims.Add(new Claim("Age", "30"));


            if (deviceId != null)
                context.IssuedClaims.Add(new Claim("device_id", deviceId));

            if (ip != null)
                context.IssuedClaims.Add(new Claim("ip", ip));
            


        }
        protected override async Task IsActiveAsync(IsActiveContext context, ApplicationUser user)
        {
            var a = context;
            var b = user;
            context.IsActive = true;
        }

    }
}
