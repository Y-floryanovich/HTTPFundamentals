using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            await Client.StartAsync(); 
        }
    }
}
