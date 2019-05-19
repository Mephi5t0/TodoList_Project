﻿using System;

namespace Models.Users
{
    /// <summary>
    /// Информация для создания пользователя
    /// </summary>
    public class UserCreationInfo
    {
        /// <summary>
        /// Инициализирует новый экземпляр описания для создания пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="passwordHash">Хэш пароля</param>
        public UserCreationInfo(string login, string passwordHash)
        {
            this.Login = login ?? throw new ArgumentNullException(nameof(login));
            this.PasswodHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        }
        
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        public string PasswodHash { get; }
    }
}
