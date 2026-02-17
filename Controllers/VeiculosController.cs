using API_Veiculos.DB_Connection;
using API_Veiculos.Domain.Entity;
using API_Veiculos.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace API_Veiculos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VeiculosController : ControllerBase
    {

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
        public string ListarTodosRegistrosVeiculos()
        {
            try
            {
                List<Veiculo> veiculos = new ConexaoDB().ListarVeiculos();

                var json = JsonSerializer.Serialize(veiculos);

                return json.ToString();
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("ListarVeiculoPorId/")]
        public Veiculo ListarVeiculoPorId(long idVeiculo)
        {
            try
            {
                Veiculo veiculo = new ConexaoDB().ListarVeiculoPorId(idVeiculo);

                return veiculo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     
    }
}
