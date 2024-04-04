using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Logs
{
    public class LogSolicitacaoInfo
    {
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public string Usuario { get; set; }
        public int Status { get; set; }
        public int StatusEmail { get; set; }
        public int SolicitadaoID { get; set; }
        public string Descricao { get; set; }
    }
}
