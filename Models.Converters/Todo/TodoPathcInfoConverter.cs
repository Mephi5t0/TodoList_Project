using System;

namespace Models.Converters.Todo
{
    using Client = global::Client.Models.Todo;
    using Model = global::Models.Todo;

    /// <summary>
    /// Предоставляет методы конвертирования запроса на изменение задачи между клиентской и серверной моделями
    /// </summary>
    public static class TodoPathcInfoConverter
    {
        /// <summary>
        /// Переводит запрос на изменение задачи из клиентсокой модели в серверную
        /// </summary>
        /// <param name="todoId">Идентификатор задачи</param>
        /// <param name="clientPatchInfo">Запрос на изменение задачи в клиентской модели</param>
        /// <returns>Запрос на изменение задачи в серверной модели</returns>
        public static Model.TodoPatchInfo Convert(string todoId, Client.TodoPatchInfo clientPatchInfo)
        {
            if (clientPatchInfo == null)
            {
                throw new ArgumentNullException(nameof(clientPatchInfo));
            }

            var modelPatchInfo = new Model.TodoPatchInfo(todoId)
            {
                IsCompleted = clientPatchInfo.IsCompleted,                
                Description = clientPatchInfo.Description,
                Title = clientPatchInfo.Title,
                Deadline = clientPatchInfo.Deadline// == default(DateTime) ? (DateTime?) null : clientPatchInfo.Deadline
            };
            
            return modelPatchInfo;
        }
    }
}
