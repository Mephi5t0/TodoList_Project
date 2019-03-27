using System;
using Newtonsoft.Json;

namespace API.Auth
{
    public class SessionState
    {
        private const long Minute = 60;
        private const long TimeExpire = 30 * Minute;
        public SessionState(string sessionId, string userId)
        {
            if (sessionId == null)
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            this.SessionId = sessionId;
            this.UserId = userId;
            this.Expire = DateTimeOffset.Now.ToUnixTimeSeconds() + TimeExpire;
        }

        public string SessionId { get; }

        public string UserId { get; }
        
        [JsonIgnore] 
        public long Expire { get; private set; }

        public void UpdateExpireTime()
        {
            this.Expire += TimeExpire;
        }

        public bool IsExpired()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds() - Expire >= TimeExpire;
        }
    }
}
