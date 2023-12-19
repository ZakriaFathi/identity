
using Server.Model;

namespace Server.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        //Task<AuthModel> SingInAsync(SingInModel model);
        //Task<string> AddRoleAsync(AddRoleModel model);
    }
}
