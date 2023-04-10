using System.ComponentModel;

namespace Core.Enumerators;

public enum EStatusProduto
{
    [Description("Promoção")]
    Promocao = 1,
    [Description("Vencido")]
    Vencido = 2
}
