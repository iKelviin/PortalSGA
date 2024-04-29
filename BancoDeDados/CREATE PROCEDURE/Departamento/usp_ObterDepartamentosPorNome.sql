CREATE PROCEDURE usp_ObterDepartamentosPorNome
(
	@Nome varchar(100)
)
AS
BEGIN	
	SELECT 
		tbl_Departamento.Id,
		tbl_Departamento.CentroCusto,
		tbl_Departamento.Nome,
		tbl_Departamento.IdEmpresa,
		tbl_Empresa.Nome as NomeEmpresa
	FROM 
		tbl_Departamento 
		INNER JOIN tbl_Empresa on tbl_Empresa.Id = tbl_Departamento.IdEmpresa
	WHERE 
		tbl_Departamento.Nome LIKE '%' + @Nome + '%'		
END