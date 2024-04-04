using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ColaboradorInfo
    {
        public int Codigo { get; set; }
        public string NomeCompleto { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public string Superior { get; set; }
        public string TipoContrato { get; set; }
        public int CentroCusto { get; set; }
        public List<SistemaInfo> Sistemas { get; set; }

    }
}
