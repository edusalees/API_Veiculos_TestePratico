using API_Veiculos.Domain.Enum;

namespace API_Veiculos.Domain.Entity
{
    public class Vehicle
    {
        public long Id { get; set; }
        public string DescVeiculo { get; set; }
        public MarcaVeiculo MarcaVeiculo { get; set; }
        public string ModeloVeiculo { get; set; }
        public string OpcionaisVeiculo { get; set; }
        public string ValorVeiculo { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
