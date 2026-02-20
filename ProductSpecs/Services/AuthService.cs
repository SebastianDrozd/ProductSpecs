using Microsoft.EntityFrameworkCore;
using ProductSpecs.Data;
using ProductSpecs.Data.Dapper;
using ProductSpecs.Dto.Auth;
using ProductSpecs.Exceptions;
using ProductSpecs.Models.Auth;

namespace ProductSpecs.Services
{
    public class AuthService(AuthQueries queries,LdapService ldapService)
    {
        public async Task<List<User>> GetAllUsersAsync()
        {
            //throw new Exception("fuck");
            return await queries.GetAllUsers();
           // return await context.Users.Select(u => new UserResponse
           // {
         //    user_id = u.user_id,
          //   username = u.username,
           //  password = u.password,
          //   role = u.role
           // }).ToListAsync();
        }

       // public async Task<UserResponse> GetUserByIdAsync(int id)
      //  {
      //      var result = await context.Users
      //          .Where(u => u.user_id == id)
      //          .Select(u => new UserResponse
       //         {
       //             user_id = u.user_id,
        //            username = u.username,
         //           password = u.password,
          //          role = u.role
           //     })
          //     .FirstOrDefaultAsync();
         //   return result;
                
       // }
//
        public async Task<bool> LoginUserAsync(UserLogin userLogin)
        {
            if (string.IsNullOrWhiteSpace(userLogin.username) || string.IsNullOrWhiteSpace(userLogin.password))
                throw new BadHttpRequestException("UserName and password are required");


            var reponse = ldapService.ValidateUser(userLogin.username, userLogin.password);

            if (!reponse)
                throw new UnauthorizedException("Wrong credentials");

            return true;
            
        }
    }
}
