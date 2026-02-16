using Microsoft.Extensions.Options;

namespace API_Veiculos.Infra
{
    public class Configuracoes
    {      
        private readonly ParametrosConfiguracao configuracao;
        public Configuracoes(IOptions<ParametrosConfiguracao> opcoes)
        {
            configuracao = opcoes.Value;
        }

        public string RetornarConnectionStrings()
        {
            string connectionString = configuracao.ConnectionStrings;

            return connectionString;
        }
    }
}
