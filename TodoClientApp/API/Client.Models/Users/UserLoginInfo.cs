using System.Runtime.Serialization;

namespace API.Client.Models.Users
{
    
    /// <summary>
    /// Информация для входа пользователя
    /// </summary>
    public class UserLoginInfo
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