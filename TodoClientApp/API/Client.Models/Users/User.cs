using System;
using System.Runtime.Serialization;

namespace API.Client.Models.Users
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [DataMember(Name="Id")]
        public string Id { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [DataMember(Name="Login")]
        public string Login { get; set; }
        
        /// <summary>
        /// Дата регистрации пользователя
        /// </summary>
        [DataMember(Name="RegisteredAt")]
        public DateTime RegisteredAt { get; set; }
    }
}
