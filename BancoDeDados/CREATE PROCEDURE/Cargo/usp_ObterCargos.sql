--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE usp_ObterCargos 
(
	@Id INT = 0
)
AS
BEGIN
	IF(@Id = 0)
	BEGIN
		SELECT 
			tbl_Cargos.Id,
			tbl_Cargos.Nome,
			IdDepartamento,
			tbl_Empresa.Id as IdEmpresa,
			tbl_Departamento.Nome as NomeDepartamento,
			tbl_Empresa.Nome as NomeEmpresa
		FROM 
			tbl_Cargos
			INNER JOIN tbl_Departamento on tbl_Departamento.Id = tbl_Cargos.IdDepartamento
			INNER JOIN tbl_Empresa on tbl_Empresa.Id = tbl_Departamento.IdEmpresa
	END
	ELSE
	BEGIN
		SELECT 
			tbl_Cargos.Id,
			tbl_Cargos.Nome,
			IdDepartamento,
			tbl_Empresa.Id as IdEmpresa,
			tbl_Departamento.Nome as NomeDepartamento,
			tbl_Empresa.Nome as NomeEmpresa
		FROM 
			tbl_Cargos
			INNER JOIN tbl_Departamento on tbl_Departamento.Id = tbl_Cargos.IdDepartamento
			INNER JOIN tbl_Empresa on tbl_Empresa.Id = tbl_Departamento.IdEmpresa
		WHERE 
			tbl_Cargos.Id = @Id
	END
END

