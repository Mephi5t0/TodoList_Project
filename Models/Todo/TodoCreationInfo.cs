using System;

namespace Models.Todo
{
    /// <summary>
    /// Информация для создания задачи
    /// </summary>
    public class TodoCreationInfo
    {
        /// <summary>
        /// Создает задачу
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, который хочет создать задачу</param>
        /// <param name="title">Заголовок задачи</param>
        /// <param name="description">Текст задачи</param>
        /// <param name="deadline">Дедлайн задачи</param>
        public TodoCreationInfo(string userId, string title, string description, DateTime deadline)
        {
            this.UserId = userId;
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Description = description ?? throw new ArgumentNullException(nameof(description));
            this.Deadline = deadline;
        }

        /// <summary>
        /// Идентификатор пользователя, который хочет создать задачу
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Заголовок задачи
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Дедлайн задачи
        /// </summary>
        public DateTime Deadline { get; }
    }
}
