using System.Runtime.Serialization;

namespace API.Client.Models.Users
{
    /// <summary>
    /// Информация для регистрации пользователя
    /// </summary>
    public class UserRegistrationInfo
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [DataMember(Name="Login")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [DataMember(Name="Password")]
        public string Password { get; set; }
    }
}
