using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Models.Users;
using Models.Users.Services;

namespace API.Auth
{
    public class Authenticator : IAuthenticator
    {
        private readonly UserService userService;
        private readonly ConcurrentDictionary<string, SessionState> sessions;

        public Authenticator(UserService userService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException(nameof(userService));
            }

            this.userService = userService;
            this.sessions = new ConcurrentDictionary<string, SessionState>();
        }

        public async Task<SessionState> AuthenticateAsync(string login, string password, CancellationToken cancellationToken)
        {
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            cancellationToken.ThrowIfCancellationRequested();

            User user = null;

            try
            {
                user = await this.userService.GetAsync(login).ConfigureAwait(false);
            }
            catch (UserNotFoundException)
            {
                throw new AuthenticationException();
            }

            var currentHash = this.HashPassword(password);

            if (!user.PasswordHash.Equals(currentHash))
            {
                throw new AuthenticationException();
            }

            var sessionId = Guid.NewGuid().ToString();
            var sessionState = new SessionState(sessionId, user.Id);

            while (!this.sessions.TryAdd(sessionId, sessionState))
            {
            }

            return sessionState;
        }

        public Task<SessionState> GetSessionAsync(string sessionId, CancellationToken cancellationToken)
        {
            if (sessionId == null)
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            if (!this.sessions.TryGetValue(sessionId, out var sessionState))
            {
                throw new AuthenticationException();
            }

            return Task.FromResult(sessionState);
        }

        public Task<SessionState> DeleteSessionAsync(string sessionId)
        {
            if (sessionId == null)
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            SessionState session;
            while (!this.sessions.TryRemove(sessionId, out session))
            {
            }
            
            return Task.FromResult(session);
        }

        public Task<bool> IsAccessAllowedAsync(string userId, string sessionId)
        {
            if (sessionId == null)
            {
                throw new ArgumentNullException(nameof(sessionId));
            }
            
            var result = sessions[sessionId].UserId == userId;
            
            return Task.FromResult(result);
        }

        public Task UpdateSession(string sessionId)
        {
            if (sessionId == null)
            {
                throw new ArgumentNullException(nameof(sessionId));
            }
            
            sessions[sessionId].UpdateExpireTime();
            
            return Task.CompletedTask;
        }

        public void DeleteExpiredSessions()
        {
            foreach (var session in sessions)
            {
                if (sessions[session.Key].IsExpired())
                {
                    while (!this.sessions.TryRemove(session.Key, out var deleted))
                    {
                    }
                }
            }
        }
        
        private string HashPassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = md5.ComputeHash(passwordBytes);
                var hash = BitConverter.ToString(hashBytes);
                return hash;
            }
        }
    }
}
