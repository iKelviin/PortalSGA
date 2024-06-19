CREATE TABLE [dbo].[tbl_Solicitacoes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [int] NOT NULL,
	[AcessoEmail] [varchar](20) NOT NULL,
	[AcessoInternet] [bit] NOT NULL,
	[DataInicio] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[StatusEmail] [int] NULL,
	[Solicitante] [varchar](100) NOT NULL,
	[DataSolicitacao] [datetime] NOT NULL,
	[DataCriacao] [datetime] NULL,
	[Criador] [varchar](100) NULL,
	[DataValidacao] [datetime] NULL,
	[Validador] [varchar](100) NULL
)

