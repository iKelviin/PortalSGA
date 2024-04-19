CREATE PROCEDURE usp_ObterEmpresas
(
	@Id INT = 0
)
AS 
BEGIN
	IF (@Id = 0)
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
		END
	ELSE
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
				Id = @Id
		END
END