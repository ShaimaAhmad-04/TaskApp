using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using To_Do_App.Model;

namespace To_Do_App
{
    public class ToDoApp_DbContext:DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lookup>().HasData(
                new Lookup { Id = 1, MajorCode = 0, MinorCode = 0, Name = "Priority" },
                new Lookup { Id = 2, MajorCode = 0, MinorCode = 1, Name = "High" },
                new Lookup { Id = 3, MajorCode = 0, MinorCode = 2, Name = "Medium" },
                new Lookup { Id = 4, MajorCode = 0, MinorCode = 3, Name = "Low" }

                );
            modelBuilder.Entity<User>().HasData(
             new User { Id = 1, Name = "Admin", IsAdmin = true,Email = "Admin@outlook.com", HashedPassword = "$2a$11$uWv8KLbUXU91vzLQTSKcqerESQHThdPrNKKk6LQ8gEAZY7TCEE6GW" }
             );
        }

        public ToDoApp_DbContext(DbContextOptions<ToDoApp_DbContext> option):base(option)
        {

        }

        public DbSet<TaskItem> Tasks { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
    }
}
