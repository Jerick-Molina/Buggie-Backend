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
namespace Buggie.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    //[Authorize]
    public class AccountController : ControllerBase{

        private IUserAccess userDb;
        private IAccountAccess accDb;

        public AccountController(IUserAccess  _userDb,IAccountAccess _accDb)
        {
            userDb = _userDb;
            accDb = _accDb;
        }
    
        [HttpGet("Create")] 
        //[AllowAnonymous]
        public async Task<IActionResult> CreateAccount(){
            User userMock = new User()
            {
                
                FirstName = "Test",
                LastName = "Subject",
                Email = "TheMol90@gmail.com",
                Password = "Test9012!!",
                Role = "Admin"
            };
            var r = await accDb.AccountCreate(userMock);

            var returnMessage = returnReview(r);
            if(returnMessage != "")
            {
                return new BadRequestObjectResult(returnMessage);
            }
            return new OkObjectResult(r);
        }    

        [HttpGet("SignIn")]

        public async Task<IActionResult> SignIn()
        {
            


            return new OkObjectResult("");
        }




        private string returnReview(string result)
        {
              switch(result)
            {
                case "Empty":
                    return "One or more Inputs are empty";
              
                case "Twins":
                    return  "Email already exist";
                case "Error":
                    return "Something happened in our end, please try again later";
                case "Invalid":
                    return "Input is invalid";
            }

            return "";
        }
    }
}