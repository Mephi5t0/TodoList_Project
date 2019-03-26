using System;

namespace Models.Converters.Todo
{
    using Client = global::Client.Models.Todo;
    using Model = global::Models.Todo;

    /// <summary>
    /// Предоставляет методы конвертирования аспекта сортировки задач между клиентской и серверной моделями
    /// </summary>
    public static class TodoSortByConverter
    {
        /// <summary>
        /// Переводит аспект сортировки задач из клиентской модели в серверную
        /// </summary>
        /// <param name="clientSortBy">Аспект сортировки задач в клиентской модели</param>
        /// <returns>Аспект сортировки задач в серверной модели</returns>
        public static Model.TodoSortBy Convert(Client.TodoSortBy clientSortBy)
        {
            switch (clientSortBy)
            {
                case Client.TodoSortBy.Creation:
                    return Model.TodoSortBy.Creation;

                case Client.TodoSortBy.Deadline:
                    return Model.TodoSortBy.Deadline;

                default:
                    throw new ArgumentException($"Unknown sort by value \"{clientSortBy}\".", nameof(clientSortBy));
            }
        }
    }
}
