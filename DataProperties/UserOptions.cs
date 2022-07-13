using System;

namespace Buggie.DataProperties
{
    public class UserOptions
    {       

        //Default settings 
        public email  Email {get;set;} = new email()
        {
            AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"
        };
        public password Password {get;set;} = new password()
        {
            
        };

        public class password{
        
            public bool RequireDigit {get;set;} = false;

            public bool RequireLowercase {get;set;} = false;

            public Int16 RequiredLength {get;set;} = 0;
        }
        
        public class email 
        {
            public string AllowedUserNameCharacters {get;set;} = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        }
    }
}

