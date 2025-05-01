using FIAP.Hackathon.Domain.Enums;

namespace FIAP.Hackathon.Domain.Entities
{
    public class Agendamento
    {
        public Guid AgendamentoId { get; set; }
        public Guid PacienteId { get; set; }
        public Guid MedicoAgendaId { get; set; }
        public AgendamentoEnum.Status Situacao { get; set; }
        public DateTime? DataCancelamento { get; set; }

        public MedicoAgenda? MedicoAgenda { get; set; }

        public Agendamento(Guid pacienteId, Guid medicoAgendaId)
        {
            PacienteId = pacienteId;
            MedicoAgendaId = medicoAgendaId;
            Situacao = AgendamentoEnum.Status.Agendado;
        }

        public void CancelarAgendamento() 
        {
            Situacao = AgendamentoEnum.Status.Cancelado;
            DataCancelamento = DateTime.UtcNow;
        }
    }
}
