using System;
using Mongo.Migration.Documents;
using Mongo.Migration.Documents.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.Users
{
    /// <summary>
    /// Пользователь
    /// </summary>
    [CurrentVersion("0.1.1")]
    public class User : IDocument
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

        public DocumentVersion Version { get; set; }
    }
}
