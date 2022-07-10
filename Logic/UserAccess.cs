using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buggie.DataProperties;
using Buggie.Interface;

namespace  Buggie.Logic{

    public class UserAccess : IUserAccess
    {
        
        public readonly IMySqlDataAccess db;
        
        public UserAccess(IMySqlDataAccess _db)
        {
            db = _db;
        }

        //This will be used to get 
        public async Task<List<User>> GetUsers()
        {
            
            string sql = "select * from test";

            var r = await db.LoadData<User,dynamic>(sql,"");

            return r;
        
        }

        public async Task<User> FindUser(User user)
        {
            var sql = $"select Email From Users where Email = '{user}'";
            try
            {   
                var result = await db.LoadData<User,dynamic>(sql,"");
                if(result.Count > 0)
                {   
                    return result[0];
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new User();
        }

    }
}