using System;

namespace Models.Converters.Todo
{
    using Client = global::Client.Models.Todo;
    using Model = global::Models.Todo;

    /// <summary>
    /// Предоставляет методы конвертирования информации о задаче между серверной и клиентской моделями
    /// </summary>
    public static class TodoInfoConverter
    {
        /// <summary>
        /// Переводит информацию о задачу из серверной модели в клиентскую
        /// </summary>
        /// <param name="modelTodoInfo">Информация о задаче в серверной модели</param>
        /// <returns>Информация о задаче в клиентской модели</returns>
        public static Client.TodoInfo Convert(Model.TodoInfo modelTodoInfo)
        {
            if (modelTodoInfo == null)
            {
                throw new ArgumentNullException(nameof(modelTodoInfo));
            }

            var clientTodoInfo = new Client.TodoInfo
            {
                Id = modelTodoInfo.Id,
                UserId = modelTodoInfo.UserId,
                CreatedAt = modelTodoInfo.CreatedAt,
                IsCompleted = modelTodoInfo.IsCompleted,
                Title = modelTodoInfo.Title,
                Deadline = modelTodoInfo.Deadline
            };

            return clientTodoInfo;
        }
    }
}
