CREATE OR ALTER VIEW View_Solicitacao
AS
	SELECT 
		SOL.ID AS SolicitacaoID,
		SOL.Codigo,
		COL.NomeCompleto,
		COL.IdEmpresa,
		EMP.Nome AS Empresa,
		COL.IdCargo,
		CARG.Nome AS Cargo,
		COL.Superior,
		COL.TipoContrato,
		DEP.Nome AS Departamento,
		DEP.Id AS IdDepartamento,
		DEP.CentroCusto,
		SOL.DataInicio,
		SOL.Status,
		SOL.StatusEmail,
		SOL.AcessoEmail,
		SOL.AcessoInternet,
		SOL.DataSolicitacao,
		SOL.Solicitante,
		SOL.DataCriacao,
		SOL.Criador,
		SOL.Validador,
		SOL.DataValidacao
	FROM 
		tbl_Solicitacoes SOL
	INNER JOIN tbl_Colaboradores COL ON COL.Codigo = SOL.Codigo
	INNER JOIN tbl_Departamentos DEP oN DEP.CentroCusto = COL.CentroCusto
	INNER JOIN tbl_Empresas EMP on EMP.Id = COL.IdEmpresa
	INNER JOIN tbl_Cargos CARG on CARG.Id = COL.IdCargo

