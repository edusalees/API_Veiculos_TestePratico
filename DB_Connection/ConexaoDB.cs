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
    public class ConexaoDB
    {
        SettingsManager settingsManager = new SettingsManager();       

        public void CadastrarVeiculoDB(Veiculo veiculo)
        {
            if (!settingsManager.GetUseSqlServerConfig())
            {
                using (var context = new VeiculoContext())
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
                    String sqlScript = "InsertVeiculo";
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

        public void ExcluirRegistroVeiculo(long idVeiculo)
        {
            if (!settingsManager.GetUseSqlServerConfig())
            {
                using( var context = new VeiculoContext())
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

        public void EditarRegistroVeiculo(Veiculo veiculo)
        {
            try
            {
                if (!settingsManager.GetUseSqlServerConfig())
                {
                    using (var context = new VeiculoContext())
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

        public List<Veiculo> ListarVeiculos()
        {
            List<Veiculo> listaVeiculos = new List<Veiculo>();

            if (!settingsManager.GetUseSqlServerConfig())
            {
                using (var context = new VeiculoContext())
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
                    Veiculo veiculo = new Veiculo()
                    {
                        Id = reader.GetInt64(0),
                        DescVeiculo = reader.GetString(1),
                        MarcaVeiculo = (MarcaVeiculo)reader.GetByte(2),
                        ModeloVeiculo = reader.GetString(3),
                        OpcionaisVeiculo = reader.GetString(4),
                        ValorVeiculo = reader.GetDecimal(5),
                        DataRegistro = reader.GetDateTime(6)
                    };
                    listaVeiculos.Add(veiculo);
                }
            }            

            return listaVeiculos;
        }

        public Veiculo ListarVeiculoPorId(long idVeiculo)
        {
            Veiculo veiculo = new Veiculo();

            if (!settingsManager.GetUseSqlServerConfig())
            {
                using (var context = new VeiculoContext())
                {
                    Veiculo veiculoBusca = context.Veiculos.Single(v => v.Id == idVeiculo);

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
                    veiculo = new Veiculo()
                    {
                        Id = reader.GetInt64(0),
                        DescVeiculo = reader.GetString(1),
                        MarcaVeiculo = (MarcaVeiculo)reader.GetByte(2),
                        ModeloVeiculo = reader.GetString(3),
                        OpcionaisVeiculo = reader.GetString(4),
                        ValorVeiculo = reader.GetDecimal(5),
                        DataRegistro = reader.GetDateTime(6)
                    };
                }
            }            

            return veiculo;
        }
    }
}
