CREATE TABLE [dbo].[tbl_Colaboradores](
	[Codigo] [int] NOT NULL,
	[NomeCompleto] [varchar](100) NOT NULL,
	[IdEmpresa] [int] NOT NULL,
	[IdCargo] [int] NOT NULL,
	[Superior] [varchar](100) NOT NULL,
	[TipoContrato] [varchar](20) NOT NULL,
	[CentroCusto] [int] NOT NULL
)


