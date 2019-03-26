using System;

namespace Models.Todo
{
    /// <summary>
    /// Исключение, которое возникает при попытке получить несуществующую задачу
    /// </summary>
    public class TodoNotFoundExcepction : Exception
    {
        /// <summary>
        /// Создает новый экземпляр исключения о том, что задача не найдена
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public TodoNotFoundExcepction(string message)
            : base(message)
        {
        }
    }
}
