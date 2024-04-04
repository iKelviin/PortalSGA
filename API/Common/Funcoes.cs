using System.Configuration;
using System.Text.RegularExpressions;
using System.Xml;
using static Common.Enumeracoes;

namespace Common
{
    public static class Funcoes
    {
        public static string MontaConexao(Banco nomeBanco)
        {
            string providerString = string.Empty;

            //Busca a conexao criptografada no arquivo de configuracao de acordo com o banco informado
            switch (nomeBanco)
            {
                case Banco.SGA:
                    providerString = ConfigurationManager.ConnectionStrings["ConnectionSGA"].ConnectionString;
                    break;
                case Banco.Apdata:
                    providerString = ConfigurationManager.ConnectionStrings["ConnectionApdata"].ConnectionString;
                    break;
                case Banco.iPlan:
                    providerString = ConfigurationManager.ConnectionStrings["ConnectioniPlan"].ConnectionString;
                    break;
                case Banco.Natura:
                    providerString = ConfigurationManager.ConnectionStrings["ConnectionNatura"].ConnectionString;
                    break;
                case Banco.JobTrack:
                    providerString = ConfigurationManager.ConnectionStrings["ConnectionJobTrack"].ConnectionString;
                    break;
                case Banco.iQuote:
                    providerString = ConfigurationManager.ConnectionStrings["ConnectioniQuote"].ConnectionString;
                    break;
                case Banco.MCI:
                    providerString = ConfigurationManager.ConnectionStrings["ConnectionMCI"].ConnectionString;
                    break;
                case Banco.WipTracker:
                    providerString = ConfigurationManager.ConnectionStrings["ConnectionWipTracker"].ConnectionString;
                    break;
                case Banco.TOTVS:
                    providerString = ConfigurationManager.ConnectionStrings["ConnectionTOTVS"].ConnectionString;
                    break;
                default:
                    break;
            }


            //providerString = Plural.Seguranca.Criptografia.Descriptografar(providerString, "!P@L@U#R$A%L&0*");

            return providerString;
        }
        public static string RetornarValorXML(string caminho, string nodeAPesquisar)
        {
            XmlDocument xd = new XmlDocument();
            string valor = "";

            xd.Load(caminho);

            XmlNodeList nodeList = xd.SelectNodes(nodeAPesquisar);

            foreach (XmlNode node in nodeList)
            {
                foreach (XmlNode oNodeChild in node.ChildNodes)
                {
                    if (oNodeChild.NodeType == XmlNodeType.Comment)
                    {
                        continue;
                    }
                    else
                    {
                        valor = oNodeChild.Value.ToString();
                    }
                }
            }

            return valor;
        }

        public static int geraPontosSenha(string senha)
        {
            if (senha == null) return 0;
            int pontosPorTamanho = GetPontoPorTamanho(senha);
            int pontosPorMinusculas = GetPontoPorMinusculas(senha);
            int pontosPorMaiusculas = GetPontoPorMaiusculas(senha);
            int pontosPorDigitos = GetPontoPorDigitos(senha);
            int pontosPorSimbolos = GetPontoPorSimbolos(senha);
            int pontosPorRepeticao = GetPontoPorRepeticao(senha);
            return pontosPorTamanho + pontosPorMinusculas + pontosPorMaiusculas + pontosPorDigitos + pontosPorSimbolos - pontosPorRepeticao;
        }

        private static int GetPontoPorTamanho(string senha)
        {
            return Math.Min(10, senha.Length) * 6;
        }

        private static int GetPontoPorMinusculas(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[a-z]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private static int GetPontoPorMaiusculas(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[A-Z]", "").Length;
            return Math.Min(2, rawplacar) * 10;
        }

        private static int GetPontoPorDigitos(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[0-9]", "").Length;
            return Math.Min(2, rawplacar) * 10;
        }

        private static int GetPontoPorSimbolos(string senha)
        {
            int rawplacar = Regex.Replace(senha, "[a-zA-Z0-9]", "").Length;
            return Math.Min(2, rawplacar) * 10;
        }

        private static int GetPontoPorRepeticao(string senha)
        {
            Regex regex = new Regex(@"(\w)*.*\1");
            bool repete = regex.IsMatch(senha);
            if (repete)
            {
                return 10;
            }
            else
            {
                return 0;
            }
        }

    }
}
