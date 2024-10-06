using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Todo.Application.DTOs;
using Todo.Application.IServices;
using Todo.Infrastructure.EntityFramework;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetUsersAsync()
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

                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting the users");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
           
        }

        [HttpGet("{userID:int}")]
        public async Task<IActionResult> GetUsersTodos(int userID)
        {
            try
            {
                var usersTodos = await _service.GetUserTodosAsync(userID);
                if (usersTodos == null)
                {
                    return NotFound(new
                    {
                        msg = $"User {userID} has no todos"
                    });
                }
                return Ok(_mapper.Map<IEnumerable<UserTodsDTO>>(usersTodos));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while getting the User {userID} todo list");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }
        
        
    }
}
