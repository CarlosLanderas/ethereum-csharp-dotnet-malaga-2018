using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ethereum_csharp_dotnet_malaga_2018.Model
{
    [FunctionOutput]
    public class Job
    {
        [Parameter("uint", 1)]
        public int Id { get; set; }
        [Parameter("string", 2)]
        public string Name { get; set; }
        [Parameter("string", 3)]
        public string Description { get; set; }
        [Parameter("uint", 4)]
        public int Status { get; set; }
        [Parameter("address", 5)]
        public string Creator { get; set; }

    }

    public enum Status
    {
        Done = 0,
        InProgress = 1,
        Cancelled = 2
    }
}
