using FIAP.Hackathon.Application.Requests;
using FIAP.Hackathon.Application.Responses;
using FIAP.Hackathon.Domain.Entities;

namespace FIAP.Hackathon.Application.Interfaces
{
    public interface IMedicoAgendaService
    {
        bool CriarAgenda(CriarAgendaRequest request);
        bool ExcluirAgenda(Guid medicoAgendaId);
        Task<List<AgendaResponse>?> RetornarAgendaDisponivel(Guid? especialidadeId);
        Task<List<MedicoAgendaResponse>?> RetornarAgenda(Guid medicoId);
    }
}
