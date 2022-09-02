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

            // Adding components

            PizzaComponent tomatoPaste = new PizzaComponent { Id = 1, Name = "Tomato paste" };
            PizzaComponent cheese = new PizzaComponent { Id = 2, Name = "Cheese" };
            PizzaComponent mozzarella = new PizzaComponent { Id = 3, Name = "Mozzarella" };
            PizzaComponent egg = new PizzaComponent { Id = 4, Name = "Egg" };
            PizzaComponent mushroom = new PizzaComponent { Id = 5, Name = "Mushroom" };
            PizzaComponent pineapple = new PizzaComponent { Id = 6, Name = "Pineapple" };
            PizzaComponent olives = new PizzaComponent { Id = 7, Name = "Olives" };
            PizzaComponent tomato = new PizzaComponent { Id = 8, Name = "Tomato" };
            PizzaComponent pepper = new PizzaComponent { Id = 9, Name = "Pepper" };
            PizzaComponent onion = new PizzaComponent { Id = 10, Name = "Onion" };
            PizzaComponent corn = new PizzaComponent { Id = 11, Name = "Corn" };
            PizzaComponent pickles = new PizzaComponent { Id = 12, Name = "Pickles" };
            PizzaComponent greens = new PizzaComponent { Id = 13, Name = "Greens" };
            PizzaComponent salami = new PizzaComponent { Id = 14, Name = "Salami" };
            PizzaComponent ham = new PizzaComponent { Id = 15, Name = "Ham" };
            PizzaComponent becon = new PizzaComponent { Id = 16, Name = "Becon" };
            PizzaComponent chicken = new PizzaComponent { Id = 17, Name = "Chicken" };
            PizzaComponent tuna = new PizzaComponent { Id = 18, Name = "Tuna" };
            PizzaComponent shrimps = new PizzaComponent { Id = 19, Name = "Shrimps" };

            modelBuilder.Entity<PizzaComponent>().HasData(new PizzaComponent[] 
            {
                tomatoPaste, cheese, mozzarella, egg, mushroom, pineapple, olives, tomato, pepper, onion, corn, pickles, greens, salami, ham, becon, chicken, tuna, shrimps
            });

            // Adding pizzas
            Pizza carbonara = new Pizza 
            { 
                Id = 1, Name = "Carbonara", PhotoName = "Carbonara", PhotoPath = "/images/pizzas/Carbonara.png", Price = 30  
            };

            Pizza hawaiian = new Pizza 
            { 
                Id = 2, Name = "Hawaiian", PhotoName = "Hawaiian", PhotoPath = "/images/pizzas/Hawaiian.png", Price = 25
            };

            Pizza margerita = new Pizza
            {
                Id = 3, Name = "Margerita", PhotoName = "Margerita", PhotoPath = "/images/pizzas/Margerita.png", Price = 20
            };

            Pizza pepperoni = new Pizza
            {
                Id = 4, Name = "Pepperoni", PhotoName = "Pepperoni", PhotoPath = "/images/pizzas/Pepperoni.png", Price = 30
            };

            modelBuilder.Entity<Pizza>().HasData(new Pizza[] 
            { 
                carbonara, 
                hawaiian,
                margerita,
                pepperoni
            });
        }
    }
}
