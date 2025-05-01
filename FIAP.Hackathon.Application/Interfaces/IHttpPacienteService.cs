using FIAP.Hackathon.Application.Responses;

namespace FIAP.Hackathon.Application.Interfaces
{
    public interface IHttpPacienteService
    {
        Task<PacienteResponse?> ConsultarDadosPaciente(Guid pacienteId);
        Task<bool> PacienteValido(Guid pacienteId);
    }
}
