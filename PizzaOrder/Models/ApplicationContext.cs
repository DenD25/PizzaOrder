using Microsoft.EntityFrameworkCore;

namespace PizzaOrder.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pizza>? Pizzas { get; set; }
        public DbSet<PizzaComponent>? PizzaComponents { get; set; }

        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Pizza>()
                .HasMany(t => t.PizzaComponents)
                .WithMany(t => t.Pizzas);

            modelBuilder
                .Entity<PizzaComponent>()
                .HasMany(t => t.Pizzas)
                .WithMany(t => t.PizzaComponents);

            modelBuilder
                .Entity<Role>()
                .HasMany(t => t.Users)
                .WithOne(t => t.Role)
                .HasForeignKey(t => t.RoleId)
                .HasPrincipalKey(t => t.Id);

            // Adding roles
            Role adminRole = new Role { Id = 1, Name = "admin" };
            Role userRole = new Role { Id = 2, Name = "user" };
            Role managerRole = new Role { Id = 2, Name = "manager" };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, managerRole, userRole });
        }
    }
}
