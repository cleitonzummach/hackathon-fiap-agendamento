using FIAP.Hackathon.Domain.Entities;
using FIAP.Hackathon.Domain.Enums;

namespace FIAP.Hackathon.Application.Responses
{
    public class MedicoAgendaResponse
    {
        public string DataHoraInicio { get; set; }
        public string DataHoraFim { get; set; }
        public string? Paciente { get; set; }
        public string? EmailPaciente { get; set; }
        public string Situacao { get; set; }

        public MedicoAgendaResponse(MedicoAgenda medicoAgenda, PacienteResponse? paciente) 
        {
            DataHoraInicio = medicoAgenda.DataHoraInicio.ToString("dd/MM/yyyy HH:mm");
            DataHoraFim = medicoAgenda.DataHoraFim.ToString("dd/MM/yyyy HH:mm");
            Paciente = paciente != null ? paciente.Nome : "";
            EmailPaciente = paciente != null ? paciente.Email : "";
            Situacao = MedicoAgendaEnum.RetornarDescricao(medicoAgenda.Situacao);
        }
    }
}
