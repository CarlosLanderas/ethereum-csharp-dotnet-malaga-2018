
using System.IO;
using System.Threading.Tasks;

namespace smartcontract_deployment_client
{
    public class ContractReader
    {
        public async Task<(string abi, string bytecode)> GetContract(string contractName)
        {  
            var contractPath = @"..\..\"; 
            var abi = await File.ReadAllTextAsync($"{contractPath}{contractName}.abi");
            var bytecode = $"0x{(await File.ReadAllTextAsync($"{contractPath}{contractName}.bin"))}";
            return (abi, bytecode);
        }
    }
}
