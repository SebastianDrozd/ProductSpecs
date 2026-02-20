using Dapper;
using MySqlConnector;
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

        public async Task<User> GetUserByUserName(string username)
        {
            const string sql = @"Select * from users where username = @username";
            await using var connection = new MySqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<User>
                (sql,
                new { username }
                );
        }

        public async Task<List<User>> GetAllUsers()
        {
            const string sql = "SELECT * FROM users";

            await using var connection = new MySqlConnection(_connectionString);

            return (await connection.QueryAsync<User>(sql)).AsList();
        }
    }
}
