namespace ProductSpecs.Dto.Auth
{
    public class AuthSession
    {
        public string session_id { get; set; } = "";
        public int user_id { get; set; }
        public string username { get; set; } = "";
        public string? role { get; set; }
        public DateTime expires_utc { get; set; }
        public DateTime created_utc { get; set; }
        public DateTime? last_seen_utc { get; set; }
    }
}
