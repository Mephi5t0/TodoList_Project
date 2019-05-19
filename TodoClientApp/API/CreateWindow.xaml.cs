using System;
using System.Net;
using System.Net.Http;
using System.Windows;
using API.Client.Models.Todo;
using API.Network;
using Newtonsoft.Json;

namespace API
{
    public partial class CreateWindow : Window
    {
        private TodoCollection Todos;
        
        public CreateWindow(TodoCollection todos)
        {
            Todos = todos;
            InitializeComponent();
        }

        private async void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            const string emptyString = "";

            if (Title.Text == emptyString || Description.Text == emptyString || Deadline.Text == emptyString)
            {
                Error.Text = "Fill all fields, please";
                return;
            }

            if (!DateTime.TryParse(Deadline.Text, out var deadline))
            {
                Error.Text = "Deadline has inappropriate form";
                return;
            }

            var buildInfo = new TodoBuildInfo
            {
                Title = Title.Text,
                Description = Description.Text,
                Deadline = deadline
            };
            
            HttpResponseMessage response;

            try
            {
                response = await NetworkWorker.CreateTodo(buildInfo);
            }
            catch
            {
                Error.Text = "Server is unavailable";
                return;
            }

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var serverTodo = JsonConvert.DeserializeObject<Todo>(responseContent);
                
                Todos.Add(serverTodo);
                
                Close();
            }
            else
            {
                Error.Text = "Server error occured";
            }
        }

        private void Reset_Clicked(object sender, RoutedEventArgs e)
        {
            Title.Clear();
            Description.Clear();
            Deadline.Clear();
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}