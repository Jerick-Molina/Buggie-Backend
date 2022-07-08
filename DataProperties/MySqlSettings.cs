using System;
using System.Collections.Generic;
namespace Buggie.DataProperties{
    public class DatabaseSettings {

     
        public string Host {get;set;}

     
        public string Port {get;set;} 


        public string User {get;set;}

        public string Password {get;set;}

        //Connecition to mySql database
        public string ConnectionString 
        {
           get{
             return "";
           }
        }

    }
}