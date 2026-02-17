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
ALTER PROCEDURE InsertVeiculo
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

----------------------------------=================================================

use veiculos_db
go
ALTER PROCEDURE ExcluirRegistroVeiculo
@paramIdVeiculo BIGINT
AS
BEGIN
UPDATE Veiculos
SET RegistroAtivo = 0, DataInativacao = getdate()
WHERE IdVeiculo = @paramIdVeiculo
end
-----------------------------------------------------------------
use veiculos_db
go
ALTER PROCEDURE EditarRegistroVeiculo
@paramIdVeiculo BIGINT,
@paramDescVeiculo VARCHAR(255),
@paramMarcaVeiculo TINYINT,
@paramModeloVeiculo VARCHAR(255),
@paramOpcionaisVeiculo VARCHAR(255),
@paramValorVeiculo FLOAT
AS
BEGIN
UPDATE 
	Veiculos
SET
	DescVeiculo = CASE WHEN @paramDescVeiculo <> DescVeiculo THEN @paramDescVeiculo ELSE DescVeiculo END,
	MarcaVeiculo = CASE WHEN @paramMarcaVeiculo <> MarcaVeiculo THEN @paramMarcaVeiculo ELSE MarcaVeiculo END,
	ModeloVeiculo = CASE WHEN @paramModeloVeiculo <> ModeloVeiculo THEN @paramModeloVeiculo ELSE ModeloVeiculo END,
	OpicionaisVeiculo = CASE WHEN @paramOpcionaisVeiculo <> OpicionaisVeiculo THEN @paramOpcionaisVeiculo ELSE OpicionaisVeiculo END,
	ValorVeiculo = CASE WHEN @paramValorVeiculo <> ValorVeiculo THEN @paramValorVeiculo ELSE ValorVeiculo END
WHERE 
	IdVeiculo = @paramIdVeiculo
END

--===========================================================================

USE veiculos_db
GO
ALTER PROCEDURE ListarTodosRegistrosVeiculos
AS
BEGIN
SELECT
	[IdVeiculo]
   ,[DescVeiculo]
   ,[MarcaVeiculo]
   ,[ModeloVeiculo]
   ,[OpicionaisVeiculo]
   ,[ValorVeiculo]
   ,[DataRegistro]
FROM
	Veiculos V
WHERE
	V.RegistroAtivo = 1 AND
	V.DataInativacao IS NOT NUll
END

--==============================================================================]

USE veiculos_db
GO
CREATE PROCEDURE ListarRegistrosVeiculoPorId
@paramIdVeiculo BIGINT
AS
BEGIN
SELECT
	[IdVeiculo]
   ,[DescVeiculo]
   ,[MarcaVeiculo]
   ,[ModeloVeiculo]
   ,[OpicionaisVeiculo]
   ,[ValorVeiculo]
   ,[DataRegistro]
FROM
	Veiculos V
WHERE 
	V.IdVeiculo = @paramIdVeiculo AND
	V.RegistroAtivo = 1
END
