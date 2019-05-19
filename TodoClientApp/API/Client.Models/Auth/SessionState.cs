using System.Runtime.Serialization;

namespace API.Client.Models.Auth
{
    public class SessionState
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [DataMember(Name="Login")]
        public string UserId { get; set; }

        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        [DataMember(Name="Password")]
        public string SessionId { get; set; }
    }
}