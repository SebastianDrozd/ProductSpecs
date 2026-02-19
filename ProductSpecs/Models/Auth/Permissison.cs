namespace ProductSpecs.Models.Auth
{
    public class Permissison
    {
        public int permission_id { get; set; }
        public int permission_user_id { get; set; }
        public int permission_dept_id { get; set; }
        public string permission_desc { get; set; }
    }
}
