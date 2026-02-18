using API_Veiculos.Domain.Entity;
using API_Veiculos.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Configuration;
using API_Veiculos.Infra;

namespace API_Veiculos.DB_Connection
{
    public class DbExecution
    {
        SettingsManager settingsManager = new SettingsManager();       

        public void RegisterVehicle(Vehicle veiculo)
        {
            if (!settingsManager.GetUseDatabaseConfig())
            {
                using (var context = new VehicleContext())
                {
                    context.Add(veiculo);

                    context.SaveChanges();
                }
            }
            else
            {
                string connectionString = settingsManager.GetConnectionStringConfig();

                try
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    string sqlScript = "InsertVeiculo";
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(sqlScript, connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@paramDescVeiculo", veiculo.DescVeiculo);
                    sqlCommand.Parameters.AddWithValue("@paramMarcaVeiculo", veiculo.MarcaVeiculo);
                    sqlCommand.Parameters.AddWithValue("@paramModeloVeiculo", veiculo.ModeloVeiculo);
                    sqlCommand.Parameters.AddWithValue("@paramOpcionaisVeiculo", veiculo.OpcionaisVeiculo);
                    sqlCommand.Parameters.AddWithValue("@paramValorVeiculo", veiculo.ValorVeiculo);

                    veiculo.Id = Convert.ToInt64(sqlCommand.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void DeleteRecordVehicle(long idVeiculo)
        {
            if (!settingsManager.GetUseDatabaseConfig())
            {
                using( var context = new VehicleContext())
                {
                    var veiculo = context.Veiculos.Single(v => v.Id == idVeiculo);

                    context.Veiculos.Remove(veiculo);
                    context.SaveChanges();
                }
            }
            else
            {
                string connectionString = settingsManager.GetConnectionStringConfig();

                SqlConnection connection = new SqlConnection(connectionString);
                String sqlScript = "ExcluirRegistroVeiculo";
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlScript, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@paramIdVeiculo", idVeiculo);

                sqlCommand.ExecuteScalar();
            }            
        }

        public void UpdateRecordVehicle(Vehicle veiculo)
        {
            try
            {
                if (!settingsManager.GetUseDatabaseConfig())
                {
                    using (var context = new VehicleContext())
                    {
                        var veiculoBusca = context.Veiculos.Single(v => v.Id == veiculo.Id);

                        veiculoBusca.Id = veiculo.Id;
                        veiculoBusca.DescVeiculo = veiculo.DescVeiculo;
                        veiculoBusca.MarcaVeiculo = veiculo.MarcaVeiculo;
                        veiculoBusca.ModeloVeiculo = veiculo.ModeloVeiculo;
                        veiculoBusca.OpcionaisVeiculo = veiculo.OpcionaisVeiculo;
                        veiculoBusca.ValorVeiculo = veiculo.ValorVeiculo;
                        veiculoBusca.DataRegistro = veiculo.DataRegistro;
                                                               
                        context.SaveChanges();
                    }
                }
                else
                {
                    string connectionString = settingsManager.GetConnectionStringConfig();

                    SqlConnection connection = new SqlConnection(connectionString);
                    String sqlScript = "EditarRegistroVeiculo";
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(sqlScript, connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@paramIdVeiculo", veiculo.Id);
                    sqlCommand.Parameters.AddWithValue("@paramDescVeiculo", veiculo.DescVeiculo == null ? DBNull.Value : veiculo.DescVeiculo);
                    sqlCommand.Parameters.AddWithValue("@paramMarcaVeiculo", veiculo.MarcaVeiculo == null ? DBNull.Value : veiculo.MarcaVeiculo);
                    sqlCommand.Parameters.AddWithValue("@paramModeloVeiculo", veiculo.ModeloVeiculo == null ? DBNull.Value : veiculo.ModeloVeiculo);
                    sqlCommand.Parameters.AddWithValue("@paramOpcionaisVeiculo", veiculo.OpcionaisVeiculo == null ? DBNull.Value : veiculo.OpcionaisVeiculo);
                    sqlCommand.Parameters.AddWithValue("@paramValorVeiculo", veiculo.ValorVeiculo == null ? DBNull.Value : veiculo.ValorVeiculo);

                    sqlCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> listaVeiculos = new List<Vehicle>();

            if (!settingsManager.GetUseDatabaseConfig())
            {
                using (var context = new VehicleContext())
                {
                    var veiculos = context.Veiculos.ToList();

                    foreach(var veiculo in veiculos)
                    {
                        listaVeiculos.Add(veiculo);
                    }
                }
            }
            else
            {
                string connectionString = settingsManager.GetConnectionStringConfig();

                SqlConnection connection = new SqlConnection(connectionString);
                String sqlScript = "ListarTodosRegistrosVeiculos";
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlScript, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Vehicle veiculo = new Vehicle()
                    {
                        Id = reader.GetInt64(0),
                        DescVeiculo = reader.GetString(1),
                        MarcaVeiculo = (MarcaVeiculo)reader.GetByte(2),
                        ModeloVeiculo = reader.GetString(3),
                        OpcionaisVeiculo = reader.GetString(4),
                        ValorVeiculo = reader.GetString(5),
                        DataRegistro = reader.GetDateTime(6)
                    };
                    listaVeiculos.Add(veiculo);
                }
            }            

            return listaVeiculos;
        }

        public Vehicle GetVehicleById(long idVeiculo)
        {
            Vehicle veiculo = new Vehicle();

            if (!settingsManager.GetUseDatabaseConfig())
            {
                using (var context = new VehicleContext())
                {
                    Vehicle veiculoBusca = context.Veiculos.Single(v => v.Id == idVeiculo);

                    if (veiculoBusca != null)
                    {
                        return veiculoBusca;
                    }
                }
            }
            else
            {              
                string connectionString = settingsManager.GetConnectionStringConfig();

                SqlConnection connection = new SqlConnection(connectionString);
                String sqlScript = "ListarRegistrosVeiculoPorId";
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlScript, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@paramIdVeiculo", idVeiculo);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    veiculo = new Vehicle()
                    {
                        Id = reader.GetInt64(0),
                        DescVeiculo = reader.GetString(1),
                        MarcaVeiculo = (MarcaVeiculo)reader.GetByte(2),
                        ModeloVeiculo = reader.GetString(3),
                        OpcionaisVeiculo = reader.GetString(4),
                        ValorVeiculo = reader.GetString(5),
                        DataRegistro = reader.GetDateTime(6)
                    };
                }
            }            

            return veiculo;
        }
    }
}
