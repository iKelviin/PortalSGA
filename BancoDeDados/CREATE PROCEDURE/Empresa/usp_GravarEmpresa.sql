CREATE OR ALTER PROCEDURE usp_GravarEmpresa
(
	@Id INT = 0,
	@Nome varchar(100),
	@Email VARCHAR(100),
	@Telefone VARCHAR(20),
	@Cep VARCHAR(20),
	@Endereco VARCHAR(100),
	@Numero INT,
	@Complemento VARCHAR(50),
	@Bairro VARCHAR(50),
	@Cidade VARCHAR(50),
	@Estado VARCHAR(20),
	@Ativo BIT
)
AS
BEGIN
	IF EXISTS( SELECT 1 FROM tbl_Empresas where Id = @Id)
	BEGIN
		UPDATE tbl_Empresas
		SET			
			Nome = @Nome,
			Email = @Email,
			Telefone = @Telefone,
			Cep = @Cep,
			Endereco = @Endereco,
			Numero = @Numero,
			Complemento = @Complemento,
			Bairro = @Bairro,
			Cidade = @Cidade,
			Estado = @Estado,
			Ativo = @Ativo
		WHERE Id = @Id

		SELECT @Id
	END
	ELSE
	BEGIN
		INSERT INTO tbl_Empresas 
		(
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
		) 
		VALUES 
		(
			@Nome,
			@Email,
			@Telefone,
			@Cep,
			@Endereco,
			@Numero,
			@Complemento,
			@Bairro,
			@Cidade,
			@Estado,
			@Ativo
		)
		SELECT @@IDENTITY
	END
END


