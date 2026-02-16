USE veiculos_db
GO
CREATE TABLE Veiculo
(
	IdVeiculo BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	DescVeiculo VARCHAR(255),
	MarcaVeiculo TINYINT NOT NULL,
	ModeloVeiculo VARCHAR(50) NOT NULL,
	OpicionaisVeiculo VARCHAR,
	ValorVeiculo FLOAT
);

use veiculos_db
go
CREATE PROCEDURE InsertVeiculo
@paramDescVeiculo VARCHAR(255),
@paramMarcaVeiculo TINYINT,
@paramModeloVeiculo VARCHAR(255),
@paramOpcionaisVeiculo VARCHAR(255),
@paramValorVeiculo FLOAT
AS
BEGIN
INSERT INTO veiculos_db..Veiculos
VALUES(@paramDescVeiculo, @paramMarcaVeiculo, @paramModeloVeiculo, @paramOpcionaisVeiculo, @paramValorVeiculo, 0, getdate(), null)
SELECT SCOPE_IDENTITY()
END

use veiculos_db
go
CREATE PROCEDURE ExcluirRegistroVeiculo
@paramIdVeiculo BIGINT
AS
BEGIN
UPDATE Veiculos
SET RegistroAtivo = 0, DataInativacao = getdate()
end