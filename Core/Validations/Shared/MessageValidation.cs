namespace Core.Validations;

public class MessageValidation
{
    #region Services

    public const string INFORMACAO_JA_EXISTE = "Essa informação já existe!";

    #endregion

    #region FluentValidation

    public const string CAMPO_OBRIGATORIO = "O campo {PropertyName} é obrigatório";
    public const string TAMANHO_CAMPO = "O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";
    public const string COMPARAR_CAMPO = "O campo {PropertyName} precisa ser maior que {ComparisonValue}";

    #endregion

}
