using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Todo.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("/errors")]
        public IActionResult Errors()
        {
            // get the details of the unhandled exception
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionHandler.Error;
            // return a custom (default) error message
            return new JsonResult(new {erroMessage = exception.Message});
        }
    }
}
