using Dapper;
using MySqlConnector;
using ProductSpecs.Dto.Auth;

namespace ProductSpecs.Data.Dapper
{
    public class SessionQueries
    {
        private readonly string _connectionString;

        public SessionQueries(IConfiguration config) 
        {
            _connectionString = config.GetConnectionString("Mysql");
        }

        public async Task CreateSessionAsync(AuthSession session) 
        {
            const string sql = 
                @"Insert into auth_sessions
                 (session_id,user_id,username,role,expires_utc,created_utc,last_seen_utc)
                 values (@session_id,@user_id,@username,@role,@expires_utc,@created_utc,@last_seen_utc)";
            await using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(sql, session);
        }
        public async Task<AuthSession?> GetSessionASync(string sessionId) 
        {
            const string sql = 
                @"Select session_id,user_id,username,role,expires_utc,created_utc,last_seen_utc
                    from auth_sessions
                    where session_id = @sessionId";
            await using var connection = new MySqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<AuthSession>(sql, new { sessionId });
        }

        public async Task TouchSessionAsync(string sessionId) 
        {
            const string sql =
                @"Update auth_sessions
                    SET last_seen_utc = UTC_TIMESTAMP()
                    WHERE session_id = @session_id";
            await using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(sql, new { sessionId });
        }

        public async Task DeleteSessionAsync(string sessionId) 
        {
            const string sql = @"Delete from auth_sessions where session_id = @session_id";
            await using var connection = new MySqlConnection(_connectionString);
            await connection.ExecuteAsync(sql, new {sessionId});
        }
    }
}
