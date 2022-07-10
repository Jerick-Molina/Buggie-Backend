using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buggie.DataProperties;
using Buggie.Interface;


namespace  Buggie.Logic{

    public class AccountAccess : IAccountAccess {
        
        public readonly IMySqlDataAccess db;
        public readonly IUserAccess userDb;
        public IAccountAuthentication acc;
        
        public AccountAccess (IMySqlDataAccess _db, IAccountAuthentication _acc, IUserAccess _userDb)
        {
           db = _db;
           acc = _acc;
           userDb = _userDb;
        }
        public async Task<string> AccountCreate(User user){
            string sql = "insert into Users(FirstName,LastName,Email,Password,Role) values (@FirstName,@LastName,@Email,@Password,@Role)";
            try
            {
                //Checks if user is empty
                if(user.Email != string.Empty &&
                   user.Password != string.Empty 
                )
                {
                var result = await userDb.FindUser(user);
                if(result.Email == null)
                 {
                    //Hashes password before going into the database
                    user.Password = acc.HashPassword(user.Password);
                    //Inserts Info into the Database
                    db.SaveData<User>(sql,user);
                    //Gives user a access token
                    var token = await acc.GenerateJwtAccessToken(user);
                    //returns token
                    return token;
                }
                }else
                {   
                
                    return "Empty";
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Message);
            }
            return "Error";
        }
         public async Task<string> AccountSignIn(User user){
            try
            {
                if(user.Email != string.Empty &&
                   user.Password != string.Empty)
                {
                    user.Password = acc.HashPassword(user.Password);
                    var result = await userDb.FindUser(user);
                    //Checks if user exist
                    if(result.Email != null)
                    {
                        user.Password = acc.HashPassword(user.Password);
                        //Checks if passwords are identical
                        if(result.Password == user.Password)
                        {
                            
                            var token = await acc.GenerateJwtAccessToken(user);

                            return token;
                        }
                    }
                    return "Invalid";
                }
                return "Empty";
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "Error";
        }
        
    }
}