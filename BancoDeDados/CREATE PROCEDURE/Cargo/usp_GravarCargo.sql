CREATE PROCEDURE usp_GravarCargo
(
	@Id INT = 0,
	@Nome varchar(50),
	@IdDepartamento INT
)
AS
BEGIN
	IF EXISTS( SELECT 1 FROM tbl_Cargos where Id = @Id)
	BEGIN
		UPDATE tbl_Cargos
		SET
			Nome = @Nome,
			IdDepartamento = @IdDepartamento
		WHERE Id = @Id
	END
	ELSE
	BEGIN
		INSERT INTO tbl_Cargos (Nome,IdDepartamento) VALUES (@Nome,@IdDepartamento)
		SELECT @@IDENTITY
	END
END