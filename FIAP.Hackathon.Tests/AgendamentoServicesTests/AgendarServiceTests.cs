using FIAP.Hackathon.Domain.Entities;
using FIAP.Hackathon.Domain.Enums;
using Moq;

namespace FIAP.Hackathon.Tests.AgendamentoServicesTests
{
    public class AgendarServiceTests : AgendamentoServiceTestsBase
    {
        [Fact]
        public void Agendar_CriacaoAgendamentoSucesso_RetornaTrue()
        {
            // Arrange
            var request = GerarAgendarRequestValido();
            var medicoAgenda = new MedicoAgenda(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1));
            var agendamento = GerarAgendamento(request.PacienteId, request.MedicoAgendaId);

            _mockAgendamentoRepository.Setup(repo => repo.Criar(It.IsAny<Agendamento>())).Returns(true);
            _mockMedicoAgendaRepository.Setup(repo => repo.RetornarPorId(request.MedicoAgendaId)).Returns(medicoAgenda);
            _mockMedicoAgendaRepository.Setup(repo => repo.Editar()).Returns(true);
            _mockNotificacaoService.Setup(service => service.Registrar(
                agendamento.AgendamentoId,
                NotificacaoEnum.TipoNotificacao.LembreteAgendamento,
                NotificacaoEnum.Status.AguardandoEnvio)).Returns(true);

            // Act
            var resultado = _agendamentoService.Agendar(request);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void Agendar_CriacaoAgendamentoComErro_RetornaFalse()
        {
            // Arrange
            var request = GerarAgendarRequestValido();

            _mockAgendamentoRepository.Setup(repo => repo.Criar(It.IsAny<Agendamento>())).Returns(false);

            // Act
            var resultado = _agendamentoService.Agendar(request);

            // Assert
            Assert.False(resultado);
        }
    }
}
