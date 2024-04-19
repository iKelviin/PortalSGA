CREATE PROCEDURE usp_ObterCargosPorDepartamento
(
	@IdDepartamento INT
)
AS
BEGIN	
	SELECT 
		Id,
		Nome,
		IdDepartamento
	FROM 
		tbl_Cargos
	WHERE 
		IdDepartamento = @IdDepartamento	
END
