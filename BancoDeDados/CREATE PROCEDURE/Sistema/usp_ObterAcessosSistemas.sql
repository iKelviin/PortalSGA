CREATE OR ALTER PROCEDURE usp_ObterAcessosSistemas 
	@Codigo INT
AS
BEGIN
	SELECT 
		tbl_AcessoSistemas.Codigo,
		tbl_Sistemas.Nome,
		tbl_Sistemas.Id as IdSistema
	FROM 
		tbl_AcessoSistemas
	INNER JOIN 
		tbl_Sistemas ON tbl_Sistemas.Id = tbl_AcessoSistemas.IdSistema
	WHERE 
		Codigo = @Codigo
END

