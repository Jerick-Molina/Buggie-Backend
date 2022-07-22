using System.Collections.Generic;
using System.Threading.Tasks;
using Buggie.DataProperties;

namespace Buggie.Interface{
    public interface IProjectAccess
    {   
        Task<bool> CreateProject(Projects project);
       Task<List<Projects>> FindProjectsByCompany(int companyId);
    }
}