using Microsoft.EntityFrameworkCore;
using ProductSpecs.Data;
using ProductSpecs.Dto.Auth;

namespace ProductSpecs.Services
{
    public class AuthService(MysqlDbContext context)
    {
        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            return await context.Users.Select(u => new UserResponse
            {
             user_id = u.user_id,
             username = u.username,
             password = u.password,
             role = u.role
            }).ToListAsync();
        }

        public async Task<UserResponse> GetUserByIdAsync(int id)
        {
            var result = await context.Users
                .Where(u => u.user_id == id)
                .Select(u => new UserResponse
                {
                    user_id = u.user_id,
                    username = u.username,
                    password = u.password,
                    role = u.role
                })
                .FirstOrDefaultAsync();
            return result;
                
        }
    }
}
