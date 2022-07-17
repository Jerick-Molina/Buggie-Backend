using System;
using System.Collections.Generic;
namespace Buggie.DataProperties{
    public class Ticket {


        public string TicketId {get;set;}

        public string Description {get;set;}

        public string Name {get;set;} 

        //Open or Closed? 
        public string Status {get;set;}

        // Who is assigned to this ticket, based on Id.
        public int AssignedTo {get;set;}

        //When was this ticket created
        public DateTime DateStart {get;set;}

        //What is the priority of the ticket
        public string TicketPriority {get;set;}

        //What company created the ticket
        public int CompanyId {get;set;} 

        //Who created the ticket
        public string CreatedById {get;set;} 

        public string ProjectId {get;set;}

    }
}