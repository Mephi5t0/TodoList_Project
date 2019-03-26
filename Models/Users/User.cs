using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.Users
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [BsonElement("Login")]
        public string Login { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        [BsonElement("PasswordHash")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Дата регистрации пользователя
        /// </summary>
        [BsonElement("RegisteredAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime RegisteredAt { get; set; }
    }
}
