using System.Threading.Tasks;
using Buggie.DataProperties;

namespace Buggie.Interface
{
    
    public interface IAccountAuthentication
    {
        

        Task<string> GenerateJwtAccessToken(User user);

        Task<string> GenerateJwtInfoToken(User user);

        string HashPassword(string password);

        bool PasswordAuthentication(string password,string hashedPassword);

        
    }
}