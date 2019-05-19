using System;
using System.Runtime.Serialization;

namespace API.Client.Models.Todo
{
    /// <summary>
    /// Информация для создания ззадачи
    /// </summary>
    public class TodoBuildInfo
    {
        /// <summary>
        /// Заголовок задачи
        /// </summary>
        [DataMember(Name="Title")]
        public string Title { get; set; }

        /// <summary>
        /// Описание задачи
        /// </summary>
        [DataMember(Name="Description")]
        public string Description { get; set; }
        
        /// <summary>
        /// Дедлайн задачи
        /// </summary>
        [DataMember(Name="Deadline")]
        public DateTime Deadline{ get; set; }

    }
}
