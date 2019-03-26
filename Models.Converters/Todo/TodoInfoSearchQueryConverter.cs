using System;

namespace Models.Converters.Todo
{
    using Client = global::Client.Models.Todo;
    using Model = global::Models.Todo;

    /// <summary>
    /// Предоставляет методы конвертирования запроса задач между клиентской и серверной моделями
    /// </summary>
    public static class TodoInfoSearchQueryConverter
    {
        /// <summary>
        /// Переводит запрос задач из клиентсокой модели в серверную
        /// </summary>
        /// <param name="clientQuery">Запрос задач в клиентской модели</param>
        /// <returns>Запрос задач в серверной модели</returns>
        public static Model.TodoInfoSearchQuery Convert(Client.TodoInfoSearchQuery clientQuery)
        {
            if (clientQuery == null)
            {
                throw new ArgumentNullException(nameof(clientQuery));
            }

            var modelSort = clientQuery.Sort.HasValue ?
                SortTypeConverter.Convert(clientQuery.Sort.Value) :
                (SortType?)null;

            var modelSortBy = clientQuery.SortBy.HasValue ?
                TodoSortByConverter.Convert(clientQuery.SortBy.Value) :
                (Model.TodoSortBy?)null;

            var modelQuery = new Model.TodoInfoSearchQuery
            {
                CreatedFrom = clientQuery.CreatedFrom,
                CreatedTo = clientQuery.CreatedTo,
                DeadLineFrom = clientQuery.DeadLineFrom,
                DeadLineTo = clientQuery.DeadLineTo,
                UserId = clientQuery.UserId,
                IsCompleted = clientQuery.IsCompleted,
                Limit = clientQuery.Limit,
                Offset = clientQuery.Offset,
                Sort = modelSort,
                SortBy = modelSortBy
            };

            return modelQuery;
        }
    }
}
