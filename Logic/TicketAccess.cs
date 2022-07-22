using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buggie.DataProperties;
using Buggie.Interface;
namespace  Buggie.Logic{

    
    
    public class TicketAccess  : ITicketAccess{
        
        public readonly IMySqlDataAccess db;

        public TicketAccess(IMySqlDataAccess _db)
        {
            db = _db;
        }

        /*Create Ticket
            Only Admin || Associate can create tickets
        */
      public async Task<bool> CreateTicket(Ticket ticket)
        {
            //MySQL: Creates tickets
            string sql = "insert into Tickets (Name,Description,Status,AssignedTo,DateStart,Priority,CompanyId,CreatedById,ProjectId) values (@Name,@Description,@Status,@AssignedTo,@DateStart,@Priority,@CompanyId,@CreatedById,@ProjectId) ";

             try
             {
                db.SaveData<Ticket>(sql,ticket);

              return true;
             }catch(Exception e)
             {

             }

             return false;
        }

      public async Task EditTicket(string userToken, Ticket ticket)
      {

        
      }
      public async Task<List<Ticket>> SearchTicketsByCompany(int companyId)
      {

            string sql = $"select * from Tickets where CompanyId = {companyId.ToString()}";
          try
          {

            var results =  await db.LoadData<Ticket,string>(sql,"");
              if(results.Count > 0)  return results;
                

          }catch(Exception e)
          {
              Console.WriteLine(e.Message);
          }

          return new List<Ticket>();
      }

      public async Task<Ticket> SearchTicketsByProject(int projectId,int companyId)
      {

            string sql = $"select * from Tickets where ProjectId,CompanyId = {projectId},{companyId}";
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