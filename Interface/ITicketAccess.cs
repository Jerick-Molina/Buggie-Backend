using System.Collections.Generic;
using System.Threading.Tasks;
using Buggie.DataProperties;

namespace Buggie.Interface{
    public interface ITicketAccess
    {
        
         Task<bool> CreateTicket(Ticket ticket);
        Task<List<Ticket>> SearchTicketsByCompany(int companyId);
    }
}