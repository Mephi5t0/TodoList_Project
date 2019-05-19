using System.Net;
using System.Net.Http;
using System.Windows;
using API.Client.Models.Auth;
using API.Client.Models.Users;
using API.Data;
using API.Network;
using Newtonsoft.Json;

namespace API
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, RoutedEventArgs e)
        {
            const string emptyStr = "";

            if (Login.Text == emptyStr || Password.Password == emptyStr)
            {
                Error.Text = "Please, enter login and password";
                return;
            }

            var loginInfo = new UserLoginInfo
            {
                Login = Login.Text,
                Password = Password.Password
            };

            HttpResponseMessage response;
            try
            {
                response = await NetworkWorker.Login(loginInfo);
            }
            catch
            {
                Error.Text = "Server is unavailable";
                return;
            }

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    Error.Text = "Invalid login or password";
                    return;
                case HttpStatusCode.OK:
                {
                    SessionData.Login = Login.Text;
                    SaveSessionState(response);
                    Hide();
                    var todoListWindow = new TodoList();
                    todoListWindow.ShowDialog();
                    Show();
                    return;
                }
                default:
                    Error.Text = "Server error occured";
                    return;
            }
        }

        private void Register_Clicked(object sender, RoutedEventArgs e)
        {
            var registationWindow = new Registration();

            Hide();
            registationWindow.ShowDialog();
            Show();
        }

        private void SaveSessionState(HttpResponseMessage response)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var state = JsonConvert.DeserializeObject<SessionState>(responseContent);

            SessionData.SessionState = state;
        }
    }
}