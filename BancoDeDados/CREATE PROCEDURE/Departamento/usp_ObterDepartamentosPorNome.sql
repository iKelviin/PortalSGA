CREATE PROCEDURE usp_ObterDepartamentosPorNome
(
	@Nome varchar(100)
)
AS
BEGIN	
	SELECT 
		Id,
		CentroCusto,
		Nome,
		IdEmpresa
	FROM 
		tbl_Departamento 
	WHERE 
		Nome LIKE '%' + @Nome + '%'		
END