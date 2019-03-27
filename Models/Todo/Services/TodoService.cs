using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Models.Todo.Services
{
    public class TodoService
    {
        private readonly IMongoCollection<TodoInfo> todoItems;

        public TodoService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("TodoStoreDb"));
            var database = client.GetDatabase("TodoStoreDb");
            todoItems = database.GetCollection<TodoInfo>("TodoList");
        }

        public Task<List<TodoInfo>> SearchAsync(TodoInfoSearchQuery query, CancellationToken cancelltionToken)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            cancelltionToken.ThrowIfCancellationRequested();

            var search = todoItems.AsQueryable().ToEnumerable();

            if (query.CreatedFrom != null)
            {
                search = search.Where(todoInfo => todoInfo.CreatedAt >= query.CreatedFrom.Value);
            }

            if (query.CreatedTo != null)
            {
                search = search.Where(todoInfo => todoInfo.CreatedAt <= query.CreatedTo.Value);
            }
            
            if (query.DeadLineFrom != null)
            {
                search = search.Where(todoInfo => todoInfo.Deadline >= query.DeadLineFrom.Value);
            }

            if (query.DeadLineTo != null)
            {
                search = search.Where(todoInfo => todoInfo.Deadline <= query.DeadLineTo.Value);
            }

            if (query.UserId != null)
            {
                search = search.Where(todoInfo => todoInfo.UserId == query.UserId);
            }

            if (query.IsCompleted != null)
            {
                search = search.Where(todoInfo => todoInfo.IsCompleted == query.IsCompleted.Value);
            }

            if (query.Offset != null)
            {
                search = search.Skip(query.Offset.Value);
            }

            if (query.Limit != null)
            {
                search = search.Take(query.Limit.Value);
            }

            var sort = query.Sort ?? SortType.Ascending;
            var sortBy = query.SortBy ?? TodoSortBy.Creation;

            if (sort != SortType.Ascending || sortBy != TodoSortBy.Creation)
            {
                DateTime Select(TodoInfo todo)
                {
                    switch (sortBy)
                    {
                        case TodoSortBy.Deadline:
                            return todo.Deadline;

                        case TodoSortBy.Creation:
                            return todo.CreatedAt;

                        default:
                            throw new ArgumentException($"Unknown todo sort by value \"{sortBy}\".", nameof(query));
                    }
                }

                search = sort == SortType.Ascending ?
                    search.OrderBy(Select) :
                    search.OrderByDescending(Select);
            }

            var result = search.ToList();

            return Task.FromResult(result);
        }

        public Task<Todo> GetAsync(string id)
        {
            var search = todoItems.Find(todoItem => todoItem.Id == id);
            
            var result = (Todo) search.FirstOrDefault();

            return Task.FromResult(result);
        }

        public Task<TodoInfo> CreateAsync(TodoCreationInfo creationInfo, CancellationToken cancellationToken)
        {
            if (creationInfo == null)
            {
                throw new ArgumentNullException(nameof(creationInfo));
            }

            cancellationToken.ThrowIfCancellationRequested();

            var now = DateTime.Now;

            var todo = new Todo
            {
                UserId = creationInfo.UserId,
                CreatedAt = now,
                Deadline = creationInfo.Deadline,
                IsCompleted = false,
                Title = creationInfo.Title,
                Description = creationInfo.Description
            };
            
            todoItems.InsertOne(todo);

            return Task.FromResult<TodoInfo>(todo);
        }

        public Task<Todo> PatchAsync(TodoPatchInfo patchInfo, CancellationToken cancelltionToken)
        {
            if (patchInfo == null)
            {
                throw new ArgumentNullException(nameof(patchInfo));
            }

            cancelltionToken.ThrowIfCancellationRequested();
            
            var todo = (Todo) todoItems.Find(item => item.Id == patchInfo.TodoId).FirstOrDefault();
            
            if (todo is null)
            {
                throw new TodoNotFoundExcepction("No todo with " + patchInfo.TodoId + " id");
            }

            if (patchInfo.Title != null)
            {
                todo.Title = patchInfo.Title;
            }

            if (patchInfo.Description != null)
            {
                todo.Description = patchInfo.Description;
            }

            if (patchInfo.Deadline != null)
            {
                todo.Deadline = patchInfo.Deadline.Value;
            }    
            
            if (patchInfo.IsCompleted != null)
            {
                todo.IsCompleted = patchInfo.IsCompleted.Value;
            }
            todoItems.ReplaceOne(task => task.Id == todo.Id, todo);
            
            return Task.FromResult(todo);
        }

        public Task RemoveAsync(TodoInfo todoItemIn)
        {
            todoItems.DeleteOne(todoItem => todoItem.Id == todoItemIn.Id);

            return Task.CompletedTask;
        }

        public Task RemoveAsync(string id)
        {
            todoItems.DeleteOne(todoItem => todoItem.Id == id);

            return Task.CompletedTask;
        }
    }
}