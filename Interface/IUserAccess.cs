using Dapper;
using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buggie.DataProperties;

namespace Buggie.Interface{
    public interface IUserAccess
    {
       Task<List<User>> GetUsers();
    }
}