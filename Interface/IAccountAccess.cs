
using System.Collections.Generic;
using System.Threading.Tasks;
using Buggie.DataProperties;

namespace Buggie.Interface{

    public interface IAccountAccess
    {


      Task<string> AccountCreate(User user);
    }
}