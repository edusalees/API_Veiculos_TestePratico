using API_Veiculos.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace API_Veiculos.Infra
{
    public class VehicleContext : DbContext
    {
        public DbSet<Vehicle> Veiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Veiculos");
        }
    }
}
