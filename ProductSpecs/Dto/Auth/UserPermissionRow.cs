namespace ProductSpecs.Dto.Auth
{
    public class UserPermissionRow
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string role { get; set; }
        public string permission_desc { get; set; }
        public string department_name { get; set; }
        public int department_id { get; set; }
    }
}
