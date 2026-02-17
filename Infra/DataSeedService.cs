using API_Veiculos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using API_Veiculos.Domain.Entity;
using API_Veiculos.Domain.Enum;

namespace API_Veiculos.Infra
{
    public class DataSeedService : IDataSeed
    {
        private readonly VeiculoContext _context;

        public DataSeedService(VeiculoContext context)
        {
            _context = context;
        }

        public async Task SeedDataAsync()
        {
            if(!await _context.Veiculos.AnyAsync())
            {
                _context.Veiculos.Add(
                    new Veiculo
                    {
                        DescVeiculo = "aogvh",
                        MarcaVeiculo = MarcaVeiculo.Honda,
                        ModeloVeiculo = "asdogh",
                        OpcionaisVeiculo = "sodngvo",
                        ValorVeiculo = 3.43M,
                        DataRegistro = DateTime.Now.Date
                    });
                await _context.SaveChangesAsync();
            }
        }
    }
}
