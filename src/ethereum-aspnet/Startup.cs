using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ethereum_csharp_dotnet_malaga_2018.Abstractions;
using ethereum_csharp_dotnet_malaga_2018.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace ethereum_csharp_dotnet_malaga_2018
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMaintenanceService>(sp =>
            {
                var key = Configuration["PrivateKey"];
                var privateKey = new EthECKey(key);
                var address = privateKey.GetPublicAddress();
                var account = new Account(privateKey);
                var web3 = new Web3(account, Configuration["GatewayUrl"]);
                
                return new EthereumMaintenanceService(web3,
                    privateKey,
                    Configuration["SmartContractAddress"],
                    Configuration["Abi"]);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {            
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
