using FIAP.Hackathon.Application.Responses;

namespace FIAP.Hackathon.Application.Interfaces
{
    public interface IHttpMedicoService
    {
        Task<MedicoResponse?> ConsultarDadosMedico(Guid medicoId);
        Task<bool> MedicoValido(Guid medicoId);
    }
}
