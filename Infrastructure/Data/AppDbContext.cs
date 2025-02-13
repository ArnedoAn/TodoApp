using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TodoItem> TodoItems => Set<TodoItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem { Id = 1, Title = "Estudiar contenedores", IsComplete = false },
                new TodoItem { Id = 2, Title = "Implementar contenedores", IsComplete = false }
            );
        }
    }
}
