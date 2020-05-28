using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Identity.Server.Controllers
{
    internal interface IIdentityService
    {
        Task PasswordSignInAsync(string username, string password, bool v1, bool v2);
        Task CreateAsync(IdentityUser user, string password);
    }
}