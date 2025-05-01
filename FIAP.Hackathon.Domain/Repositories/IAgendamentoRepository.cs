using FIAP.Hackathon.Domain.Entities;

namespace FIAP.Hackathon.Domain.Repositories
{
    public interface IAgendamentoRepository
    {
        Agendamento? RetornarPorId(Guid agendamentoId);
        IEnumerable<Agendamento> RetornarPorPacienteId(Guid pacienteId);
        bool Criar(Agendamento agendamento);
        bool Editar();
    }
}
