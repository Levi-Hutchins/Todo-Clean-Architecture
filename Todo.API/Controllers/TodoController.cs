using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.DTOs;
using Todo.Application.IServices;
using Todo.Domain.Models;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ILogger<TodoController> _logger;
        private readonly IMapper _mapper;

        public TodoController(ITodoService todoService, ILogger<TodoController> logger, IMapper mapper)
        {
            _todoService = todoService;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTodoById(int id)
        {
            try
            {
                var todo = await _todoService.GetTodoByIdAsync(id);
                if (todo == null) return NotFound(new
                {
                    msg = $"Todo {id} was not found."
                });

                // Map the entity to a DTO to prevent data leakage, and reduce coupling
                return Ok(_mapper.Map<TodoDTO>(todo));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting the todo item with id {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
           
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoDTO>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTodoListAsync()
        {
            try
            {
                var todos = await _todoService.GetTodosAsync();
                if (!todos.Any()) return Ok(new List<TodoDTO>());

                return Ok(_mapper.Map<IEnumerable<TodoDTO>>(todos));
            } 
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting the todos");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTodoAsync([FromBody] CreateTodoDTO createTodo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var todo = _mapper.Map<Todos>(createTodo);
                todo.UserId = 1002; // Test User ID
                _logger.LogInformation(JsonSerializer.Serialize(todo));
                await _todoService.AddTodoAsync(todo);
                return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, _mapper.Map<TodoDTO>(todo));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while creating a todo");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTodoAsync([FromBody] UpdateTodoDTO updateTodo,int id)
        {
            //TODO: fix the model changing things that have no changed
            try
            {
                var todoByID = await _todoService.GetTodoByIdAsync(id);
                if (todoByID == null)
                {
                    return NotFound($"Todo {id} was not found");
                }
                var todoUpdate = _mapper.Map(updateTodo, todoByID);
                _logger.LogInformation(JsonSerializer.Serialize(todoUpdate));
                await _todoService.UpdateTodoAsync(todoUpdate);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while updating the todo {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
           
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteTodoDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTodoAsync(int id)
        {
            try
            {
                var todoByID = await _todoService.GetTodoByIdAsync(id);
                if (todoByID == null)
                {
                    return NotFound($"ID {id} was not found.");
                }

                await _todoService.DeleteTodoAsync(id);
                return Ok(_mapper.Map<DeleteTodoDTO>(todoByID));

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while deleting the todo {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
            
            
            
        }
    }
}
