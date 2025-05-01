using FIAP.Hackathon.Domain.Entities;

namespace FIAP.Hackathon.Domain.Repositories
{
    public interface IMedicoAgendaRepository
    {
        bool Criar(MedicoAgenda medicoAgenda);
        bool Editar();
        IEnumerable<MedicoAgenda>? RetornarAgendaDisponivel(Guid? especialidadeId);
        IEnumerable<MedicoAgenda>? RetornarAgenda(Guid medicoId);
        MedicoAgenda? RetornarPorId(Guid medicoAgendaId);
    }
}
