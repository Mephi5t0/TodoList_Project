using MongoDB.Bson.Serialization.Attributes;

namespace Models.Todo
{
    /// <summary>
    /// Задача
    /// </summary>
    public class Todo : TodoInfo
    {
        /// <summary>
        /// Описание задачи
        /// </summary>
        [BsonElement("Description")]
        public string Description { get; set; }
    }
}
