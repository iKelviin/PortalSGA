using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Usuario
{
    public class UsuarioInfo
    {

        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Acesso { get; set; }
        public bool Ativo { get; set; }

        public List<int> CodTela { get; set; }
    }
}
