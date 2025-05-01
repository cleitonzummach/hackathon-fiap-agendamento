using FIAP.Hackathon.Application.Requests;
using FIAP.Hackathon.Application.Responses;
using FIAP.Hackathon.Domain.Entities;

namespace FIAP.Hackathon.Application.Interfaces
{
    public interface IAgendamentoService
    {
        bool Agendar(AgendarRequest request);
        Task<List<ConsultarAgendamentoResponse>> ConsultarAgendamento(Guid pacienteId);
        bool CancelarAgendamento(Guid agendamentoId);
    }
}
