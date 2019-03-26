using System;

namespace Models.Converters
{ 
    /// <summary>
    /// Предоставляет методы конвертирования типа сортировки между клиентской и серверной моделями
    /// </summary>
    public static class SortTypeConverter
    {
        /// <summary>
        /// Переводит тип сортировки из серверной модели в клиентскую
        /// </summary>
        /// <param name="sortType">Тип сортировки в сервеной модели</param>
        /// <returns>Тип сортировки в клиентской модели</returns>
        public static Client.Models.SortType Convert(SortType sortType)
        {
            switch (sortType)
            {
                case SortType.Ascending:
                    return Client.Models.SortType.Ascending;

                case SortType.Descending:
                    return Client.Models.SortType.Descending;

                default:
                    throw new ArgumentException($"Unknown sort type \"{sortType}\".", nameof(sortType));
            }
        }

        /// <summary>
        /// Переводит тип сортировки из клиентской модели в серверную
        /// </summary>
        /// <param name="sortType">Тип сортировки в клиентской модели</param>
        /// <returns>Тип сортировки в серверной модели</returns>
        public static SortType Convert(Client.Models.SortType sortType)
        {
            switch (sortType)
            {
                case Client.Models.SortType.Ascending:
                    return SortType.Ascending;

                case Client.Models.SortType.Descending:
                    return SortType.Descending;

                default:
                    throw new ArgumentException($"Unknown sort type \"{sortType}\".", nameof(sortType));
            }
        }
    }
}