using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.Todo
{
    /// <summary>
    /// Информация о задаче
    /// </summary>
    [BsonKnownTypes(typeof(Todo))]
    public class TodoInfo
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        /// [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит задача
        /// </summary>
        [BsonElement("UserId")]
        public string UserId { get; set; }

        /// <summary>
        /// Дата создания задачи
        /// </summary>
        [BsonElement("CreatedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дедлайн задачи
        /// </summary>
        [BsonElement("Deadline")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Флаг, указывающий, выполнена ли задача
        /// </summary>
        [BsonElement("IsCompleted")]
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Название задачи
        /// </summary>
        [BsonElement("Title")]
        public string Title { get; set; }
    }
}
