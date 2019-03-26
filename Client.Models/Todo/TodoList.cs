using System.Collections.Generic;

namespace Client.Models.Todo
{
    /// <summary>
    ///  Список c описанием задач
    /// </summary>
    public class TodoList
    {
        /// <summary>
        /// Краткая информация о задачах
        /// </summary>
        public IReadOnlyCollection<TodoInfo> todoItems { get; set; }
    }
}
