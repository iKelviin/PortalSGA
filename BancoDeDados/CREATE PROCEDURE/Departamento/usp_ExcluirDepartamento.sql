CREATE OR ALTER PROCEDURE usp_ExcluirDepartamento
(
	@Id INT
)
AS
BEGIN
	DELETE FROM tbl_Departamentos WHERE Id = @Id
END