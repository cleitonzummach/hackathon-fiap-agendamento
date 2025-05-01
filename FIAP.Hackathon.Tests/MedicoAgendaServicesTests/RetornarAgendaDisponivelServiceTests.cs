using FIAP.Hackathon.Domain.Entities;
using Moq;

namespace FIAP.Hackathon.Tests.MedicoAgendaServicesTests
{
    public class RetornarAgendaDisponivelServiceTests : MedicoAgendaServiceTestsBase
    {
        private readonly Guid _especialidadeId = Guid.NewGuid();

        [Fact]
        public async Task RetornarAgendaDisponivel_ExistemAgendasDisponiveis_RetornaLista()
        {
            // Arrange
            var medicoId1 = Guid.NewGuid();
            var medicoId2 = Guid.NewGuid();
            var medicoAgenda1 = GerarMedicoAgenda(Guid.NewGuid(), medicoId1, _especialidadeId);
            var medicoAgenda2 = GerarMedicoAgenda(Guid.NewGuid(), medicoId2, _especialidadeId);
            var medicoAgendas = new List<MedicoAgenda> { medicoAgenda1, medicoAgenda2 };
            var medicoResponse1 = GerarMedicoResponse(medicoId1);
            var medicoResponse2 = GerarMedicoResponse(medicoId2);

            _mockMedicoAgendaRepository.Setup(repo => repo.RetornarAgendaDisponivel(_especialidadeId)).Returns(medicoAgendas);
            _mockHttpMedicoService.Setup(service => service.ConsultarDadosMedico(medicoId1)).ReturnsAsync(medicoResponse1);
            _mockHttpMedicoService.Setup(service => service.ConsultarDadosMedico(medicoId2)).ReturnsAsync(medicoResponse2);

            // Act
            var response = await _medicoAgendaService.RetornarAgendaDisponivel(_especialidadeId);

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task RetornarAgendaDisponivel_NaoExistemAgendasDisponiveis_RetornaNull()
        {
            // Arrange
            _mockMedicoAgendaRepository.Setup(repo => repo.RetornarAgendaDisponivel(_especialidadeId)).Returns(new List<MedicoAgenda>());

            // Act
            var response = await _medicoAgendaService.RetornarAgendaDisponivel(_especialidadeId);

            // Assert
            Assert.NotNull(response);
            Assert.Empty(response);
        }
    }
}
