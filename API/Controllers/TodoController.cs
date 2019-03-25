using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
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
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            return todoService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetTodo")]
        public ActionResult<TodoItem> Get(string id)
        {
            var todoItem = todoService.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public ActionResult<TodoItem> Create(TodoItem todoItem)
        {
            todoService.Create(todoItem);

            return CreatedAtRoute("GetTodo", new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, TodoItem todoItemIn)
        {
            var todoItem = todoService.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            todoService.Update(id, todoItemIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var todoItem = todoService.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            todoService.Remove(todoItem.Id);

            return NoContent();
        }
    }
}