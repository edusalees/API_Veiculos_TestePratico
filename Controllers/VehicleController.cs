using API_Veiculos.DB_Connection;
using API_Veiculos.Domain.Entity;
using API_Veiculos.Domain.Validation;
using API_Veiculos.Infra;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace API_Veiculos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {

        [HttpPost]
        [Route("RegisterVehicle/")]
        public async Task<IActionResult> RegisterVehicle(Vehicle vehicle)
        {
            VehicleValidator validator = new VehicleValidator();
            ValidationResult validationResult = validator.Validate(vehicle);

            if (validationResult.IsValid)
            {
                try
                {
                    new DbExecution().RegisterVehicle(vehicle);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest(new
                {                    
                    Error = validationResult.Errors
                });
            }
            


            return Ok();
        }

        [HttpPost]
        [Route("UpdateRecordVehicle/")]
        public async Task<IActionResult> UpdateRecordVehicle(Vehicle vehicle)
        {
            VehicleValidator validator = new VehicleValidator();
            ValidationResult validationResult = validator.Validate(vehicle);
            if (validationResult.IsValid)
            {
                try
                {
                    new DbExecution().UpdateRecordVehicle(vehicle);
                }
                catch (Exception ex)
                {
                    BadRequest(ex);
                }
            }
            else
            {
                return BadRequest(new
                {
                    Error = validationResult.Errors
                });
            }

            return NoContent();
        }

        [HttpPost]
        [Route("DeleteRecordVehicle/")]
        public async Task<IActionResult> DeleteRecordVehicle(long idVeiculo)
        {
            try
            {
                new DbExecution().DeleteRecordVehicle(idVeiculo);
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
                List<Vehicle> vehicles = new DbExecution().ListarVeiculos();

                var json = JsonSerializer.Serialize(vehicles);

                return json.ToString();
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("ListarVeiculoPorId/")]
        public Vehicle ListarVeiculoPorId(long idVeiculo)
        {
            try
            {
                Vehicle vehicle = new DbExecution().ListarVeiculoPorId(idVeiculo);

                return vehicle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     
    }
}
