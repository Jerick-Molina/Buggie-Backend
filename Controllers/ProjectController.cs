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
    [Route("API/[controller]")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private IUserAccess user;
        private IAccountAccess acc;
        private ICompanyAccess com;

        private IAccountAuthentication authen;
       
        public ProjectController(IUserAccess  _user,IAccountAccess _acc, 
        ICompanyAccess _com,IAccountAuthentication _authen)
        {
            com = _com;
            user = _user;
            acc = _acc;
            authen = _authen;
        }
    }
}