CREATE PROCEDURE usp_ExcluirDepartamento
(
	@Id INT
)
AS
BEGIN
	DELETE FROM tbl_Departamento WHERE Id = @Id
END