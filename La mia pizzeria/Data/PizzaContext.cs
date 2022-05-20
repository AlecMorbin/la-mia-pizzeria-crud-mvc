using Microsoft.EntityFrameworkCore;
using La_mia_pizzeria.Models;

namespace La_mia_pizzeria.Data
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Database=PizzaDB;Integrated Security=True");
        }
    }
}
