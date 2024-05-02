using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CargoInfo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdDepartamento { get; set; }
        public string? NomeDepartamento { get; set; }
        public int IdEmpresa { get; set; }
        public string? NomeEmpresa { get; set; }
    }
}
