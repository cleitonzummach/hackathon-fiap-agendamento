using FIAP.Hackathon.Application.Interfaces;
using FIAP.Hackathon.Domain.Entities;
using FIAP.Hackathon.Domain.Enums;
using FIAP.Hackathon.Domain.Repositories;

namespace FIAP.Hackathon.Application.Services
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly INotificacaoRepository _notificacaoRepository;

        public NotificacaoService(INotificacaoRepository notificacaoRepository) 
        {
            _notificacaoRepository = notificacaoRepository;
        }

        public bool Registrar(Guid agendamentoId, NotificacaoEnum.TipoNotificacao tipo, NotificacaoEnum.Status situacao) 
        {
            try
            {
                Notificacao notificacao = new Notificacao(
                    agendamentoId,
                    tipo,
                    situacao);

                return _notificacaoRepository.Registrar(notificacao);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Cancelar(Guid agendamentoId)
        {
            try
            {
                var notificacoes = _notificacaoRepository.RetornarPorAgendamentoId(agendamentoId);

                if (notificacoes != null)
                {
                    foreach (var notificacao in notificacoes)
                    {
                        _notificacaoRepository.Cancelar(notificacao);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
