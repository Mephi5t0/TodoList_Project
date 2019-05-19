using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace API.Client.Models.Todo
{
    /// <summary>
    /// Информация о задаче
    /// </summary>
    public class TodoInfo
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        [DataMember(Name="Id")]
        public string Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит задача
        /// </summary>
        [DataMember(Name="UserId")]
        public string UserId { get; set; }

        /// <summary>
        /// Дата создания задачи
        /// </summary>
        [DataMember(Name="CreatedAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Крайник срок выполнения задачи
        /// </summary>
        [DataMember(Name="Deadline")]
        public DateTime Deadline { get; set; }
        
        /// <summary>
        /// Флаг, указывающий, выполнена ли задача
        /// </summary>
        [DataMember(Name="IsCompleted")]
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Название задачи
        /// </summary>
        [DataMember(Name="Title")]
        public string Title { get; set; }
    }
}
