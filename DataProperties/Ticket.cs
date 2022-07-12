using System;
using System.Collections.Generic;
namespace Buggie.DataProperties{
    public class Ticket {


        public string TicketId {get;set;}

      
        public string Name {get;set;} 

        //Condition: Red|yellow|Green
        //Red : Critical
        //Yellow : Moderate
        //Green : Finished
        public string Status {get;set;}

        // Who is assigned to this ticket, based on Id.
        public int AssignedTo {get;set;}

        //When was this ticket created
        public DateTime DateStart {get;set;}

        //When was this ticket Deleted
        public DateTime DateFinished {get;set;}

        //What company created the ticket
        public string CompanyId {get;set;}

        //Who created the ticket
        public string CreatedById {get;set;} 

    }
}