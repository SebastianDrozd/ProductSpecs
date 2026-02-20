using ProductSpecs.Models.Auth;

namespace ProductSpecs.Dto.Auth
{
    public class UserResponse
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public List<PermissionResponse> permissions { get; set; } 
        public string? role { get; set; }
    }
}
