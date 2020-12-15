using CoreRCON;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static IPAddress ipaddress = IPAddress.Parse("127.0.0.1");
        static ushort port = 25575;
        static string password = "minecraft";
        static RCON rcon = new RCON(ipaddress, port, password);

        static private string command = $"/data get block 339 66 671 Items";

        static async Task Main(string[] args)
        {
            await Run();
        }

        static async Task Run()
        {
            ChestItemsData chestItemsData = new ChestItemsData();
            await rcon.ConnectAsync();
            var result = await rcon.SendCommandAsync(command);

            Console.WriteLine(result);

            chestItemsData.Extraction(result);
        }
    }
}
