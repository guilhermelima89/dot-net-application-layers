using Core.Models;

namespace Core.Interfaces;

public interface INotificadorService
{
    bool TemNotificacao();
    List<Notificacao> ObterNotificacoes();
    void Handle(Notificacao notificacao);
}
