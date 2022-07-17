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
    [Route("API/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class AccountController : ControllerBase{

        private IUserAccess user;
        private IAccountAccess acc;
        private ICompanyAccess com;

        private IAccountAuthentication authen;
       
        public AccountController(IUserAccess  _user,IAccountAccess _acc, 
        ICompanyAccess _com,IAccountAuthentication _authen)
        {
            com = _com;
            user = _user;
            acc = _acc;
            authen = _authen;
        }
    
        [HttpPost("Create/User")] 
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount([FromHeader]string companyCode,User userData){
            
            var companyExist = await com.FindCompany(companyCode);
            if(companyExist.CompanyCode == string.Empty) return new BadRequestObjectResult("Invalid Code");
             
            var userExist = await user.FindUser(userData);
            if(userExist.Email == string.Empty)
            {   
                userData.CompanyId = companyExist.CompanyId;
                var tokens = await acc.AccountCreate(userData);

                if(tokens != null) return new OkObjectResult(tokens);
                
            }  
            return new BadRequestObjectResult("Invalid Credidentials");
        }    
        
        //Get the company name along with creating the user account
        [HttpPost("Create/Company")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCompany([FromHeader]string Name,[FromBody]User user)
        {
          
            var userExist = await acc.FindAccount(user);
            if(userExist.Email != string.Empty)  return new BadRequestObjectResult("User already exist");
            
            if(Name != "")
            {

             var code = await com.CreateCompany(Name,user);
             if(code != "")
             {

              var comp = await com.FindCompany(code);
              user.CompanyId = comp.CompanyId;
              user.Role = "Admin";
              var tokens = await acc.AccountCreate(user);
              if(tokens != null) return new BadRequestObjectResult(tokens);
             }

            }else return new BadRequestObjectResult("Company name empty");
            
            return new OkObjectResult("");
        }

        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(User user)
        {
            
            var tokens = await acc.AccountSignIn(user);
            if(tokens != null) return new BadRequestObjectResult(tokens);

            return new OkObjectResult("Null");
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

       
    }
}