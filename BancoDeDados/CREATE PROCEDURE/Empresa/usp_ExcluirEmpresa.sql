CREATE PROCEDURE [dbo].[usp_ExcluirEmpresa]
@Id INT
AS
BEGIN
	DELETE FROM tbl_Empresa WHERE Id = @Id
END