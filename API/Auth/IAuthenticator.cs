using System.Threading;
using System.Threading.Tasks;

namespace API.Auth
{
    public interface IAuthenticator
    {
        Task<SessionState> AuthenticateAsync(string login, string password, CancellationToken cancellationToken);

        Task<SessionState> GetSessionAsync(string sessionId, CancellationToken cancellationToken);

        Task<SessionState> DeleteSessionAsync(string sessionId);

        Task<bool> isAccessAllowedAsync(string userId, string sessionId);
    }
}