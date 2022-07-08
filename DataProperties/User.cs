using System;

namespace Buggie.DataProperties
{
    public class User
    {
        public string Id {get;set;}

        public string FirstName {get;set;}

        public string LastName {get;set;}

        public string Email {get;set;}

        public string Password {get;set;}


        //Assigned Role(Can only be one role. Only Admin will be able to Update Role. Admin cannot edit Admin) : Admin, Associate, Developer
        public string Role {get;set;}


    }
}

