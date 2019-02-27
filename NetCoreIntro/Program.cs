using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace NetCoreIntro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.SetMinimumLevel(hostingContext.HostingEnvironment.IsProduction()
                        ? LogLevel.Warning
                        : LogLevel.Debug);
                    logging.AddConsole();
                })
                .UseStartup<Startup>();
    }
}
