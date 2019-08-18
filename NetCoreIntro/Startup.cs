using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetCoreIntro
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            const string connectionString = "CoffeeInMemoryDB";
            services.AddDbContext<CoffeeDBContext>(context => context.UseInMemoryDatabase(connectionString));

            #region Custom health check

//            services.AddSingleton(s => new DbHealthCheck(connectionString));
//            services.AddHealthChecks().AddCheck<DbHealthCheck>(connectionString);

            #endregion

            #region Out of the box DB health check

            services
                .AddHealthChecks()
                .AddDbContextCheck<CoffeeDBContext>();

            #endregion

            services.AddSingleton<ClientIdVerifier>();
            services.AddSingleton<LocalStore>();

            services.AddHostedService<ImportantWorkerHostedService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = WriteResponse
            });

            #region Middleware

            app.Map("/ping",
                middleware => middleware.Run(async context =>
                {
                    await context.Response.WriteAsync("Service is working.");
                }));

            app.UseMiddleware<ClientIdVerifier>();

            app.UseMvc();

            #endregion
        }

        private static Task WriteResponse(HttpContext httpContext,
            HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("status", result.Status.ToString()),
                new JProperty("results", new JObject(result.Entries.Select(pair =>
                    new JProperty(pair.Key, new JObject(
                        new JProperty("status", pair.Value.Status.ToString()),
                        new JProperty("description", pair.Value.Description),
                        new JProperty("data", new JObject(pair.Value.Data.Select(
                            p => new JProperty(p.Key, p.Value))))))))));

            return httpContext.Response.WriteAsync(
                json.ToString(Formatting.Indented));
        }
    }
}