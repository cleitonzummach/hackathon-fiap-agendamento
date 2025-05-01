using FIAP.Hackathon.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.Hackathon.Application.Interfaces
{
    public interface INotificacaoService
    {
        bool Registrar(Guid agendamentoId, NotificacaoEnum.TipoNotificacao tipo, NotificacaoEnum.Status situacao);
        bool Cancelar(Guid agendamentoId);
    }
}
