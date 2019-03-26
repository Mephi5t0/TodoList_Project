using System;

namespace Models.Converters.Todo
{
    using Client = global::Client.Models.Todo;
    using Model = global::Models.Todo;

    /// <summary>
    /// Предоставляет методы конвертирования запроса на создание задачи между клиентской и серверной моделями
    /// </summary>
    public static class TodoBuildInfoConverter
    {
        /// <summary>
        /// Переводит запрос на создание задачи из клиентсокой модели в серверную
        /// </summary>
        /// <param name="clientUserId">Идентификатор пользователя в клиентской модели</param>
        /// <param name="clientBuildInfo">Запрос на создание задачи в клиентской модели</param>
        /// <returns>Запрос на создание задачи в серверной модели</returns>
        public static Model.TodoCreationInfo Convert(string clientUserId, Client.TodoBuildInfo clientBuildInfo)
        {
            if (clientUserId == null)
            {
                throw new ArgumentNullException(nameof(clientUserId));
            }

            if (clientBuildInfo == null)
            {
                throw new ArgumentNullException(nameof(clientBuildInfo));
            }

            var modelCreationInfo = new Model.TodoCreationInfo(
                clientUserId,
                clientBuildInfo.Title,
                clientBuildInfo.Description,
                clientBuildInfo.Deadline);

            return modelCreationInfo;
        }
    }
}
