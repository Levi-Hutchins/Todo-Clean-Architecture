using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Todo.Application.DTOs;
using Todo.Application.IServices;
using Todo.Domain.Models;
using Todo.Infrastructure.EntityFramework;

namespace Todo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(IUserService service, ILogger<UserController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersAsync()
        {
            try
            {
                var users = await _service.GetUsersAsync();
                if (users == null)
                {
                    return NotFound(new
                    {
                        msg = "No users found"
                    });
                }

                return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting the users");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
           
        }
        [HttpGet("{userId:int}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetUserByIdAsync(int userId)
        {
            try
            {

                if (userId <= 0)
                {
                    return BadRequest(new ProblemDetails
                    {
                        Title = "Invalid User ID",
                        Detail = $"The provided user ID {userId} is invalid.",
                        Status = StatusCodes.Status400BadRequest
                    });
                }

                var user = await _service.GetUserAsync(userId);
                if (user == null)
                {
                    return NotFound(new ProblemDetails
                    {
                        Title = "User Not Found",
                        Detail = $"User with ID {userId} was not found.",
                        Status = StatusCodes.Status404NotFound
                    });
                }
                
                return Ok(_mapper.Map<UserDTO>(user));

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while getting user {userId}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        [HttpGet("{userId:int}/todos")]
        [ProducesResponseType(typeof(IEnumerable<UserTodsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserTodsDTO>>> GetUsersTodos(int userId)
        {
            try
            {
                var usersTodos = await _service.GetUserTodosAsync(userId);
                if (usersTodos == null)
                {
                    return NotFound(new
                    {
                        msg = $"User {userId} has no todos"
                    });
                }
                return Ok(_mapper.Map<IEnumerable<UserTodsDTO>>(usersTodos));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while getting the User {userId} todo list");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUserAsync([FromBody] UserDTO newUser)
        {
            var user = _mapper.Map<User>(newUser);
            
            var addedUser = await _service.CreateUserAsync(user);
            
            _logger.LogInformation(JsonSerializer.Serialize(addedUser));
            if (!addedUser.Success)
            {
                return Conflict(addedUser.ErrorMessage);
            }
            return CreatedAtAction(nameof(GetUserByIdAsync), new { userId = user.Id }, _mapper.Map<UserDTO>(user));

        }
        
        
    }
}
