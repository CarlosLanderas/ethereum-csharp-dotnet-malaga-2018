using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ethereum_csharp_dotnet_malaga_2018.Abstractions;
using ethereum_csharp_dotnet_malaga_2018.Model;
using ethereum_csharp_dotnet_malaga_2018.Services;
using Microsoft.AspNetCore.Mvc;

namespace ethereum_csharp_dotnet_malaga_2018.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;

        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }
        [HttpGet]
        [Route("jobs")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            var jobs =  await _maintenanceService.GetJobs();
            return Ok(jobs);
        }

        [HttpPost]
        [Route("job")]
        public async Task<ActionResult<string>> SendJob(Job job)
        {
            var transactionHash = await _maintenanceService.SendJob(job);
            return Ok(new { tx = transactionHash });
        }

        [HttpGet]
        [Route("contract-address")]
        public ActionResult<string> GetContractAddress()
        {
            return Ok(new { address = _maintenanceService.GetContractAddress() });
        }
    }
}
