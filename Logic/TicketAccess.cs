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
      public async Task CreateTicket(string userToken,Ticket ticket)
        {
            
            string sql = "select * from test";

             db.SaveData<Ticket>(sql,ticket);
        }
    
      public async Task<List<Ticket>> SearchTicketsByCompany(Company company)
      {

            string sql = $"select * from Tickets where CompanyId =  '{company.CompanyId}'";
          try
          {
                return await db.LoadData<Ticket,dynamic>(sql,"");

          }catch(Exception e)
          {
              Console.WriteLine(e.Message);
          }

          return new List<Ticket>();
      }

      public async Task<Ticket> SearchTicketById(Ticket ticket)
      {

            string sql = $"select * from Tickets where TicketId = '{ticket.TicketId}'";
          try
          {
            var result = await db.LoadData<Ticket,dynamic>(sql,"");
                  if(result.Count <= 1 && result.Count != 0)
                {   
                    return result[0];
                }
          }catch(Exception e)
          {

          }


        return new Ticket();
      }
    }
}