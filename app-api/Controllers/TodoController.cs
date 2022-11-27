using app_domain.Core;
using app_domain.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodos()
        {
            var todos = await _unitOfWork.Todos.GetAll();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodoById(int id)
        {
            var todoFromDb = await _unitOfWork.Todos.GetById(id);

            if (todoFromDb == null) return NotFound();

            return Ok(todoFromDb);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTodo(string title, int userId, bool completed)
        {
            await _unitOfWork.Todos.Add(new Todo { Title = title, UserId = userId, Completed = completed });
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTodo(Todo todo)
        {
            await _unitOfWork.Todos.Update(todo);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            var todoDb = await _unitOfWork.Todos.GetById(id);

            if (todoDb == null) return NotFound();

            await _unitOfWork.Todos.Delete(todoDb);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
