using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Usuario
{
    public class UsuarioADInfo
    {
        public string PrimeiroNome { get; set; }
        public string Usuario { get; set; }
        public string UltimoNome { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Superior { get; set; }
        public string Departamento { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }
    }
}
