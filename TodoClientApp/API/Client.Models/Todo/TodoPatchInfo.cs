using System;
using System.Runtime.Serialization;

namespace API.Client.Models.Todo
{
    /// <summary>
    /// Информация для изменения задачи
    /// </summary>
    public class TodoPatchInfo
    {
        /// <summary>
        /// Флаг, указывающий, что задача выполнена
        /// </summary>
        [DataMember(Name="IsCompleted")]
        public bool? IsCompleted { get; set; }

        /// <summary>
        /// Новый заголовок задачи
        /// </summary>
        [DataMember(Name="Title")]
        public string Title { get; set; }

        /// <summary>
        /// Новое описание задачи
        /// </summary>
        [DataMember(Name="Description")]
        public string Description { get; set; }
        
        /// <summary>
        /// Новый дедлайн задачи
        /// </summary>
        [DataMember(Name="Deadline")]
        public DateTime? Deadline { get; set; }
    }
}
