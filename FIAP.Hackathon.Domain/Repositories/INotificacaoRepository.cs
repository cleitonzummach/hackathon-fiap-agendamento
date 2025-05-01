using FIAP.Hackathon.Domain.Entities;

namespace FIAP.Hackathon.Domain.Repositories
{
    public interface INotificacaoRepository
    {
        bool Registrar(Notificacao notificacao);
        bool Cancelar(Notificacao notificacao);
        List<Notificacao>? RetornarPorAgendamentoId(Guid agendamentoId);
    }
}
