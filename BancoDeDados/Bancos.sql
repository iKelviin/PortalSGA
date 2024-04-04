CREATE DATABASE Apdata
CREATE DATABASE iPlan
CREATE DATABASE Natura
CREATE DATABASE JobTrack
CREATE DATABASE iQuote
CREATE DATABASE MCI
CREATE DATABASE WipTracker
CREATE DATABASE TOTVS



Create TABLE tbl_Usuario(
	Id int identity primary key,
	Usuario varchar(100) not null,
	Senha varchar(100) not null,
	Nome varchar(100) not null,
	Email varchar(100) not null,
	Departamento varchar(100) not null,
	Cargo varchar(100) not null,
	Superior varchar(100) not null,
	Ativo BIT not null
)


CREATE PROCEDURE [dbo].[usp_ObterParametrosProcedure] --'usp_ValidarUsuario'
(
	@NomeProcedure VARCHAR(100)
)
AS
BEGIN
	SELECT
		sys.parameters.name AS Nome,
		sys.types.name AS Tipo,
		sys.parameters.max_length AS Tamanho,
		sys.parameters.precision AS Precisao,
		sys.parameters.scale AS Escala,
		sys.parameters.is_nullable AS AceitaNulo,
		sys.parameters.is_output AS ParametroDeSaida
	FROM
		sys.objects WITH (NOLOCK)
	INNER JOIN
		sys.parameters WITH (NOLOCK)
	ON
		sys.parameters.object_id = sys.objects.object_id
	INNER JOIN
		sys.types  WITH (NOLOCK)
	ON
		sys.types.system_type_id = sys.parameters.system_type_id
	WHERE
		sys.objects.name = @NomeProcedure
END



CREATE PROCEDURE usp_InserirAcesso --'Rodrigues.Silva','Rodrigues Silva',null,1,null,'Atendimento ao Cliente','Assistente De Atendimento Ao Cliente','Paulo Souza'
(
	@Usuario VARCHAR(50),
	@Nome VARCHAR(100),
	@Email VARCHAR(100) = null,
	@Ativo INT,
	@Senha VARCHAR(50) = null,
	@Departamento VARCHAR(100),
	@Cargo VARCHAR(100),
	@Superior VARCHAR(100)
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM tbl_Usuario where Usuario = @Usuario)
		BEGIN
			IF(@Senha is null or @Senha = '')
				BEGIN
					PRINT 'UPDATE SENHA NULL'
					UPDATE
						tbl_Usuario
					SET
						Nome = @Nome,
						Email = ISNULL(@Email,''),
						Ativo = @Ativo,
						Departamento = @Departamento,
						Cargo = @Cargo,
						Superior = @Superior
					WHERE
						Usuario = @Usuario
					Select ID from tbl_Usuario where Usuario = @Usuario
				END
			ELSE
				BEGIN
					PRINT 'UPDATE SENHA NAO NULL'
					UPDATE
						tbl_Usuario
					SET
						Nome = @Nome,
						Email = ISNULL(@Email,''),
						Ativo = @Ativo,
						Departamento = @Departamento,
						Cargo = @Cargo,
						Superior = @Superior,
						Senha = @Senha
					WHERE
						Usuario = @Usuario
					Select ID from tbl_Usuario where Usuario = @Usuario
				END
		END

	ELSE
		BEGIN
			PRINT 'INSERINDO'
			INSERT INTO
				tbl_Usuario
			(
				Usuario,
				Nome,
				Email,
				Ativo,
				Departamento,
				Cargo,
				Superior,
				Senha
			)
			VALUES
			(
				@Usuario,
				@Nome,
				ISNULL(@Email,''),
				@Ativo,
				@Departamento,
				@Cargo,
				@Superior,
				@Senha			
			)
			Select ID from tbl_Usuario where Usuario = @Usuario
		END
END



CREATE PROCEDURE usp_ExcluirAcesso
(
	@Usuario VARCHAR(50)
)
AS
BEGIN
	UPDATE tbl_Usuario SET Ativo = 0 WHERE Usuario = @Usuario
END

