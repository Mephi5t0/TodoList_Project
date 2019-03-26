using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Client.Models.Todo
{
    /// <summary>
    /// Информация для создания ззадачи
    /// </summary>
    [DataContract]
    public class TodoBuildInfo
    {
        /// <summary>
        /// Заголовок задачи
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Title { get; set; }

        /// <summary>
        /// Описание задачи
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Description { get; set; }
        
        /// <summary>
        /// Дедлайн задачи
        /// </summary>
        [DataMember(IsRequired = true)]
        public DateTime Deadline{ get; set; }

    }
}
