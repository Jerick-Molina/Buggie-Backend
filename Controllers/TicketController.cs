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
using System.Security.Claims;

namespace Buggie.Controllers
{

    [ApiController]
    [Route("API/[controller]")]
    [Authorize]
    public class TicketController : ControllerBase{

         IUserAccess user;
         IAccountAccess acc;
         ICompanyAccess com;

         ITicketAccess tkt;
         IProjectAccess proj;
         IAccountAuthentication authen;
       
        public TicketController(IUserAccess  _user,IAccountAccess _acc, 
        ICompanyAccess _com,IAccountAuthentication _authen,
        ITicketAccess _tkt,IProjectAccess _proj)
        {
            com = _com;
            user = _user;
            acc = _acc;
            authen = _authen;
            tkt = _tkt;
            proj = _proj;
        }
      [HttpPost("Create")]
      public async Task<IActionResult> CreateTicket([FromHeader] string Authorization,[FromHeader] string ProjectId,[FromBody] Ticket ticket)
      {
            string[] validRoles = {"Admin","Manager"};
            var identity = authen.ReadTokens(HttpContext.User.Identity as ClaimsIdentity);
            var isValid  = authen.IsUserRoleValid(identity,validRoles);

            //Checks if user has a valid role to be using this api.
            if(isValid == false) return new UnauthorizedObjectResult("Role is not valid");
            if(!int.TryParse(ProjectId, out int parsedInt))  return new UnauthorizedObjectResult("Invalid ProjectId");
            ticket.ProjectId = parsedInt;
            ticket.CompanyId = identity.CompanyId;
            ticket.CreatedById = identity.UserId;
            var result = await tkt.CreateTicket(ticket);
            if(result == false) return new ConflictObjectResult("Could not create token");

            return new OkObjectResult("Success");
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