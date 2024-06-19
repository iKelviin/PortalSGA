CREATE OR ALTER PROCEDURE usp_ObterDepartamentos
(
	@Id INT = 0	
)
AS
BEGIN	
	IF(@Id = 0)
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
		END
	ELSE 
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
				tbl_Departamentos.Id = @Id
		END
END