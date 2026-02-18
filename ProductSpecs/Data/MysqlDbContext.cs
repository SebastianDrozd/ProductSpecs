using Microsoft.EntityFrameworkCore;
using ProductSpecs.Models.Auth;

namespace ProductSpecs.Data
{
    public class MysqlDbContext(DbContextOptions<MysqlDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
    }
}
