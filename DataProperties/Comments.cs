using System;
using System.Collections.Generic;
namespace Buggie.DataProperties{
    public class Comments 
    {   

        
        public string CompanyId {get;set;}

        //Which project this belongs to
        public string ProjectId {get;set;}
        
        //Which Ticket this belongs to
        public string TicketId {get;set;}

        //Who created the comment
        public string CreatorId {get;set;}

        //What the comment says
        public string Message {get;set;}

        //the date comment was created
        public DateTime DateCreated {get;set;}
    }
}
    