using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductSpecs.Dto.Auth;
using ProductSpecs.Middleware;
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
      //  [HttpGet("{id}")]
       // public async Task<ActionResult<UserResponse>> getUser(int id)
      //  {
      //      return Ok(await service.GetUserByIdAsync(id));
      //  }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> LoginUser(UserLogin userLogin)
        {
            LoginResponse result = await service.LoginUserAsync(userLogin);

            Response.Cookies.Append(SessionAuthMiddleware.CookieName, result.SessionId, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddHours(4),
                Path = "/"
            });
            return Ok(result.User);
           // return Ok(await service.LoginUserAsync(userLogin));
        }
    }
}
