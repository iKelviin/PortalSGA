CREATE PROCEDURE usp_ObterCargosPorNome
(
	@Nome varchar(100)
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
		Nome LIKE '%' + @Nome + '%'			
END
