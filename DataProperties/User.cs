using System;

namespace Buggie.DataProperties
{
    public class User
    {
        public string UserId {get;set;} = string.Empty;

        public string FirstName {get;set;} = string.Empty;

        public string LastName {get;set;} = string.Empty;

        public string Email {get;set;} = string.Empty;

        public string Password {get;set;} = string.Empty;

        //Assigned Role(Can only be one role. Only Admin will be able to Update Role. Admin cannot edit Admin) : Admin, Associate, Developer
        public string Role {get;set;} = string.Empty;

        public string CompanyId {get;set;} = string.Empty;
    }
}

