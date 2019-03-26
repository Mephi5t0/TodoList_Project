using System;

namespace API.Auth
{
    public class SessionState
    {
        public SessionState(string sessionId, string userId)
        {
            if (sessionId == null)
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            this.SessionId = sessionId;
            this.UserId = userId;
        }

        public string SessionId { get; }

        public string UserId { get; }
    }
}
