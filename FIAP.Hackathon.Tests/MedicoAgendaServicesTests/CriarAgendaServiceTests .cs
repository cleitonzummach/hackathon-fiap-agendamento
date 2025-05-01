using FIAP.Hackathon.Domain.Entities;
using Moq;

namespace FIAP.Hackathon.Tests.MedicoAgendaServicesTests
{
    public class CriarAgendaServiceTests : MedicoAgendaServiceTestsBase
    {
        [Fact]
        public void CriarAgenda_CriadoComSucesso_RetornaTrue()
        {
            // Arrange
            var request = GerarCriarAgendaRequestValido();
            _mockMedicoAgendaRepository.Setup(repo => repo.Criar(It.IsAny<MedicoAgenda>())).Returns(true);

            // Act
            var resultado = _medicoAgendaService.CriarAgenda(request);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void CriarAgenda_ErroAoCriar_RetornaFalse()
        {
            // Arrange
            var request = GerarCriarAgendaRequestValido();
            _mockMedicoAgendaRepository.Setup(repo => repo.Criar(It.IsAny<MedicoAgenda>())).Returns(false);

            // Act
            var resultado = _medicoAgendaService.CriarAgenda(request);

            // Assert
            Assert.False(resultado);
        }
    }
}
