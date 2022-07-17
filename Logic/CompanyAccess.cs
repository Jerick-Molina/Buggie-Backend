using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buggie.DataProperties;
using Buggie.Interface;

namespace  Buggie.Logic{

    public class CompanyAccess : ICompanyAccess
    {

        private IMySqlDataAccess db;


        public CompanyAccess(IMySqlDataAccess _db, IUserAccess _accDb)
        {
            db = _db;
        }
        // Account must be created before the company.
        public async Task<string> CreateCompany(string companyName,User user)
        {
            string sql = "insert into Company(Name,CompanyCode) values (@Name, @CompanyCode)";
           
            var code = GenerateCompanyCode();
            var codeExist = await FindCompany(code.Result);
             dynamic parameters = new {companyName,code.Result};
            if(codeExist.CompanyCode == string.Empty)
            {
                 db.SaveData<Company>(sql,new Company{Name = companyName,CompanyCode =code.Result});
                return  code.Result;
            }

            return string.Empty;
        }
        
        public async Task<string> GenerateCompanyCode()
        {
            int codeLength = 8;
            
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string code = "";
            var rng  = new Random();
            for(var i =0; i < codeLength; i++)
            {
               code = code + alphabet[rng.Next(alphabet.Length)];
            }

            return code;
        }

        public async Task<Company> FindCompany(string code)
        {
            string sql = $"select * from Company where CompanyCode = '{code}'";
            
            var results = await db.LoadData<Company,dynamic>(sql,"");

            if(results.Count <= 1 && results.Count != 0)
            {
                return results[0];
            }
            return new Company();
        }
        
    }
}