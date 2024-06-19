CREATE OR ALTER PROCEDURE usp_InserirSistemaColaborador
	@IdSistema INT,
	@Codigo INT
AS
BEGIN
	INSERT INTO tbl_AcessoSistemas (IdSistema,Codigo) VALUES (@IdSistema, @Codigo)

	SELECT @@IDENTITY
END


