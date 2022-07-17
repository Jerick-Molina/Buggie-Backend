using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buggie.DataProperties;
using Buggie.Interface;
using Buggie.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Buggie.Controllers
{

    [ApiController]
    [Route("API/[controller]")]
    public class UserController : ControllerBase{

        public IMySqlDataAccess db;

        public IUserAccess user;
        
        public UserController (IMySqlDataAccess _db,IUserAccess _user){
            db = _db;
            user = _user;
        }

        [HttpGet]
        public  async Task<IActionResult> GetUsers(){


            var r = await user.GetUsers();

            return new OkObjectResult(r.ToList());
        }
    }
}