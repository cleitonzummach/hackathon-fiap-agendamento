using FIAP.Hackathon.Domain.Entities;
using Moq;

namespace FIAP.Hackathon.Tests.MedicoAgendaServicesTests
{
    public class ExcluirAgendaServiceTests : MedicoAgendaServiceTestsBase
    {
        private readonly Guid _medicoAgendaIdExistente = Guid.NewGuid();
        private readonly Guid _medicoAgendaIdInexistente = Guid.NewGuid();

        [Fact]
        public void ExcluirAgenda_AgendaExistente_ExcluiAgenda_RetornaTrue()
        {
            // Arrange
            MedicoAgenda medicoAgenda = GerarMedicoAgenda(_medicoAgendaIdExistente, Guid.NewGuid(), Guid.NewGuid());
            _mockMedicoAgendaRepository.Setup(repo => repo.RetornarPorId(_medicoAgendaIdExistente)).Returns(medicoAgenda);
            _mockMedicoAgendaRepository.Setup(repo => repo.Editar()).Returns(true);

            // Act
            var resultado = _medicoAgendaService.ExcluirAgenda(_medicoAgendaIdExistente);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void ExcluirAgenda_AgendaInexistente_RetornaFalse()
        {
            // Arrange
            _mockMedicoAgendaRepository.Setup(repo => repo.RetornarPorId(_medicoAgendaIdInexistente)).Returns((MedicoAgenda?)null);
            _mockMedicoAgendaRepository.Setup(repo => repo.Editar()).Returns(true);

            // Act
            var resultado = _medicoAgendaService.ExcluirAgenda(_medicoAgendaIdInexistente);

            // Assert
            Assert.False(resultado);
        }
    }
}
