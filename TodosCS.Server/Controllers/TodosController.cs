using Microsoft.AspNetCore.Mvc;
using TodosCS.Server.Data.Repositories;
using TodosCS.Server.Models;

namespace TodosCS.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodosController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _todoRepository.GetAll();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var todo = await _todoRepository.GetById(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Todo todo)
        {
            await _todoRepository.Create(todo);
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Todo todo)
        {
            var updatedTodo = await _todoRepository.Update(id, todo);

            if (updatedTodo == null)
            {
                return NotFound();
            }

            return Ok(updatedTodo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var is_removed = await _todoRepository.Delete(id);

            if (is_removed)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}

