CREATE PROCEDURE usp_GravarDepartamento
(
	@Id INT = 0,
	@CentroCusto INT,
	@Nome varchar(50),
	@IdEmpresa INT
)
AS
BEGIN
	IF EXISTS( SELECT 1 FROM tbl_Departamento where Id = @Id)
	BEGIN
		UPDATE tbl_Departamento
		SET
			Nome = @Nome,
			CentroCusto = @CentroCusto,
			IdEmpresa = @IdEmpresa
		WHERE Id = @Id
	END
	ELSE
	BEGIN
		INSERT INTO tbl_Departamento (CentroCusto,Nome,IdEmpresa) VALUES (@CentroCusto,@Nome,@IdEmpresa)
		SELECT @@IDENTITY
	END
END