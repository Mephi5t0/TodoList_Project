using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using API.Auth;
using API.Errors;
using Microsoft.AspNetCore.Mvc;
using Models.Converters.Users;
using Models.Users;
using Models.Users.Services;

namespace API.Controllers
{
    using Client = global::Client.Models;
    using Model = global::Models.Todo;
    
    [Route("api/users")]
    public class AuthController : Controller
    {
        private UserService userService;
        private IAuthenticator authenticator;
        
        public AuthController(UserService userService, IAuthenticator authenticator)
        {
            this.userService = userService;
            this.authenticator = authenticator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Client.Users.UserRegistrationInfo registrationInfo,
            CancellationToken cancellationToken)
        {
            UserCreationInfo creationInfo = null;
            try
            {
                creationInfo = new UserCreationInfo(registrationInfo.Login, HashPassword(registrationInfo.Password));

            }
            catch (UserDuplicationException e)
            {
                this.BadRequest(creationInfo);
            }
            catch (ArgumentException)
            {
                var error = ServiceErrorResponses.UserIdIsNull("UserRegistrationInfo");
                return this.BadRequest(error);
            } 

            var result = await userService.CreateAsync(creationInfo, cancellationToken);
            var clientUser = UserConverter.Convert(result);
            
            return this.Ok(clientUser);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Client.Auth.Credentials query,
            CancellationToken cancellationToken)
        {     
            SessionState result;
            try
            {
                result = await this.authenticator.AuthenticateAsync(query.Login, query.Password, cancellationToken);
            }
            catch (Exception e)
            {
                return this.BadRequest();
            }

            return this.Ok(result);
        }
        
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var sessionId = HttpContext.Items["SessionId"].ToString();
            
            await authenticator.DeleteSessionAsync(sessionId);

            return this.Ok();
        }
        
        private string HashPassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = md5.ComputeHash(passwordBytes);
                var hash = BitConverter.ToString(hashBytes);
                return hash;
            }
        }
    }
}