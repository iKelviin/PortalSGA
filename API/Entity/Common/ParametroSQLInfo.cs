namespace Entity.Common;

/// <summary>
/// Classe que armazena os dados de parâmetros de uma procedure SQL específica (usp_ObterParametrosProcedure).
/// </summary>
public class ParametroSQLInfo
{
    /// <summary>
    /// Nome do parâmetro.
    /// </summary>
    public string Nome { get; set; }

    /// <summary>
    /// Tipo do parâmetro.
    /// </summary>
    public string Tipo { get; set; }

    /// <summary>
    /// Tamanho do parâmetro.
    /// </summary>
    public int Tamanho { get; set; }

    /// <summary>
    /// Precisão do parâmetro.
    /// </summary>
    public int Precisao { get; set; }

    /// <summary>
    /// Escala do parâmetro.
    /// </summary>
    public int Escala { get; set; }

    /// <summary>
    /// Valor indicando se o parâmetro aceita valores nulos (true) ou não (false).
    /// </summary>
    public bool AceitaNulo { get; set; }

    /// <summary>
    /// Valor indicando se o parâmetro é de saída (true) ou não (false).
    /// </summary>
    public bool ParametroDeSaida { get; set; }
}
