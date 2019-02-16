using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetCoreIntro
{
    public class ClientIdVerifier : IMiddleware
    {
        private readonly List<string> _allowedClients;

        public ClientIdVerifier()
        {
            _allowedClients = new List<string>
            {
                "Microsoft",
                "PGS"
            };
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var clientId = context.Request.Headers["ClientId"];
            var isPingEndpoint = context.Request.Path.Value == "/ping";

            if (_allowedClients.Any(allowedId => allowedId == clientId) || isPingEndpoint)
            {
                await next(context);
            }
            else
            {
                await context.Response.WriteAsync("Client not allowed.");
            }
        }
    }
}