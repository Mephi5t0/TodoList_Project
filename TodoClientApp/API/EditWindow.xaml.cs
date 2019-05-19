using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Windows;
using API.Client.Models.Todo;
using API.Network;
using Newtonsoft.Json;

namespace API
{
    public partial class EditWindow : Window
    {
        private TodoCollection Todos;
        private string id;
        
        public EditWindow(string id, TodoCollection todos)
        {
            this.id = id;
            Todos = todos;
            
            InitializeComponent();
            InitializeFields();
        }

        private void InitializeFields()
        {
            var todo = Todos[GetIndexOfTodoById(id)];
            Title.Text = todo.Title;
            Deadline.Text = todo.Deadline.ToString("dd.MM.yyyy hh:mm:ss");
            IsCompleted.IsChecked = todo.IsCompleted;
            
            HttpResponseMessage response;

            try
            {
                response =  NetworkWorker.GetTodoById(id).Result;
            }
            catch
            {
                return;
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var serverTodo = JsonConvert.DeserializeObject<Todo>(responseContent);
                Description.Text = serverTodo.Description;
            }
        }

        private async void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            if (!DateTime.TryParse(Deadline.Text, out var deadline))
            {
                Error.Text = "Deadline has inappropriate form";
                return;
            }

            var isCompleted = IsCompleted.IsChecked;

            var pathInfo = new TodoPatchInfo
            {
                Title = Title.Text,
                Description = Description.Text,
                Deadline = deadline,
                IsCompleted = isCompleted
            };

            HttpResponseMessage response;

            try
            {
                response = await NetworkWorker.PatchTodo(pathInfo, id);
            }
            catch
            {
                Error.Text = "Server is unavailable";
                return;
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var serverTodo = JsonConvert.DeserializeObject<Todo>(responseContent);
                var index = GetIndexOfTodoById(serverTodo.Id);

                Todos[index] = serverTodo;
            }
            else
            {
                return;
            }
            
            Close();
        }

        private void Delete_Clicked(object sender, RoutedEventArgs e)
        {
            HttpResponseMessage response;

            try
            {
                response = NetworkWorker.DeleteTodo(id).Result;
            }
            catch
            {
                Error.Text = "Server is unavailable";
                return;
            }

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                Todos.Remove(Todos[GetIndexOfTodoById(id)]);
                Close();
            }
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private int GetIndexOfTodoById(string todoId)
        {
            for (var i = 0; i < Todos.Count; ++i)
            {
                if (Todos[i].Id == todoId)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}