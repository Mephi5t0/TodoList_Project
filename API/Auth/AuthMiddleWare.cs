using System.Threading;
using System.Threading.Tasks;
using API.Errors;
using Client.Models.Errors;
using Microsoft.AspNetCore.Http;

namespace API.Auth
{
    public class AuthMiddleWare
    {
        private static readonly string LOGIN_REQUEST = "/api/users/login";
        private static readonly string REGISTRATION_REQUEST = "/api/users/register";
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
                    context.Items["SessionId"] = session.SessionId;
                    session.UpdateExpireTime();
                }
                catch
                {
                    await ResponseUnauthenticated(context, cancellationToken);

                    return;
                }
            }
            else
            {
                var path = context.Request.Path;

                if (path != LOGIN_REQUEST && path != REGISTRATION_REQUEST)
                {
                    await ResponseUnauthenticated(context, cancellationToken);

                    return;
                }
            }

            await this.next.Invoke(context);
        }

        private static async Task ResponseUnauthenticated(HttpContext context, CancellationToken cancellationToken)
        {
            var err = ServiceErrorResponses.Unauthenticated();
            var errMessage = err.Error.Message;

            context.Response.StatusCode = (int) err.StatusCode;
            await context.Response.WriteAsync(errMessage, cancellationToken);
        }
    }
}