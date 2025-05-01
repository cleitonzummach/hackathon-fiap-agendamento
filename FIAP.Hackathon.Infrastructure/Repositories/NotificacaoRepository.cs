using FIAP.Hackathon.Domain.Entities;
using FIAP.Hackathon.Domain.Repositories;
using FIAP.Hackathon.Infrastructure.Data.Context;

namespace FIAP.Hackathon.Infrastructure.Repositories
{
    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly HackathonDBContext _context;
        
        public NotificacaoRepository(HackathonDBContext context) 
        {
            _context = context;
        }

        public bool Registrar(Notificacao notificacao)
        {
            try
            {
                _context.Notificacao.Add(notificacao);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Cancelar(Notificacao notificacao)
        {
            try
            {
                notificacao.Situacao = Domain.Enums.NotificacaoEnum.Status.Cancelado;
                notificacao.DataExclusao = DateTime.UtcNow;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Notificacao>? RetornarPorAgendamentoId(Guid agendamentoId)
        {
            return _context.Notificacao
                .Where(x => x.AgendamentoId == agendamentoId)
                .ToList();
        }
    }
}
