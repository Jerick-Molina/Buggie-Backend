using System;
using System.Collections.Generic;
namespace Buggie.DataProperties{
    public class Ticket {


        public string Id {get;set;}

        //Name of Ticket
        public string Name {get;set;} 

        //Condition: Red|yellow|Green
        //Red : Critical
        //Yellow : Moderate
        //Green : Finished
        public string Condition {get;set;}

        // Who is assigned to this ticket, based on Id.
        public List<string> AssignedTo {get;set;}

        //When was this ticket created
        public DateTime DateStart {get;set;}

        //When was this ticket Deleted
        public DateTime DateFinished {get;set;}

        //What company created the ticket(for queue purposes)
        public string Company {get;set;}

        //Who created the ticket
        public User Creator {get;set;} 

    }
}