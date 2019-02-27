using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreIntro
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            const string connectionString = "CoffeeInMemoryDB";
            services.AddDbContext<CoffeeDBContext>(context => context.UseInMemoryDatabase(connectionString));

            #region Custom health check

            services.AddSingleton(s => new DbHealthCheck(connectionString));
            services.AddHealthChecks().AddCheck<DbHealthCheck>(connectionString);

            #endregion
            #region Out of the box DB health check
//            services
//                .AddHealthChecks()
//                .AddDbContextCheck<CoffeeDBContext>();

            #endregion

            services.AddSingleton<ClientIdVerifier>();
            services.AddSingleton<LocalStore>();
            services.AddHostedService<CustomHostedService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHealthChecks("/health");

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
    }
}