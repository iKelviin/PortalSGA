CREATE OR ALTER PROCEDURE usp_GravarDepartamento
(
	@Id INT = 0,
	@CentroCusto INT,
	@Nome varchar(50),
	@IdEmpresa INT
)
AS
BEGIN
	IF EXISTS( SELECT 1 FROM tbl_Departamentos where Id = @Id)
	BEGIN
		UPDATE tbl_Departamentos
		SET
			Nome = @Nome,
			CentroCusto = @CentroCusto,
			IdEmpresa = @IdEmpresa
		WHERE Id = @Id
	END
	ELSE
	BEGIN
		INSERT INTO tbl_Departamentos (CentroCusto,Nome,IdEmpresa) VALUES (@CentroCusto,@Nome,@IdEmpresa)
		SELECT @@IDENTITY
	END
END