using Dapper;
using MySqlConnector;
using ProductSpecs.Dto.Auth;
using ProductSpecs.Exceptions;
using ProductSpecs.Models.Auth;

namespace ProductSpecs.Data.Dapper
{
    public class AuthQueries
    {
        private readonly string _connectionString;

        public AuthQueries(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Mysql");
        }

        public async Task<List<UserPermissionRow>> GetUserByUserName(string username)
        {
            try {
                const string sql = @"SELECT u.user_id, u.username, u.role, p.permission_desc,d.department_id, d.department_name
                                  FROM users u 
                                   Join permissions p on u.user_id = p.permission_user_id 
                                  JOIN departments d ON p.permission_dept_id = d.department_id 
                                  where username = @username";
                await using var connection = new MySqlConnection(_connectionString);
                return (await connection.QueryAsync<UserPermissionRow>(sql, new { username })).AsList();
            }
            catch (Exception e) {
                throw new DatabaseException("Sql Error");
            }
           
        }

        public async Task<List<User>> GetAllUsers()
        {
            const string sql = "SELECT * FROM users";

            await using var connection = new MySqlConnection(_connectionString);

            return (await connection.QueryAsync<User>(sql)).AsList();
        }
    }
}
