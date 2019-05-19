using System.Net;
using System.Windows;
using API.Client.Models.Users;
using API.Network;

namespace API
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private async void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            var password = Password.Password;
            var confirmationPwd = ConfirmPassword.Password;
            const string emptyStr = "";

            if (Login.Text == emptyStr || Password.Password == emptyStr)
            {
                Error.Text = "Please, enter Login and Password";
                return;
            }
            
            if (password != confirmationPwd)
            {
                Error.Text = "The Password and confirmation password do not match";
                return;
            }

            var registrationInfo = new UserRegistrationInfo
            {
                Login = Login.Text,
                Password = Password.Password
            };

            HttpStatusCode responseCode;
            try
            {
                responseCode = await NetworkWorker.Register(registrationInfo);
            }
            catch
            {
                Error.Text = "Server is unavailable";
                return;
            }
            
            switch (responseCode)
            {
                case HttpStatusCode.OK:
                    Close();
                    break;
                case HttpStatusCode.Conflict:
                    Error.Text = "User with such login already exists";
                    break;
            }
        }

        private void Reset_Clicked(object sender, RoutedEventArgs e)
        {
            Login.Clear();
            Password.Clear();
            ConfirmPassword.Clear();
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}