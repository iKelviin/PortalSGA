CREATE OR ALTER PROCEDURE [dbo].[usp_ExcluirEmpresa]
@Id INT
AS
BEGIN
	DELETE FROM tbl_Empresas WHERE Id = @Id
END