namespace Core.Models;

public class Notificacao
{
    public Notificacao(string mensagem)
    {
        Mensagem = mensagem;
    }

    public string Mensagem { get; }
}
