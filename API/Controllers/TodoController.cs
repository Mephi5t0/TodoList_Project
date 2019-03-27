using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Auth;
using API.Errors;
using Microsoft.AspNetCore.Mvc;
using Models.Converters.Todo;
using Models.Todo.Services;

namespace API.Controllers
{
    using Client = global::Client.Models.Todo;
    using Model = global::Models.Todo;

    [Route("api/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoService todoService;

        public TodoController(TodoService todoService)
        {
            this.todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult> SearchTodoItems([FromQuery] Client.TodoInfoSearchQuery query,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userId = HttpContext.Items["UserId"].ToString();

            if (userId != query.UserId)
            {
                return this.StatusCode(403);
            }
            
            var modelQuery = TodoInfoSearchQueryConverter.Convert(query ?? new Client.TodoInfoSearchQuery());
            var modelNotes = await todoService.SearchAsync(modelQuery, cancellationToken).ConfigureAwait(false);
            var clientNotes = modelNotes.Select(note => TodoInfoConverter.Convert(note)).ToList();
            var clientNotesList = new Client.TodoList
            {
                todoItems = clientNotes
            };

            return this.Ok(clientNotesList);
        }

        [HttpGet("{id:length(24)}", Name = "GetTodo")]
        public async Task<ActionResult> GetAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var todoItem = await todoService.GetAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }
            
            var userId = HttpContext.Items["UserId"].ToString();

            if (userId != todoItem.UserId)
            {
                return this.StatusCode(403);
            }

            var modelItem = TodoConverter.Convert(todoItem);

            return Ok(modelItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoAsync([FromBody] Client.TodoBuildInfo buildInfo,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (buildInfo == null)
            {
                var error = ServiceErrorResponses.BodyIsMissing("TodoBuildInfo");
                return this.StatusCode(403);
            }

            var userId = HttpContext.Items["UserId"];

            var creationInfo = TodoBuildInfoConverter.Convert(userId.ToString(), buildInfo);
            var modelTodoInfo =
                await this.todoService.CreateAsync(creationInfo, cancellationToken).ConfigureAwait(false);
            var clientTodoInfo = TodoInfoConverter.Convert(modelTodoInfo);

            return this.Ok(clientTodoInfo);
        }

        [HttpPatch]
        [Route("{id:length(24)}")]
        public async Task<IActionResult> PatchTodoAsync([FromRoute] string id,
            [FromBody] Client.TodoPatchInfo patchInfo, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var userId = HttpContext.Items["UserId"].ToString();
            var todoItem = await todoService.GetAsync(id);
            
            if (userId != todoItem.UserId)
            {
                return this.StatusCode(403);
            }

            if (patchInfo == null)
            {
                var error = ServiceErrorResponses.BodyIsMissing("TodoPatchInfo");
                return this.BadRequest(error);
            }

            var modelPathInfo = TodoPathcInfoConverter.Convert(id, patchInfo);

            Model.Todo modelTodo;

            try
            {
                modelTodo = await this.todoService.PatchAsync(modelPathInfo, cancellationToken).ConfigureAwait(false);
            }
            catch (Model.TodoNotFoundExcepction)
            {
                var error = ServiceErrorResponses.TodoNotFound(id);
                return this.NotFound(error);
            }

            var clientNote = TodoConverter.Convert(modelTodo);

            return this.Ok(clientNote);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                var error = ServiceErrorResponses.UserIdIsNull(id);
                return this.BadRequest(error);
            }

            var todoItem = await todoService.GetAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }
            
            var userId = HttpContext.Items["UserId"].ToString();
            
            if (userId != todoItem.UserId)
            {
                return this.StatusCode(403);
            }

            await todoService.RemoveAsync(id);

            return NoContent();
        }
    }
}