CREATE OR ALTER PROCEDURE usp_ObterCargosPorDepartamento
(
	@IdDepartamento INT
)
AS
BEGIN	
	SELECT 
			tbl_Cargos.Id,
			tbl_Cargos.Nome,
			IdDepartamento,
			tbl_Empresas.Id as IdEmpresa,
			tbl_Departamentos.Nome as NomeDepartamento,
			tbl_Empresas.Nome as NomeEmpresa
		FROM 
			tbl_Cargos
			INNER JOIN tbl_Departamentos on tbl_Departamentos.Id = tbl_Cargos.IdDepartamento
			INNER JOIN tbl_Empresas on tbl_Empresas.Id = tbl_Departamentos.IdEmpresa
	WHERE 
		IdDepartamento = @IdDepartamento	
END
