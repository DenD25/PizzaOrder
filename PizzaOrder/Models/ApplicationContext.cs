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

            modelBuilder
                .Entity<OrderUser>()
                .HasMany(t => t.Pizzas)
                .WithMany(t => t.OrderUsers);

            modelBuilder
                .Entity<OrderAnonymous>()
                .HasMany(t => t.Pizzas)
                .WithMany(t => t.OrderAnonymous);

            // Adding roles
            Role adminRole = new Role { Id = 1, Name = "admin" };
            Role userRole = new Role { Id = 2, Name = "user" };
            Role managerRole = new Role { Id = 3, Name = "manager" };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, managerRole, userRole });

            // Adding users

            User adminUser = new User { Id = 1, Login = "admin", Password = "admin", RoleId = adminRole.Id, PhoneNumber = 999999999, City = "Warsaw", StreetName = "Gdanska", HouseNumber  = "1"};
            User managerUser = new User { Id = 2, Login = "manager", Password = "manager", RoleId = managerRole.Id, PhoneNumber = 888888888, City = "Warsaw", StreetName = "Gdanska", HouseNumber = "1" };

            modelBuilder.Entity<User>().HasData(new User[] { adminUser, managerUser });
        }
    }
}
