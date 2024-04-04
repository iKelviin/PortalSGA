using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Solicitacao
{
    public class SolicitacaoCompletaInfo
    {
        public int SolicitacaoID { get; set; }
        public int Codigo { get; set; }
        public string AcessoEmail { get; set; }
        public string AcessoInternet { get; set; }
        public DateTime DataInicio { get; set; }
        public int Status { get; set; }
        public int StatusEmail { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public string Solicitante { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Criador { get; set; }
        public DateTime DataValidacao { get; set; }
        public string Validador { get; set; }


        //COLABORADOR
        public string NomeCompleto { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public string Superior { get; set; }
        public string TipoContrato { get; set; }
        public int CentroCusto { get; set; }
        public List<SistemaInfo> Sistemas { get; set; }

        //DEPARTAMENTO
        public string Departamento { get; set; }
    }
}
