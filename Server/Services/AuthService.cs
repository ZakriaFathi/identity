using Microsoft.AspNetCore.Identity;
using Server.Model;

namespace Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                GivenName = model.GivenName,
                FamilyName = model.FamilyName,
                Email = model.Email,
                UserName = model.UserName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }
                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");

            return new AuthModel
            {
                Email = model.Email,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                UserName = user.UserName,
            };
        }

        //public async Task<AuthModel> SingInAsync(SingInModel model)
        //{
        //    var authModel = new AuthModel();

        //    var user = await _userManager.FindByEmailAsync(model.Email);

        //    if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        //    {
        //        authModel.Message = "Email or Password is incorrect!";
        //        return authModel;
        //    }

        //    var rolesList = await _userManager.GetRolesAsync(user);

        //    authModel.IsAuthenticated = true;
        //    authModel.Email = user.Email;
        //    authModel.UserName = user.UserName;
        //    authModel.Roles = rolesList.ToList();

        //    return authModel;
        //}

        //public async Task<string> AddRoleAsync(AddRoleModel model)
        //{
        //    var user = await _userManager.FindByIdAsync(model.UserId);

        //    if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
        //        return "Invalid user ID or Role";

        //    if (await _userManager.IsInRoleAsync(user, model.Role))
        //        return "User already assigned to this role";

        //    var result = await _userManager.AddToRoleAsync(user, model.Role);

        //    return result.Succeeded ? string.Empty : "Sonething went wrong";
        //}
    }
}
