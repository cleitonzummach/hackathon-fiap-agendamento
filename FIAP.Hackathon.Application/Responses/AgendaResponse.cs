using FIAP.Hackathon.Domain.Entities;

namespace FIAP.Hackathon.Application.Responses
{
    public class AgendaResponse
    {
        public Guid MedicoAgendaId { get; set; }
        public string DataHoraInicio { get; set; }
        public string DataHoraFim { get; set; }
        public string Medico { get; set; }
        public string Especialidade { get; set; }

        public AgendaResponse(MedicoAgenda medicoAgenda, MedicoResponse? medico) 
        {
            MedicoAgendaId = medicoAgenda.MedicoAgendaId;
            DataHoraInicio = medicoAgenda.DataHoraInicio.ToString("dd/MM/yyyy HH:mm");
            DataHoraFim = medicoAgenda.DataHoraFim.ToString("dd/MM/yyyy HH:mm");
            Medico = medico != null ? medico.Nome : "";
            Especialidade = medico != null ? medico.Especialidade : "";
        }
    }
}
