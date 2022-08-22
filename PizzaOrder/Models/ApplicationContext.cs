using Microsoft.EntityFrameworkCore;

namespace PizzaOrder.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pizza>? Pizzas { get; set; }
        public DbSet<PizzaComponent>? PizzaComponents { get; set; }

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
        }
    }
}
