using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buggie.DataProperties;
using Buggie.Interface;


namespace  Buggie.Logic{

    public class AccountAccess : IAccountAccess {
        
        public readonly IMySqlDataAccess db;
        public readonly IUserAccess userAcss;
        public IAccountAuthentication authen;
        
        public AccountAccess (IMySqlDataAccess _db, IAccountAuthentication _authen, IUserAccess _userAcss)
        {
           db = _db;
           authen = _authen;
           userAcss = _userAcss;
        }
       public async Task<string[]> AccountCreate(User user){
            string sql = "insert into Users(FirstName,LastName,Email,Password,Role,CompanyId) values (@FirstName,@LastName,@Email,@Password,@Role,@CompanyId)";
            try
            {
                //Checks if user is empty
                if(user.Email != string.Empty &&
                   user.Password != string.Empty &&
                   user.FirstName != string.Empty &&
                   user.LastName != string.Empty
                )
                {
                    //Hashes password before going into the database
                    user.Password = authen.HashPassword(user.Password);
                    //Inserts Info into the Database
                    db.SaveData<User>(sql,user);
                    //Gives user a access token and its 
                    var accesstoken = await authen.GenerateJwtAccessToken(user);
                    
                    var userInfo = await userAcss.FindUser(user);

                    var infoToken = await authen.GenerateJwtInfoToken(userInfo);
                    //Going to return AccessToken and IdentityToken
                    string[] tokens = {accesstoken,infoToken};
                    //returns token
                    return tokens;
                
                }
               
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        public async Task<string[]> AccountSignIn(User user){
            try
            {
                if(user.Email != string.Empty &&
                   user.Password != string.Empty)
                {
                    user.Password = authen.HashPassword(user.Password);
                    var result = await FindAccount(user);
                    //Checks if user exist
                    if(result.Email != string.Empty)
                    {
                       
                        //Checks if passwords are identical
                        if(result.Password == user.Password)
                        {
                            
                            var accessToken = await authen.GenerateJwtAccessToken(result);
                            var infoToken = await authen.GenerateJwtInfoToken(result);

                            string[] tokens = {accessToken,infoToken};
                            return tokens;
                        }
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        

        public async Task<User> FindAccount(User user)
        {
            var sql = $"select * From Users where Email = '{user.Email}'";
            try
            {   
                var result = await db.LoadData<User,dynamic>(sql,"");
                if(result.Count <= 1 && result.Count != 0)
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