using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductSpecs.Dto.Auth;
using ProductSpecs.Services;

namespace ProductSpecs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AuthService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> getusers()
        {
            return Ok(await service.GetAllUsersAsync());
        }
    }
}
