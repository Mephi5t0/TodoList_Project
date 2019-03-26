using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Auth
{
    public class AuthMiddleWare
    {
        private IAuthenticator authenticator;
        private RequestDelegate next;

        public AuthMiddleWare(IAuthenticator authenticator, RequestDelegate next)
        {
            this.authenticator = authenticator;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cancellationToken = new CancellationToken();
            
            if (context.Request.Headers.TryGetValue("SessionId", out var sessionId))
            {
                try
                {
                    var session = await this.authenticator.GetSessionAsync(sessionId, cancellationToken);
                    context.Items["UserId"] = session.UserId;
                }
                catch
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Session id is invalid", cancellationToken);
                    
                    return;
                }
            }

            await this.next.Invoke(context);
        }
    }
}
