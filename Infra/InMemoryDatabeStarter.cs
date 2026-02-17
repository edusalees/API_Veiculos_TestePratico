using API_Veiculos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Veiculos.Infra
{
    public static class InMemoryDatabeStarter
    {
        public static void Starter(IServiceCollection services)
        {
            services.AddDbContext<VeiculoContext>(options => options.UseInMemoryDatabase(databaseName: "Veiculos"));

            services.AddTransient<IDataSeed, DataSeedService>();
        }
    }
}
