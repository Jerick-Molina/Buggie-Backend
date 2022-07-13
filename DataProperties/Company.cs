using System;
using System.Collections.Generic;

namespace Buggie.DataProperties{
    public class Company {

        //Company-Id
        public int CompanyId {get;set;}

        //Name of Company
        public string Name {get;set;} = string.Empty;


        //Code to join company && Company code can be changed only by admin
        public string CompanyCode {get;set;} = string.Empty;


    }
}