CREATE PROCEDURE usp_ExcluirCargo
(
	@Id INT
)
AS
BEGIN
	DELETE FROM tbl_Cargos WHERE Id = @Id
END
