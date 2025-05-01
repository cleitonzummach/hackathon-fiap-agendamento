using FIAP.Hackathon.Domain.Enums;

namespace FIAP.Hackathon.Domain.Entities
{
    public class Notificacao
    {
        public Guid NotificacaoId { get; set; }
        public Guid AgendamentoId { get; set; }
        public NotificacaoEnum.TipoNotificacao TipoNotificacao { get; set; }
        public NotificacaoEnum.Status Situacao { get; set; }
        public DateTime? DataExclusao { get; set; }

        public Notificacao(Guid agendamentoId, NotificacaoEnum.TipoNotificacao tipoNotificacao, NotificacaoEnum.Status situacao)
        {
            AgendamentoId = agendamentoId;
            TipoNotificacao = tipoNotificacao;
            Situacao = situacao;
        }
    }
}
