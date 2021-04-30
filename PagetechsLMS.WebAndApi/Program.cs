using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagetechsLMS.WebAndApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logger =>
                {
                    //logger.AddFilter("System", LogLevel.Warning);
                    //logger.AddFilter("Microsoft", LogLevel.Warning);
                    logger.AddFilter("PagetechsLMS", LogLevel.Trace);
                    logger.AddLog4Net();
                })
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     config.AddJsonFile(
                         "set.config.json", optional: true, reloadOnChange: true);
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
