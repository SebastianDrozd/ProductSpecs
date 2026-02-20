using ProductSpecs.Data.Dapper;
using System.Security.Claims;

namespace ProductSpecs.Middleware
{
    public class SessionAuthMiddleware
    {
        public const string CookieName = "ps_sid";

        private readonly RequestDelegate _next;

        public SessionAuthMiddleware(RequestDelegate next) 
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, SessionQueries sessions) 
        {
            if (context.Request.Cookies.TryGetValue(CookieName, out var sid) && !string.IsNullOrWhiteSpace(sid)) 
            {
                var session = await sessions.GetSessionASync(sid);

                if (sessions != null && session.expires_utc > DateTime.UtcNow) 
                {
                    _ = sessions.TouchSessionAsync(sid);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,session.user_id.ToString()),
                        new Claim(ClaimTypes.Name, session.username)
                    };

                    if (!string.IsNullOrWhiteSpace(session.role)) 
                    {
                        claims.Add(new Claim(ClaimTypes.Role, session.role));
                    }
                    var identity = new ClaimsIdentity(claims,authenticationType: "Session");
                    context.User = new ClaimsPrincipal(identity);
                }
            }
            await _next(context);
        }
    }
}
