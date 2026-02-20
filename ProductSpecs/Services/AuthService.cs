using Microsoft.EntityFrameworkCore;
using ProductSpecs.Data;
using ProductSpecs.Data.Dapper;
using ProductSpecs.Dto.Auth;
using ProductSpecs.Exceptions;
using ProductSpecs.Models.Auth;
using System.Text.Json;

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
            //check if username/password is empty
            if (string.IsNullOrWhiteSpace(userLogin.username) || string.IsNullOrWhiteSpace(userLogin.password))
                throw new BadRequestException("UserName and password are required");

            //check AD to see if user exists
            var reponse = ldapService.ValidateUser(userLogin.username, userLogin.password);

            //if does not exist throw
            if (!reponse)
                throw new UnauthorizedException("Wrong Username/Password");

            //grab user permissions
            var userPermissionsRow = await queries.GetUserByUserName(userLogin.username);

            //map permissions to list
            List < PermissionResponse > permissions = new List< PermissionResponse >();
            foreach(UserPermissionRow upr in userPermissionsRow) {
               // Console.WriteLine(upr.department_name);
                permissions.Add(new PermissionResponse
                {
                    department_id = upr.department_id,
                    department_name = upr.department_name,
                    permission_desc = upr.permission_desc,

                });
                
            }
            //generaete the full user with permissions
            var user = new UserResponse
            {
                user_id = userPermissionsRow[0].user_id,
                username = userPermissionsRow[0].username,
                permissions = permissions,
                role = userPermissionsRow[0].role
            };


            return true;
            
        }
    }
}
