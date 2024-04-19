CREATE PROCEDURE usp_ObterCargos 
(
	@Id INT = 0
)
AS
BEGIN
	IF(@Id = 0)
	BEGIN
		SELECT 
			Id,
			Nome,
			IdDepartamento	
		FROM 
			tbl_Cargos 		
	END
	ELSE
	BEGIN
		SELECT 
			Id,
			Nome,
			IdDepartamento
		FROM 
			tbl_Cargos
		WHERE 
			Id = @Id
	END
END
