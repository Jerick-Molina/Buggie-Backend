using Dapper;
using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buggie.Interface;

namespace Buggie.Database{
 
    //Instead of writing this everytime to connect you just instanciate it once
    public class MySqlDataAccess : IMySqlDataAccess
    {
        string connectString = "server=192.168.3.139;userid=buggie;password=Mixon9090;database=BuggieDB";
        public   async Task<List<T>> LoadData<T, U>(string sql, string parameters)
        {
           
            using (IDbConnection dbConnection = new MySqlConnection(connectString))
            {            
                try
                {
                   var returns = await dbConnection.QueryAsync<T>(sql,parameters);                     
                    return returns.ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return new List<T>();
                }

               
            }
        }

    
        public void SaveData<U>(string sql, U parameters)
        {
            using (IDbConnection dbConnection = new MySqlConnection(connectString))
            {
                try
                {
                    dbConnection.Execute(sql, parameters);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Save Data: " + e.Message);
                }
            }
        }

    }
}