using System;

namespace Models.Converters.Todo
{
    using Client = global::Client.Models.Todo;
    using Model = global::Models.Todo;

    /// <summary>
    /// Предоставляет методы конвертирования задачи между серверной и клиентской моделями
    /// </summary>
    public static class TodoConverter
    {
        /// <summary>
        /// Переводит задачу из серверной модели в клиентскую
        /// </summary>
        /// <param name="modelTodo">Задача в серверной модели</param>
        /// <returns>Задача в клиентской модели</returns>
        public static Client.Todo Convert(Model.Todo modelTodo)
        {
            if (modelTodo == null)
            {
                throw new ArgumentNullException(nameof(modelTodo));
            }

            var clientTodo = new Client.Todo
            {
                Id = modelTodo.Id,
                UserId = modelTodo.UserId,
                CreatedAt = modelTodo.CreatedAt,
                IsCompleted = modelTodo.IsCompleted,
                Title = modelTodo.Title,
                Description = modelTodo.Description,
                Deadline = modelTodo.Deadline
            };

            return clientTodo;
        }
    }
}
