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
    public class ProjectController : ControllerBase
    {
         IUserAccess user;
         IAccountAccess acc;
         ICompanyAccess com;

         IAccountAuthentication authen;
         IProjectAccess proj;
       
        public ProjectController(IUserAccess  _user,IAccountAccess _acc, 
        ICompanyAccess _com,IAccountAuthentication _authen,
        IProjectAccess _proj)
        {
            com = _com;
            user = _user;
            acc = _acc;
            authen = _authen;
            proj = _proj;
        }


        [HttpPost("Create")]
        public async Task<IActionResult>  CreateProject([FromBody] Projects project)
        {

            string[] validRoles = {"Admin","Manager"};
            var identity = authen.ReadTokens(HttpContext.User.Identity as ClaimsIdentity);
           
            var isValid  = authen.IsUserRoleValid(identity,validRoles);

            //Checks if user has a valid role to be using this api.
            if(isValid == false) return new UnauthorizedObjectResult("Role is not valid");
            project.CompanyId = identity.CompanyId;
            var didSucceed = await proj.CreateProject(project);
            if(didSucceed == false)  
            {
                return new ConflictObjectResult("Could not insert project");
            }

            return new OkObjectResult("Success");
        }
         [HttpPost("Find")]
        public async Task<IActionResult>  FindProject([FromBody] Projects project)
        {

            string[] validRoles = {"Admin","Manager"};
            var identity = authen.ReadTokens(HttpContext.User.Identity as ClaimsIdentity);
           
            var isValid  = authen.IsUserRoleValid(identity,validRoles);

            //Checks if user has a valid role to be using this api.
            if(isValid == false) return new UnauthorizedObjectResult("Role is not valid");
            project.CompanyId = identity.CompanyId;
            var didSucceed = await proj.CreateProject(project);
            if(didSucceed == false)   return new ConflictObjectResult("Could not insert project");
            

            return new OkObjectResult("Success");
        }
    }
}