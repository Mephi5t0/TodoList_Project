using System.Runtime.Serialization;

namespace API.Client.Models.Todo
{
    /// <summary>
    /// задача
    /// </summary>
    public class Todo : TodoInfo
    {
        /// <summary>
        /// Описание задачи
        /// </summary>
        [DataMember(Name="Description")]
        public string Description { get; set; }

        public override string ToString()
        {
            return "Description: " + Description;
        }
    }
}
