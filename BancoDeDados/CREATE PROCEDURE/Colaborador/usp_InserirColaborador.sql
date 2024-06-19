CREATE OR ALTER PROCEDURE usp_InserirColaborador
(
	@Codigo INT,
	@NomeCompleto VARCHAR(100),
	@IdEmpresa INT,
	@IdCargo INT,
	@Superior VARCHAR(100),
	@TipoContrato VARCHAR(20),
	@CentroCusto INT
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM tbl_Colaboradores where Codigo = @Codigo)
		BEGIN
			UPDATE
				tbl_Colaboradores
			SET
				NomeCompleto = @NomeCompleto,
				IdEmpresa = @IdEmpresa,
				IdCargo = @IdCargo,
				Superior = @Superior,
				TipoContrato = @TipoContrato,
				CentroCusto = @CentroCusto
			WHERE
				Codigo = @Codigo

		END

	ELSE
		BEGIN
			INSERT INTO
				tbl_Colaboradores
			(
				Codigo,
				NomeCompleto,
				IdEmpresa,
				IdCargo,
				Superior,
				TipoContrato,
				CentroCusto
			)
			VALUES
			(
				@Codigo,
				@NomeCompleto,
				@IdEmpresa,
				@IdCargo,
				@Superior,
				@TipoContrato,
				@CentroCusto				
			)
			Select @Codigo
		END
END
