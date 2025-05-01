using FIAP.Hackathon.Domain.Entities;

namespace FIAP.Hackathon.Tests.AgendamentoServicesTests
{
    public class CancelarAgendamentoServiceTests : AgendamentoServiceTestsBase
    {
        private readonly Guid _agendamentoIdExistente = Guid.NewGuid();
        private readonly Guid _agendamentoIdInexistente = Guid.NewGuid();

        [Fact]
        public void CancelarAgendamento_AgendamentoExistente_CancelaAgendamento_RetornaTrue()
        {
            // Arrange
            Agendamento agendamento = new Agendamento(Guid.NewGuid(), Guid.NewGuid());
            _mockAgendamentoRepository.Setup(repo => repo.RetornarPorId(_agendamentoIdExistente)).Returns(agendamento);
            _mockAgendamentoRepository.Setup(repo => repo.Editar()).Returns(true);
            _mockNotificacaoService.Setup(service => service.Cancelar(_agendamentoIdExistente)).Returns(true);

            // Act
            var resultado = _agendamentoService.CancelarAgendamento(_agendamentoIdExistente);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void CancelarAgendamento_AgendamentoInexistente_RetornaFalse()
        {
            // Arrange
            _mockAgendamentoRepository.Setup(repo => repo.RetornarPorId(_agendamentoIdInexistente)).Returns((Agendamento?)null);
            _mockAgendamentoRepository.Setup(repo => repo.Editar()).Returns(true);

            // Act
            var resultado = _agendamentoService.CancelarAgendamento(_agendamentoIdInexistente);

            // Assert
            Assert.False(resultado);
        }
    }
}
