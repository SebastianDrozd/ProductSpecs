using Microsoft.EntityFrameworkCore;
using ProductSpecs.Models.Auth;

namespace ProductSpecs.Data
{
    public class MysqlDbContext(DbContextOptions<MysqlDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.user_id);
                entity.Property(e => e.user_id)
                         .HasColumnName("user_id")
                         .ValueGeneratedOnAdd();
            });
        }
    }

   
}
