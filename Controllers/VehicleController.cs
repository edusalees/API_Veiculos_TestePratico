using API_Veiculos.DB_Connection;
using API_Veiculos.Domain.Entity;
using API_Veiculos.Domain.Validation;
using API_Veiculos.Infra;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System.Text;
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
        public IActionResult RegisterVehicle(Vehicle vehicle)
        {
            VehicleValidator validator = new VehicleValidator();
            ValidationResult validationResult = validator.Validate(vehicle);

            if (validationResult.IsValid)
            {
                try
                {
                    new DbExecution().RegisterVehicle(vehicle);

                    return Ok(new
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = String.Format("Veículo Registrado com sucesso, Identificador: {0}", vehicle.Id)
                    });
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
        }

        [HttpPost]
        [Route("UpdateRecordVehicle/")]
        public IActionResult UpdateRecordVehicle(Vehicle vehicle)
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

                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Dados Atualizados com Sucesso",
                    Vehicle = vehicle
                });
            }
            else
            {
                return BadRequest(new
                {
                    Error = validationResult.Errors
                });
            }
        }

        [HttpPost]
        [Route("DeleteRecordVehicle/")]
        public IActionResult DeleteRecordVehicle(long idVeiculo)
        {
            try
            {
                new DbExecution().DeleteRecordVehicle(idVeiculo);                
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }

            return Ok(new
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Registro deletado com sucesso"
            });
        }

        [HttpGet]
        [Route("GetAllVehicles/")]
        public IActionResult GetAllVehicles()
        {
            try
            {
                List<Vehicle> vehicles = new DbExecution().GetAllVehicles();

                if (!vehicles.Any())
                {
                    return Ok(new
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Não há registros na Base de Dados"
                    });
                }

                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Vehicles = vehicles
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetVehicleById/")]
        public IActionResult GetVehicleById(long idVeiculo)
        {
            try
            {
                Vehicle vehicle = new DbExecution().GetVehicleById(idVeiculo);

                if(vehicle == null)
                {
                    return Ok(new
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Não há registro com este Identificador"
                    });
                }

                return Ok(new 
                {
                    StatusCode = StatusCodes.Status200OK,
                    Vehicles = vehicle
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
