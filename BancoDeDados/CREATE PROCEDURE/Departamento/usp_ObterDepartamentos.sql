CREATE PROCEDURE usp_ObterDepartamentos
(
	@Id INT = 0	
)
AS
BEGIN	
	IF(@Id = 0)
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
		END
	ELSE 
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
				tbl_Departamento.Id = @Id
		END
END