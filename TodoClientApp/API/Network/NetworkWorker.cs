using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API.Client.Models.Todo;
using API.Client.Models.Users;
using API.Data;
using Newtonsoft.Json;

namespace API.Network
{
    public static class NetworkWorker
    {
        private static string Url = "https://localhost:5001";

        public static Task<HttpResponseMessage> Login(UserLoginInfo loginInfo)
        {
            var json = JsonConvert.SerializeObject(loginInfo);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(Url + "/api/users/login", stringContent).Result;
                return Task.FromResult(response);
            }
        }
        
        public static Task<HttpStatusCode> Register(UserRegistrationInfo registrationInfo)
        {
            var json = JsonConvert.SerializeObject(registrationInfo);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(Url + "/api/users/register", stringContent).Result;
                return Task.FromResult(response.StatusCode);
            }
        }

        public static Task<HttpResponseMessage> GetAllTodos()
        {
            using (var client = new HttpClient())
            {
                AddSessionHeader(client);
                var response = client.GetAsync(Url + "/api/todo").Result;
                return Task.FromResult(response);
            }
        }

        public static Task<HttpResponseMessage> GetTodoById(string id)
        {
            using (var client = new HttpClient())
            {
                AddSessionHeader(client);
                var response = client.GetAsync(Url + "/api/todo/" + id).Result;
                return Task.FromResult(response);
            }
        }

        public static Task<HttpResponseMessage> PatchTodo(TodoPatchInfo patchInfo, string id)
        {
            var method = new HttpMethod("PATCH");
            var json = JsonConvert.SerializeObject(patchInfo);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(method, Url + "/api/todo/" + id) {
                Content = stringContent
            };
            
            using (var client = new HttpClient())
            {
                AddSessionHeader(client);
                var response = client.SendAsync(request).Result;
                return Task.FromResult(response);
            }
        }
        
        public static Task<HttpResponseMessage> CreateTodo(TodoBuildInfo buildInfo)
        {
            var json = JsonConvert.SerializeObject(buildInfo);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            
            using (var client = new HttpClient())
            {
                AddSessionHeader(client);
                var response = client.PostAsync(Url + "/api/todo", stringContent).Result;
                return Task.FromResult(response);
            }
        }

        public static Task<HttpResponseMessage> DeleteTodo(string id)
        {
            using (var client = new HttpClient())
            {
                AddSessionHeader(client);
                var response = client.DeleteAsync(Url + "/api/todo/" + id).Result;
                return Task.FromResult(response);
            }
        }
        
        private static void AddSessionHeader(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("SessionId", SessionData.SessionState.SessionId);
        }
    }
}