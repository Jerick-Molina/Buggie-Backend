using System.Collections.Generic;
using System.Threading.Tasks;
using Buggie.DataProperties;

namespace Buggie.Interface{
    public interface IUserAccess
    {
       Task<List<User>> GetUsers();

       Task<User> FindUser(User user);
    }
}