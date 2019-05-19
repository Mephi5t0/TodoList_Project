using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using API.Client.Models.Todo;
using API.Data;
using API.Network;
using Newtonsoft.Json;
using MessageBox = System.Windows.MessageBox;

namespace API
{
    public partial class TodoList : Window
    {
        private TodoCollection Todos;

        public TodoList()
        {
            InitializeComponent();
            Todos = (TodoCollection) this.TryFindResource("MyTodoList");
            HeadText.Text = SessionData.Login + "`s TodoList:";
            DisplayAllTodos();
        }

        private void Create_Clicked(object sender, RoutedEventArgs e)
        {
            var createWindow = new CreateWindow(Todos);

            createWindow.ShowDialog();
        }

        private async void DisplayAllTodos()
        {
            HttpResponseMessage response;

            try
            {
                response = await Task.Run(NetworkWorker.GetAllTodos);
            }
            catch
            {
                return;
            }

            var responseContent = response.Content.ReadAsStringAsync().Result;
            var serverTodos = JsonConvert.DeserializeObject<TodoInfoList>(responseContent);

            foreach (var serverTodo in serverTodos.TodoItems)
            {
                Todos.Add(serverTodo);
            }
        }

        private async void More_Clicked(object sender, RoutedEventArgs e)
        {
            var clickedObject = e.Source as FrameworkElement;

            if (clickedObject == null)
            {
                return;
            }

            var todoId = clickedObject.Uid;
            HttpResponseMessage response;

            try
            {
                response = await NetworkWorker.GetTodoById(todoId);
            }
            catch
            {
                return;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Close();
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var todo = JsonConvert.DeserializeObject<Todo>(responseContent);
                var info = MessageBox.Show(todo.Description,
                    "Description",
                    MessageBoxButton.OK,
                    MessageBoxImage.None);
            }
        }

        private void Edit_Clicked(object sender, RoutedEventArgs e)
        {
            var clickedObject = e.Source as FrameworkElement;

            if (clickedObject == null)
            {
                return;
            }

            var todoId = clickedObject.Uid;
            var editWindow = new EditWindow(todoId, Todos);

            editWindow.ShowDialog();
        }

        private void Back_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}