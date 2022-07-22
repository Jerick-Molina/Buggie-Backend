using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buggie.DataProperties;
using Buggie.Interface;

namespace  Buggie.Logic{

    public class ProjectAccess : IProjectAccess
    {

        public readonly IMySqlDataAccess db;

        public ProjectAccess(IMySqlDataAccess _db)
        {
            db = _db;
        }
        public async Task<bool> CreateProject(Projects project)
        {   
            string sql = $"insert into Projects (Name,Description,CompanyId) values (@Name,@Description,@CompanyId)";

            try
            {
                 db.SaveData<Projects>(sql,project);
                 return true;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
                return false;
        }
        public async Task<List<Projects>> FindProjectsByCompany(int companyId) 
        {   
            //MySQL: Find alls the projects with associated CompanyId
            string sql = $"select * from Projects where {companyId}";
            
            try
            {

             var results = await db.LoadData<Projects,dynamic>(sql,"");
             if(results.Count > 0)  return results;
            
            }catch(Exception e)
            {

            }
          
            return null;
        }
    }
}