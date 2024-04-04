using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Common
{
    public class FiltroInfo
    {
        public string Departamento { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }
        public int Status { get; set; }

        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public DateTime DataInicioSolicitacao { get; set; }
        public DateTime DataFimSolicitacao { get; set; }
    }
}
