using API_Veiculos.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace API_Veiculos.Infra
{
    public class VeiculoContext : DbContext
    {
        public DbSet<Veiculo> Veiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Veiculo>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Veiculos");
        }
    }
}
