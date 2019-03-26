using System;
using System.Net;
using Client.Models.Errors;

namespace API.Errors
{
    public static class ServiceErrorResponses
    {
        public static ServiceErrorResponse TodoNotFound(string todoId)
        {
            if (todoId == null)
            {
                throw new ArgumentNullException(nameof(todoId));
            }

            var error = new ServiceErrorResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Error = new ServiceError
                {
                    Code = ServiceErrorCodes.NotFound,
                    Message = $"A todo with \"{todoId}\" not found.",
                    Target = "todo"
                }
            };

            return error;
        }

        public static ServiceErrorResponse BodyIsMissing(string target)
        {
            var error = new ServiceErrorResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Error = new ServiceError
                {
                    Code = ServiceErrorCodes.BadRequest,
                    Message = "Request body is empty.",
                    Target = target
                }
            };

            return error;
        }
        
        public static ServiceErrorResponse UserIdIsNull(string target)
        {
            var error = new ServiceErrorResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Error = new ServiceError
                {
                    Code = ServiceErrorCodes.BadRequest,
                    Message = "User Id is null.",
                    Target = target
                }
            };

            return error;
        }
    }
}
