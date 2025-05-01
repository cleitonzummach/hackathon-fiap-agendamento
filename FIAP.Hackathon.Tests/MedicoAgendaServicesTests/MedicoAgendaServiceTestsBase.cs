using FIAP.Hackathon.Application.Interfaces;
using FIAP.Hackathon.Application.Requests;
using FIAP.Hackathon.Application.Responses;
using FIAP.Hackathon.Application.Services;
using FIAP.Hackathon.Domain.Entities;
using FIAP.Hackathon.Domain.Enums;
using FIAP.Hackathon.Domain.Repositories;
using Moq;

namespace FIAP.Hackathon.Tests.MedicoAgendaServicesTests
{
    public class MedicoAgendaServiceTestsBase
    {
        protected readonly Mock<IMedicoAgendaRepository> _mockMedicoAgendaRepository;
        protected readonly Mock<IHttpMedicoService> _mockHttpMedicoService;
        protected readonly Mock<IHttpPacienteService> _mockHttpPacienteService;
        protected readonly MedicoAgendaService _medicoAgendaService;

        public MedicoAgendaServiceTestsBase()
        {
            _mockMedicoAgendaRepository = new Mock<IMedicoAgendaRepository>();
            _mockHttpMedicoService = new Mock<IHttpMedicoService>();
            _mockHttpPacienteService = new Mock<IHttpPacienteService>();
            _medicoAgendaService = new MedicoAgendaService(
                _mockMedicoAgendaRepository.Object,
                _mockHttpMedicoService.Object,
                _mockHttpPacienteService.Object);
        }

        protected CriarAgendaRequest GerarCriarAgendaRequestValido()
        {
            return new CriarAgendaRequest
            {
                MedicoId = Guid.NewGuid(),
                EspecialidadeId = Guid.NewGuid(),
                DataHoraInicio = DateTime.UtcNow.AddHours(1),
                DataHoraFim = DateTime.UtcNow.AddHours(2)
            };
        }

        protected MedicoAgenda GerarMedicoAgenda(Guid medicoAgendaId, Guid medicoId, Guid especialidadeId, MedicoAgendaEnum.Status status = MedicoAgendaEnum.Status.Disponivel, List<Agendamento>? agendamentos = null)
        {
            return new MedicoAgenda(medicoId, especialidadeId, DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1));
        }

        protected MedicoResponse GerarMedicoResponse(Guid medicoId)
        {
            return new MedicoResponse() { Nome = "Dr. Teste" };
        }

        protected PacienteResponse GerarPacienteResponse(Guid pacienteId)
        {
            return new PacienteResponse() { Nome = "Paciente Teste" };
        }
    }
}
