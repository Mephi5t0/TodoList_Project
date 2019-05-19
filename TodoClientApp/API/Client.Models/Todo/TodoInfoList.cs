using System.Collections.Generic;

namespace API.Client.Models.Todo
{
    /// <summary>
    ///  Список c описанием задач
    /// </summary>
    public class TodoInfoList 
    {
        /// <summary>
        /// Краткая информация о задачах
        /// </summary>
        public IReadOnlyCollection<TodoInfo> TodoItems { get; set; }
    }
}