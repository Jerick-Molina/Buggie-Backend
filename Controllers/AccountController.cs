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

namespace Buggie.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class AccountController : ControllerBase{

        private IUserAccess userDb;
        private IAccountAccess accDb;
        private ICompanyAccess comDb;

        private IAccountAuthentication authen;
        private string rMessage = "";
        public AccountController(IUserAccess  _userDb,IAccountAccess _accDb, 
        ICompanyAccess _comDb,IAccountAuthentication _authen)
        {
            comDb = _comDb;
            userDb = _userDb;
            accDb = _accDb;
            authen = _authen;
        }
    
        [HttpGet("Create/User")] 
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount([FromHeader]string companyCode,User user){
            
            var companyExist = await comDb.FindCompany(companyCode);
             if(companyExist.CompanyCode == string.Empty)
             {
                return new BadRequestObjectResult("Invalid Code");
             }
            var userExist = await userDb.FindUser(user);
            if(userExist.Email == string.Empty)
            {   
                user.Role = "Developer";
                user.CompanyId = companyExist.CompanyId;
                  rMessage = await accDb.AccountCreate(user);
                 
            }
            var r = returnReview(rMessage);
            if(r != "")
            {
                return new BadRequestObjectResult(r);
            }
            return new OkObjectResult(rMessage);
        }    
        

        //Get the company name along with creating the user account
        [HttpGet("Create/Company")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCompany([FromHeader]string Name,[FromBody]User user)
        {
            Console.WriteLine("hi");
            var userExist = await userDb.FindUser(user);
            if(userExist.Email != string.Empty)
            {
             return new BadRequestObjectResult("User already exist");
            }
            if(Name != ""){
             var code = await comDb.CreateCompany(Name,user);
            if(code != "")
            {
             var comp = await comDb.FindCompany(code);

            user.CompanyId = comp.CompanyId;
            user.Role = "Admin";
            rMessage = await accDb.AccountCreate(user);

            }
            }else
            {
                return new BadRequestObjectResult("Company name empty");
            }
            var r = returnReview(rMessage);
            if(r != "")
            {
                return new BadRequestObjectResult(r);
            }
            return new OkObjectResult(rMessage);
        }

        [HttpGet("SignIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(User user)
        {
            
            var result = await accDb.AccountSignIn(user);
            
            var rMessage = returnReview(result);
            if(rMessage != "")
            {
               return new BadRequestObjectResult(rMessage);
            }
            return new OkObjectResult(result);
        }


     
        

        
        // [HttpGet("Info")]
        // [Authorize]
        // public async Task<IActionResult> Test([FromHeader]string Authorization)
        // {   
        //     string[] split = Authorization.Split(' ');

        //     var user =   authen.ReadJwtAccessToken(split[1].ToString());

        //     if(user.Role != "Poop")
        //     {
        //         return new UnauthorizedObjectResult(returnReview("Role"));
        //     }
        //     return new OkObjectResult("Authorized");
        // }

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
                case "Role":
                    return "Low authentication role";
            }


            return "";
        }
    }
}