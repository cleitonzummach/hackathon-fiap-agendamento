using FIAP.Hackathon.Domain.Enums;

namespace FIAP.Hackathon.Domain.Entities
{
    public class MedicoAgenda
    {
        public Guid MedicoAgendaId { get; set; }
        public Guid MedicoId { get; set; }
        public Guid EspecialidadeId { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public MedicoAgendaEnum.Status Situacao { get; set; }
        public DateTime? DataExclusao { get; set; }

        public ICollection<Agendamento>? Agendamentos { get; set; }

        public MedicoAgenda(Guid medicoId, Guid especialidadeId, DateTime dataHoraInicio, DateTime dataHoraFim)
        {
            MedicoId = medicoId;
            EspecialidadeId = especialidadeId;
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            Situacao = MedicoAgendaEnum.Status.Disponivel;
        }

        public void AlterarSituacao(MedicoAgendaEnum.Status situacao) 
        {
            Situacao = situacao;
        }

        public void Excluir()
        {
            Situacao = MedicoAgendaEnum.Status.Excluido;
            DataExclusao = DateTime.UtcNow;
        }
    }
}
