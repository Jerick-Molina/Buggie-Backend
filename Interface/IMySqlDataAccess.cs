using Dapper;
using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buggie.Interface{
    public interface IMySqlDataAccess
    {
        Task<List<T>> LoadData<T,U>(string sql,string parameters);
        void SaveData<U>(string sql, U parameters);
    }
}