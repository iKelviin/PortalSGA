CREATE PROCEDURE usp_ObterEmpresasPorNome
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
		tbl_Empresa 
	WHERE 
		Nome LIKE '%' + @Nome + '%'
END