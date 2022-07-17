using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buggie.DataProperties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data;
using MySql.Data.MySqlClient;
using Buggie.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Buggie.Controllers
{
    //All the extras apis that dont go into a certain catagory.
    [ApiController]
    [Route("API/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class ApiController : ControllerBase
    {

         IUserAccess user;
         IAccountAccess acc;
         ICompanyAccess com;

         ITicketAccess tkt;

         IAccountAuthentication authen;
       
        public ApiController(IUserAccess  _user,IAccountAccess _acc, 
        ICompanyAccess _com,IAccountAuthentication _authen,
        ITicketAccess _tkt)
        {
            com = _com;
            user = _user;
            acc = _acc;
            authen = _authen;
            tkt = _tkt;
        }

        //This will get Ticket,Projects Data
        [HttpGet("Dashboard")]
         public async Task<IActionResult> GetDashboardData([FromHeader]string Authorization)
         {  
            string[] validRoles = {"Admin","Associate"};
            var identity = authen.ReadTokens(HttpContext.User.Identity as ClaimsIdentity);
            var isValid  = authen.IsUserRoleValid(identity,validRoles);

            //Checks if user has a valid role to be using this api.
            if(isValid == false) return new UnauthorizedObjectResult("Role is not valid");
            
            //var ticketData = tkt.GetAllTicketsByCompanyId(user);

            return new OkObjectResult("");
         }
    }   
}
