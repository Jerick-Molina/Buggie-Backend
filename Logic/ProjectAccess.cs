using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buggie.DataProperties;
using Buggie.Interface;

namespace  Buggie.Logic{

    public class ProjectAccess 
    {

        public readonly IMySqlDataAccess db;

        public ProjectAccess(IMySqlDataAccess _db)
        {
            db = _db;
        }

        public async Task<List<Company>> FindProjectsByCompId(Company company) 
        {   
            //MySQL: Find alls the projects with associated CompanyId
            string sql = "select CompanyId from Projects";
            
            var results = await db.LoadData<Company,string>(sql,"");
            
            if(results.Count > 0)
            {
                return results;
            }
            return null;
        }
    }
}