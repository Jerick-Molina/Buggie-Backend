using System.Security.Claims;
using System.Threading.Tasks;
using Buggie.DataProperties;

namespace Buggie.Interface
{
    
    public interface IAccountAuthentication
    {
        

        Task<string> GenerateJwtAccessToken(string InfoToken);

        Task<string> GenerateJwtInfoToken(User user);

        string HashPassword(string password);

        bool PasswordAuthentication(string password,string hashedPassword);

        User ReadJwtAccessToken(string token);

        User ReadJwtInfoToken(string token);

        bool IsUserRoleValid(User user,string[] roles);

        User ReadTokens(ClaimsIdentity identity);
    }
}