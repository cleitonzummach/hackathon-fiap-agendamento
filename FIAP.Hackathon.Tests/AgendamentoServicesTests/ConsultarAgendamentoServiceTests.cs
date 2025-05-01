using FIAP.Hackathon.Domain.Entities;
using Moq;

namespace FIAP.Hackathon.Tests.AgendamentoServicesTests
{
    public class ConsultarAgendamentoServiceTests : AgendamentoServiceTestsBase
    {
        private readonly Guid _pacienteId = Guid.NewGuid();

        [Fact]
        public async Task ConsultarAgendamento_ExistemAgendamentos_RetornaLista()
        {
            // Arrange
            var medicoAgenda1 = GerarMedicoAgenda();
            var medicoAgenda2 = GerarMedicoAgenda();
            var agendamentos = GerarListaDeAgendamentos(_pacienteId, new List<MedicoAgenda> { medicoAgenda1, medicoAgenda2 }).ToList();
            var medicoResponse1 = GerarMedicoResponse(medicoAgenda1.MedicoId);
            var medicoResponse2 = GerarMedicoResponse(medicoAgenda2.MedicoId);

            _mockAgendamentoRepository.Setup(repo => repo.RetornarPorPacienteId(_pacienteId)).Returns(agendamentos);
            _mockHttpMedicoService.Setup(service => service.ConsultarDadosMedico(medicoAgenda1.MedicoId)).ReturnsAsync(medicoResponse1);
            _mockHttpMedicoService.Setup(service => service.ConsultarDadosMedico(medicoAgenda2.MedicoId)).ReturnsAsync(medicoResponse2);

            // Act
            var response = await _agendamentoService.ConsultarAgendamento(_pacienteId);

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task ConsultarAgendamento_NaoExistemAgendamentos_RetornaNull()
        {
            // Arrange
            _mockAgendamentoRepository.Setup(repo => repo.RetornarPorPacienteId(_pacienteId)).Returns((IEnumerable<Agendamento>)null);

            // Act
            var response = await _agendamentoService.ConsultarAgendamento(_pacienteId);

            // Assert
            Assert.NotNull(response);
            Assert.Empty(response);
        }
    }
}
