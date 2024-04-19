CREATE PROCEDURE usp_ObterDepartamentos
(
	@Id INT = 0	
)
AS
BEGIN	
	IF(@Id = 0)
		BEGIN
			SELECT 
				Id,
				CentroCusto,
				Nome,
				IdEmpresa
			FROM 
				tbl_Departamento 
		END
	ELSE 
		BEGIN
			SELECT 
				Id,
				CentroCusto,
				Nome,
				IdEmpresa
			FROM 
				tbl_Departamento 
			WHERE
				Id = @Id
		END
END