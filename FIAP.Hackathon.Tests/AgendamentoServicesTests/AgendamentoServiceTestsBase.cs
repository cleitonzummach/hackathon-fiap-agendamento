using FIAP.Hackathon.Application.Interfaces;
using FIAP.Hackathon.Application.Requests;
using FIAP.Hackathon.Application.Responses;
using FIAP.Hackathon.Application.Services;
using FIAP.Hackathon.Domain.Entities;
using FIAP.Hackathon.Domain.Repositories;
using Moq;

namespace FIAP.Hackathon.Tests.AgendamentoServicesTests
{
    public class AgendamentoServiceTestsBase
    {
        protected readonly Mock<IAgendamentoRepository> _mockAgendamentoRepository;
        protected readonly Mock<IMedicoAgendaRepository> _mockMedicoAgendaRepository;
        protected readonly Mock<IHttpMedicoService> _mockHttpMedicoService;
        protected readonly Mock<INotificacaoService> _mockNotificacaoService;
        protected readonly AgendamentoService _agendamentoService;

        public AgendamentoServiceTestsBase()
        {
            _mockAgendamentoRepository = new Mock<IAgendamentoRepository>();
            _mockMedicoAgendaRepository = new Mock<IMedicoAgendaRepository>();
            _mockHttpMedicoService = new Mock<IHttpMedicoService>();
            _mockNotificacaoService = new Mock<INotificacaoService>();

            _agendamentoService = new AgendamentoService(_mockAgendamentoRepository.Object, _mockMedicoAgendaRepository.Object, _mockHttpMedicoService.Object, _mockNotificacaoService.Object);
        }

        protected AgendarRequest GerarAgendarRequestValido()
        {
            return new AgendarRequest
            {
                PacienteId = Guid.NewGuid(),
                MedicoAgendaId = Guid.NewGuid()
            };
        }

        protected Agendamento GerarAgendamento(Guid pacienteId, Guid medicoAgendaId)
        {
            return new Agendamento(pacienteId, medicoAgendaId);
        }

        protected MedicoAgenda GerarMedicoAgenda()
        {
            return new MedicoAgenda(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1));
        }

        protected MedicoResponse GerarMedicoResponse(Guid medicoId)
        {
            return new MedicoResponse() { Nome = "Dr. Teste", CRM = new Random().Next(1000, 99999).ToString(), Especialidade = "Nome da Especialidade", Endereco = "Endereço" };
        }

        protected IEnumerable<Agendamento> GerarListaDeAgendamentos(Guid pacienteId, List<MedicoAgenda> medicoAgendas)
        {
            return medicoAgendas.Select(ma => new Agendamento(pacienteId, ma.MedicoAgendaId) { MedicoAgenda = ma });
        }
    }
}
