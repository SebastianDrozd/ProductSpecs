namespace ProductSpecs.Dto.Auth
{
    public class LoginResponse
    {
        public string SessionId { get; set; }
        public UserResponse User { get; set; }
    }
}
