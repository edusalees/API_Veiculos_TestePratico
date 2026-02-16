using API_Veiculos.DB_Connection;
using API_Veiculos.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API_Veiculos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly ILogger<VeiculosController> _logger;

        public VeiculosController(ILogger<VeiculosController> logger)
        {
            _logger = logger;
        }

        
        [HttpPost]
        [Route("CadastrarVeiculo/")]
        public async Task<IActionResult> CadastrarVeiculo(Veiculo veiculo)
        {
            try
            {
                new ConexaoDB().CadastrarVeiculoDB(veiculo);
            }
            catch (Exception ex) 
            {
                BadRequest(ex);
            }

            return NoContent();
        }

        [HttpPost]
        [Route("EditarRegistroVeiculo/")]
        public async Task<IActionResult> EditarRegistroVeiculo(Veiculo veiculo)
        {
            try
            {
                new ConexaoDB().EditarRegistroVeiculo(veiculo);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }

            return NoContent();
        }

        [HttpPost]
        [Route("ExcluirRegistroVeiculo/")]
        public async Task<IActionResult> ExcluirRegistroVeiculo(long idVeiculo)
        {
            try
            {
                new ConexaoDB().ExcluirRegistroVeiculo(idVeiculo);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }

            return NoContent();
        }

        [HttpGet]
        [Route("ListarTodosRegistrosVeiculos/")]
        public void ListarTodosRegistrosVeiculos()
        {
            try
            {
                List<Veiculo> veiculos = new ConexaoDB().ListarVeiculos();
                
                if (veiculos.Any())
                {
                    Ok(veiculos);
                }
            }
            catch(Exception ex)
            {
                BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ListarVeiculoPorId/")]
        public void ListarVeiculoPorId(long idVeiculo)
        {
            try
            {
                Veiculo veiculos = new ConexaoDB().ListarVeiculoPorId(idVeiculo);

                if (veiculos != null)
                {
                    Ok(veiculos);
                }
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
        }






    }
}
