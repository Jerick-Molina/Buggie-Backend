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



    }
}