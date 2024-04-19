CREATE PROCEDURE usp_ObterDepartamentosPorEmpresa
(
	@IdEmpresa INT
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
		IdEmpresa = @IdEmpresa		
END