using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Enumeracoes
    {
        public enum Banco
        {
            SGA = 1,
            Apdata = 2,
            iPlan = 3,
            Natura = 4,
            JobTrack = 5,
            iQuote = 6,
            MCI = 7,
            WipTracker = 8,
            TOTVS = 9
        }
        public enum TipoContrato
        {
            Profissional = 1,
            Terceiro = 2
        }
        public enum StatusSolicitacao
        {
            Solicitado = 1,
            Validado = 2,
            Criado = 3,
            Cancelada = 4,
            Bloqueado = 5
        }

        public enum StatusEmail
        {
            NãoEnviado = 0,
            SolicitacaoEnviada = 1,
            ValidacaoEnviada = 2,
            CriacaoEnviada = 3,
            BloqueioEnviado = 4,
            DesbloqueioEnviado = 5,
            Falha = 6
        }
        public enum StatusUsuarioAD
        {
            Inativo = 0,
            Ativo = 1
        }
        public enum AcessoEmail
        {
            Interno = 0,
            Externo = 1
        }
        public enum Sistemas
        {
            Apdata = 1,
            iPlan = 2,
            Natura = 3,
            JobTrack = 4,
            iQuote = 5,
            MCI = 6,
            WipTracker = 7,
            TOTVS = 8
        }
        public enum NivelAcesso
        {
            Solicitante = 1,
            Validador = 2,
            Criador = 3,
            Administrador = 4
        }
        public enum Tela
        {
            GerarSolicitacao = 1,
            ValidarSolicitacao = 2,
            CriarAcesso = 3,
            BloquearAcesso = 4,
            AcompanharSolicitacao = 5,
            AlterarAcesso = 6,
            Cadastros = 7,
            GestaoUsuarios = 8,
            Relatorios = 9
        }

    }
}
