using ethereum_csharp_dotnet_malaga_2018.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ethereum_csharp_dotnet_malaga_2018.Abstractions
{
    public interface IMaintenanceService
    {
        Task<string> SendJob(Job job);
        Task<IEnumerable<Job>> GetJobs();        
        Task<Job> GetJobByIndex(int index);
        string GetContractAddress();
    }
}
