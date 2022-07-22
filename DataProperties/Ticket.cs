using System;
using System.Collections.Generic;
namespace Buggie.DataProperties{
    public class Ticket {


        public int TicketId {get;set;}


        public string Name {get;set;} = string.Empty;

        public string Description {get;set;} = string.Empty;

        //Open or Closed? 
        public string Status {get;set;} = string.Empty;

        // Who is assigned to this ticket, based on Id.
        public int AssignedTo {get;set;}

        //When was this ticket created
        public DateTime DateStart {get;set;} = DateTime.Now;

        //What is the priority of the ticket
        public string Priority {get;set;} = string.Empty;

        //What company created the ticket
        public int CompanyId {get;set;} 

        //Who created the ticket
        public int CreatedById {get;set;}  

        public int ProjectId {get;set;}

    }
}