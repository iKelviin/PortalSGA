using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class AcessoInfo
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public int Codigo { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Empresa { get; set; }
        public string Departamento { get; set; }
        public string Cargo { get; set; }
        public string Superior { get; set; }
        public bool Ativo { get; set; }

    }
}
