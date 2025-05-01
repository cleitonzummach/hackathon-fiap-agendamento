using FIAP.Hackathon.Domain.Entities;

namespace FIAP.Hackathon.Application.Responses
{
    public class ConsultarAgendamentoResponse
    {
        public Guid AgendamentoId { get; set; }
        public string Medico { get; set; }
        public string Especialidade { get; set; }
        public string DataHoraInicio { get; set; }
        public string DataHoraFim { get; set; }

        public ConsultarAgendamentoResponse(Agendamento agendamento, MedicoResponse? medico)
        {
            AgendamentoId = agendamento.AgendamentoId;
            Medico = medico.Nome ?? "";
            Especialidade = medico.Especialidade ?? "";
            DataHoraInicio = agendamento.MedicoAgenda.DataHoraInicio.ToString("dd/MM/yyyy HH:mm");
            DataHoraFim = agendamento.MedicoAgenda.DataHoraFim.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
