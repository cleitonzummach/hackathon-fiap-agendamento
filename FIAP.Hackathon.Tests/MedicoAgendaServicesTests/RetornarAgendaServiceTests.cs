using FIAP.Hackathon.Application.Responses;
using FIAP.Hackathon.Domain.Entities;
using FIAP.Hackathon.Domain.Enums;
using Moq;

namespace FIAP.Hackathon.Tests.MedicoAgendaServicesTests
{
    public class RetornarAgendaServiceTests : MedicoAgendaServiceTestsBase
    {
        private readonly Guid _medicoId = Guid.NewGuid();

        [Fact]
        public async Task RetornarAgenda_ExistemAgendasParaMedico_RetornaLista()
        {
            // Arrange
            var pacienteId1 = Guid.NewGuid();
            var pacienteId2 = Guid.NewGuid();
            var medicoAgenda1 = GerarMedicoAgenda(Guid.NewGuid(), _medicoId, Guid.NewGuid(), MedicoAgendaEnum.Status.Agendado);
            var medicoAgenda2 = GerarMedicoAgenda(Guid.NewGuid(), _medicoId, Guid.NewGuid(), MedicoAgendaEnum.Status.Disponivel);
            var medicoAgendas = new List<MedicoAgenda> { medicoAgenda1, medicoAgenda2 };
            var pacienteResponse1 = GerarPacienteResponse(pacienteId1);

            _mockMedicoAgendaRepository.Setup(repo => repo.RetornarAgenda(_medicoId)).Returns(medicoAgendas);
            _mockHttpPacienteService.Setup(service => service.ConsultarDadosPaciente(pacienteId1)).ReturnsAsync(pacienteResponse1);

            // Act
            var response = await _medicoAgendaService.RetornarAgenda(_medicoId);

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task RetornarAgenda_NaoExistemAgendasParaMedico_RetornaNull()
        {
            // Arrange
            _mockMedicoAgendaRepository.Setup(repo => repo.RetornarAgenda(_medicoId)).Returns(new List<MedicoAgenda>());
            _mockHttpPacienteService.Setup(service => service.ConsultarDadosPaciente(It.IsAny<Guid>())).ReturnsAsync((PacienteResponse)null);

            // Act
            var response = await _medicoAgendaService.RetornarAgenda(_medicoId);

            // Assert
            Assert.NotNull(response);
            Assert.Empty(response);
        }
    }
}
