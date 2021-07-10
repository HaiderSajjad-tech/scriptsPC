using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DemoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(
			webBuilder =>
			{
				webBuilder.UseKestrel(options => {options.Listen(IPAddress.Any, port 80);});
				webBuilder.UseStartup<Startup>();
			});
                
			
			
		
    }
}
