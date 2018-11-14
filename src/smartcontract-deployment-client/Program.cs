using Microsoft.Extensions.Configuration;
using Nethereum.Hex.HexTypes;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace smartcontract_deployment_client
{
    public class Program
    {        
        public static async Task Main(string[] args)
        {
            PrintAscii();
            var contractName = "MaintenanceLog";
            Console.WriteLine($"Deploying contract {contractName}");

            var configuration = GetConfiguration();

            var privateKey = new EthECKey(configuration["PrivateKey"]);
            var account = new Account(privateKey);

            var (abi, bytecode) = await new ContractReader().GetContract(contractName);

            var web3 = new Web3(account, configuration["GatewayUrl"]);

            var gas = new HexBigInteger(1000000);
            var transaction = await web3.Eth.DeployContract.
                            SendRequestAndWaitForReceiptAsync(abi, bytecode, account.Address, gas);

            ShowMessage($"Transaction Hash: {transaction.TransactionHash}", ConsoleColor.Yellow);
            ShowMessage($"Smart Contract deployed at: {transaction.ContractAddress}", ConsoleColor.Green);

            ShowMessage("");

            ShowMessage($"Etherscan transaction url: https://rinkeby.etherscan.io/tx/{transaction.TransactionHash}", ConsoleColor.Magenta);
            ShowMessage($"Etherscan smart contract url: https://rinkeby.etherscan.io/address/{transaction.ContractAddress}", ConsoleColor.Magenta);

            Console.ReadLine();
        }
        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json")
                   .Build();
        }

        private static void PrintAscii()
        {
            var logo = @"    .___      __                 __                 .__                        
  __| _/_____/  |_  ____   _____/  |_  _____ _____  |  | _____     _________   
 / __ |/  _ \   __\/    \_/ __ \   __\/     \\__  \ |  | \__  \   / ___\__  \  
/ /_/ (  <_> )  | |   |  \  ___/|  | |  Y Y  \/ __ \|  |__/ __ \_/ /_/  > __ \_
\____ |\____/|__| |___|  /\___  >__| |__|_|  (____  /____(____  /\___  (____  /
     \/                \/     \/           \/     \/          \//_____/     \/ ";

            ShowMessage(logo, ConsoleColor.Cyan);
            Console.Write("");
        }

        private static void ShowMessage(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
