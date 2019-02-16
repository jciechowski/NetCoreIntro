using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TGNet
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

            if (_allowedClients.Any(allowedId => allowedId == clientId))
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