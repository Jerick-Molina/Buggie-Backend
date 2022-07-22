
using System.Collections.Generic;
using System.Threading.Tasks;
using Buggie.DataProperties;

namespace Buggie.Interface{

    public interface IAccountAccess
    {


      Task<string> AccountCreate(User user);

       Task<object[]> AccountSignIn(User user);

       Task<User> FindAccount(User user);

       Task<List<User>> FindUsersByCompanyId(int companyId);
    }
}