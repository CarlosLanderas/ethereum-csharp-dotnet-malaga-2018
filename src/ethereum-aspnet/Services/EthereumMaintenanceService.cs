using ethereum_csharp_dotnet_malaga_2018.Abstractions;
using ethereum_csharp_dotnet_malaga_2018.Model;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
using Nethereum.Util;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ethereum_csharp_dotnet_malaga_2018.Services
{
    public class EthereumMaintenanceService : IMaintenanceService
    {
        private readonly Web3 _web3;
        private readonly EthECKey _privateKey;
        private readonly string _fromAddress;
        private readonly string _address;
        private readonly string _abi;

        public EthereumMaintenanceService(Web3 web3, EthECKey privateKey, string address, string abi)
        {
            _web3 = web3;
            _privateKey = privateKey;
            _fromAddress = privateKey.GetPublicAddress();
            _address = address;
            _abi = abi;
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            List<Task<Job>> jobs = new List<Task<Job>>();

            var contract = GetContract();
            var totalJobs = await contract.GetFunction("totalJobs").CallAsync<int>();
            for (var i = 0; i < totalJobs; i++)
            {
                jobs.Add(GetJobByIndex(i));
            }

            var jobsResult = await Task.WhenAll(jobs);

            return jobsResult.OrderByDescending(j => j.Id);
        }

        public async Task<Job> GetJobByIndex(int index)
        {
            var contract = GetContract();
            return await contract.GetFunction("getJob")
                            .CallDeserializingToObjectAsync<Job>(new object[] { index });
        }

        public async Task<string> SendJob(Job job)
        {
            var contract = GetContract();
            var sendJobFunc = contract.GetFunction("storeJob");
            
            var transactionHash = await sendJobFunc.SendTransactionAsync(                
                _fromAddress,
                new HexBigInteger(300000),
                new HexBigInteger(Web3.Convert.ToWei(25, UnitConversion.EthUnit.Gwei)),
                new HexBigInteger(0),
                job.Name, job.Description, job.Status);

            return transactionHash;
        }

        private Contract GetContract()
        {
            return _web3.Eth.GetContract(_abi, _address);
        }

        public string GetContractAddress()
        {
            return _address;
        }
    }
}
