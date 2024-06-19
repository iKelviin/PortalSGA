---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE usp_InserirSolicitacao
(
	@Codigo INT,
	@DataInicio datetime = null,
	@Status INT = null,
	@StatusEmail INT = null,
	@AcessoInternet BIT = 0,
	@AcessoEmail VARCHAR(20) = null,
	@Solicitante VARCHAR(100),
	@Validador VARCHAR(100) = null,
	@DataValidacao datetime = null,
	@Criador VARCHAR(100) = null,
	@DataCriacao datetime = null
)
AS
BEGIN
	IF @DataValidacao = '0001-01-01'
	    SET @DataValidacao = NULL;
	
	IF @DataCriacao = '0001-01-01'
	    SET @DataCriacao = NULL;	
	
	IF EXISTS (SELECT 1 FROM tbl_Solicitacoes where Codigo = @Codigo)
		BEGIN
			UPDATE
				tbl_Solicitacoes
			SET
				DataInicio = ISNULL(@DataInicio,DataInicio),
				Status = ISNULL(@Status,Status),
				StatusEmail = ISNULL(@StatusEmail,StatusEmail),
				AcessoInternet = ISNULL(@AcessoInternet,AcessoInternet),
				AcessoEmail = ISNULL(@AcessoEmail,AcessoEmail),
				Solicitante = @Solicitante,
				Validador = ISNULL(@Validador,Validador),
				DataValidacao = ISNULL(@DataValidacao,DataValidacao),
				Criador = ISNULL(@Criador,Criador),
				DataCriacao = ISNULL(@DataCriacao,DataCriacao)

			WHERE
				Codigo = @Codigo
		END

	ELSE
		BEGIN
			INSERT INTO
				tbl_Solicitacoes
			(
				Codigo,
				DataInicio,
				Status,
				Solicitante,
				DataSolicitacao,
				AcessoInternet,
				AcessoEmail
			)
			VALUES
			(
				@Codigo,
				@DataInicio,
				@Status,
				@Solicitante,
				GETDATE(),
				@AcessoInternet,
				@AcessoEmail				
			)
			Select @@IDENTITY
		END
END
