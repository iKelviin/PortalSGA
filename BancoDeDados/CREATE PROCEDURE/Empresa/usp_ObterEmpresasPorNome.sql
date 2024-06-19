CREATE OR ALTER PROCEDURE usp_ObterEmpresasPorNome
(
	@Nome varchar(100)
)
AS 
BEGIN
	SELECT 
		Id,
		Nome,
		Email,
		Telefone,
		Cep,
		Endereco,
		Numero,
		Complemento,
		Bairro,
		Cidade,
		Estado,
		Ativo
	FROM 
		tbl_Empresas
	WHERE 
		Nome LIKE '%' + @Nome + '%'
END