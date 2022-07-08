using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buggie.DataProperties;
using Buggie.Interface;
namespace  Buggie.Logic{

    public class AccountAccess {
        
        public readonly IMySqlDataAccess db;


        public AccountAccess (IMySqlDataAccess _db)
        {
            db = _db;
        }
        public async Task<List<User>> Account_Create(){
            
            return new List<User>();
        }
         public async Task<List<User>> Account_SignIn(){
            
            return new List<User>(); 
        }
        
    }
}