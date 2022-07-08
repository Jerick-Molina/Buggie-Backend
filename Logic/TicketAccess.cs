using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buggie.DataProperties;
using Buggie.Interface;
namespace  Buggie.Logic{

    
    
    public class TicketAccess {
        
        public readonly IMySqlDataAccess db;

        public TicketAccess(IMySqlDataAccess _db)
        {
            db = _db;
        }

        /*Create Ticket
            Only Admin || Associate can create tickets
        */
      public async Task<List<User>> CreateTicket(string userToken)
        {
            
            string sql = "select * from test";

            var r = await db.LoadData<User,dynamic>(sql,"");

            return r;
        
        }
        
    }
}