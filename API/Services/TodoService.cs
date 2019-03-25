using System.Collections.Generic;
using API.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace API.Services
{
    public class TodoService
    {
        private readonly IMongoCollection<TodoItem> todoItems;

        public TodoService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("TodoStoreDb"));
            var database = client.GetDatabase("TodoStoreDb");
            todoItems = database.GetCollection<TodoItem>("TodoList");
        }

        public List<TodoItem> Get()
        {
            return todoItems.Find(todoItem => true).ToList();
        }

        public TodoItem Get(string id)
        {
            return todoItems.Find(todoItem => todoItem.Id == id).FirstOrDefault();
        }

        public TodoItem Create(TodoItem todo)
        {
            todoItems.InsertOne(todo);
            return todo;
        }

        public void Update(string id, TodoItem todoItemIn)
        {
            todoItems.ReplaceOne(todoItem => todoItem.Id == id, todoItemIn);
        }

        public void Remove(TodoItem todoItemIn)
        {
            todoItems.DeleteOne(todoItem => todoItem.Id == todoItemIn.Id);
        }

        public void Remove(string id)
        {
            todoItems.DeleteOne(todoItem => todoItem.Id == id);
        }
    }
}