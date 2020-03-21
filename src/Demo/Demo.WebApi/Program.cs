using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfiguration configuration = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                              .AddJsonFile(path: $"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                              .Build();

            return WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton(configuration);
                    //services.AddSingleton<IHostedService, MassTransitHostedService>();
                })
            .UseIISIntegration()
                    .UseKestrel()
                    .UseUrls("http://*:53708")
                .UseStartup<Startup>();
        }

    }
}
