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
        public void ConexaoDBAISIM()
        {
            var connectionString = "Server=localhost;Database=veiculos_db;Trusted_Connection=True;TrustServerCertificate=true";

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                String sqlScript = "USE Veiculos_db SELECT * FROM Veiculos";
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlScript, connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    Veiculo veiculo = new Veiculo()
                    {
                        Id = reader.GetInt64(0),
                        DescVeiculo = reader.GetString(1),
                        MarcaVeiculo = (MarcaVeiculo)reader.GetByte(2),
                        ModeloVeiculo = reader.GetString(3),
                        OpcionaisVeiculo = reader.GetString(4),
                        ValorVeiculo = reader.GetDecimal(5),
                    };
                }

                connection.Close();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nDone. Press Enter.");
            Console.Read();
        }

        public void CadastrarVeiculoDB(Veiculo veiculo)
        {
            var connectionString = "Server=localhost;Database=veiculos_db;Trusted_Connection=True;TrustServerCertificate=true";

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

        public void ExcluirRegistroVeiculo(long idVeiculo)
        {
            var connectionString = "Server=localhost;Database=veiculos_db;Trusted_Connection=True;TrustServerCertificate=true";

            SqlConnection connection = new SqlConnection(connectionString);
            String sqlScript = "ExcluirRegistroVeiculo";
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlScript, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@paramIdVeiculo", idVeiculo);

            sqlCommand.ExecuteScalar();

        }

        public void EditarRegistroVeiculo(Veiculo veiculo)
        {
            var connectionString = "Server=localhost;Database=veiculos_db;Trusted_Connection=True;TrustServerCertificate=true";

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

        public List<Veiculo> ListarVeiculos()
        {
            List<Veiculo> veiculos = new List<Veiculo>();

            var connectionString = "Server=localhost;Database=veiculos_db;Trusted_Connection=True;TrustServerCertificate=true";

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
                veiculos.Add(veiculo);
            }

            return veiculos;
        }

        public Veiculo ListarVeiculoPorId(long idVeiculo)
        {
            Veiculo veiculo = new Veiculo();

            var connectionString = "Server=localhost;Database=veiculos_db;Trusted_Connection=True;TrustServerCertificate=true";

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

            return veiculo;
        }
    }
}
