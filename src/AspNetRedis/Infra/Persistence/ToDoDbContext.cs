using AspNetRedis.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetRedis.Infra.Persistence
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>()
                .HasKey(t => t.Id);
        }
    }
}
