CREATE OR ALTER PROCEDURE usp_ObterDepartamentosPorEmpresa
(
	@IdEmpresa INT
)
AS
BEGIN	
	SELECT 
		tbl_Departamentos.Id,
		tbl_Departamentos.CentroCusto,
		tbl_Departamentos.Nome,
		tbl_Departamentos.IdEmpresa,
		tbl_Empresas.Nome as NomeEmpresa
	FROM 
		tbl_Departamentos 
		INNER JOIN tbl_Empresas on tbl_Empresas.Id = tbl_Departamentos.IdEmpresa 
	WHERE
		tbl_Departamentos.IdEmpresa = @IdEmpresa		
END