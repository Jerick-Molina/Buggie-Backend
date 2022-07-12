using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buggie.DataProperties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data;
using MySql.Data.MySqlClient;
using Buggie.Interface;
namespace Buggie.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TicketController : ControllerBase{

       private ITicketAccess ticket;
        
      
      public TicketController(ITicketAccess _ticket)
      {
        ticket = _ticket;
      }
      [HttpPost("Create")]
      public async Task<IActionResult> CreateTicket()
      {
            

        return new OkObjectResult("");
      }

      [HttpPost("Find")]
      public async Task<IActionResult> FindTicket()
      {

        return new OkObjectResult("");
      }

      [HttpPost("Find")]
      public async Task<IActionResult> FindTickets()
      {


        return new OkObjectResult("");
      }

      
  
    }
}