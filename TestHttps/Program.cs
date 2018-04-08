using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Net;
namespace TestHttps
{
    public class Program
    {
        /// <summary>
        /// 服务器端保存字符串的列表
        /// </summary>
        public static List<string> Values = new List<string>() { "value0", "value1" };
        public static  IPAddress LocalIP;
        public static void Main(string[] args)
        {
            string name = Dns.GetHostName();
            LocalIP = Dns.GetHostAddresses(name)[0];
            BuildWebHost(args).Run();

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()

                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any , 5001, listenOptions =>
                    {
                        listenOptions.UseHttps("d:\\iis.pfx", "yyz");
                    });
                    options.Listen(IPAddress.Any, 5002);
                })

                .Build();
    }
}
