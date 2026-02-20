using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductSpecs.Exceptions;

namespace ProductSpecs.Controllers
{
    
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var ex = feature?.Error;
            if (ex is null)
                return Problem(statusCode: 500, title: "Unknown Error");
            _logger.LogError(ex, "Unhandled exception at {Path}", feature?.Path);
            return ex switch
            {
                BadRequestException bre => Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: bre.Message),

                UnauthorizedException ue => Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: ue.Message),

                ServiceUnavailableException sue => Problem(
                    statusCode: StatusCodes.Status503ServiceUnavailable,
                    title: sue.Message),

                _ => Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "An unexpected error occurred.",
                    extensions: new Dictionary<string, object?>
                    {
                        ["traceId"] = HttpContext.TraceIdentifier
                    })
            };
        }
    }
}
