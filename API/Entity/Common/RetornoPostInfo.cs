namespace Entity.Common;

/// <summary>
/// Classe responsavel por armazenar dados da retorno de uma requisicao POST.
/// </summary>
public class RetornoPostInfo
{
    /// <summary>
    /// Mensagem do retorno.
    /// </summary>
    public string Mensagem { get; set; }
    /// <summary>
    /// Dados complementares do retorno (ex. ID gravado)
    /// </summary>
    public string Dados { get; set; }
}
