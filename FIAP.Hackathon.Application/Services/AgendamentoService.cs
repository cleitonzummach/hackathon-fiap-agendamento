using FIAP.Hackathon.Application.Interfaces;
using FIAP.Hackathon.Application.Requests;
using FIAP.Hackathon.Application.Responses;
using FIAP.Hackathon.Domain.Entities;
using FIAP.Hackathon.Domain.Enums;
using FIAP.Hackathon.Domain.Repositories;

namespace FIAP.Hackathon.Application.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IMedicoAgendaRepository _medicoAgendaRepository;
        private readonly IHttpMedicoService _medicoService;
        private readonly INotificacaoService _notificacaoService;

        public AgendamentoService(
            IAgendamentoRepository agendamentoRepository, 
            IMedicoAgendaRepository medicoAgendaRepository, 
            IHttpMedicoService medicoService, 
            INotificacaoService notificacaoService)
        {
            _agendamentoRepository = agendamentoRepository;
            _medicoAgendaRepository = medicoAgendaRepository;
            _medicoService = medicoService;
            _notificacaoService = notificacaoService;
        }

        public bool Agendar(AgendarRequest request)
        {
            try
            {
                Agendamento agendamento = new Agendamento(
                    request.PacienteId,
                    request.MedicoAgendaId);

                var sucesso = _agendamentoRepository.Criar(agendamento);

                if (sucesso)
                {
                    var medicoAgenda = _medicoAgendaRepository.RetornarPorId(request.MedicoAgendaId);
                    medicoAgenda.AlterarSituacao(MedicoAgendaEnum.Status.Agendado);
                    _medicoAgendaRepository.Editar();

                    _notificacaoService.Registrar(
                        agendamento.AgendamentoId, 
                        Domain.Enums.NotificacaoEnum.TipoNotificacao.LembreteAgendamento, 
                        Domain.Enums.NotificacaoEnum.Status.AguardandoEnvio);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<ConsultarAgendamentoResponse>> ConsultarAgendamento(Guid pacienteId)
        {
            List<ConsultarAgendamentoResponse> list = new List<ConsultarAgendamentoResponse>();

            var agendamentos = _agendamentoRepository.RetornarPorPacienteId(pacienteId);

            if (agendamentos != null)
            {
                foreach (var agendamento in agendamentos)
                {
                    var medico = await _medicoService.ConsultarDadosMedico(agendamento.MedicoAgenda.MedicoId);
                    list.Add(new ConsultarAgendamentoResponse(agendamento, medico));
                }
            }

            return list;
        }

        public bool CancelarAgendamento(Guid agendamentoId)
        {
            var agendamento = _agendamentoRepository.RetornarPorId(agendamentoId);

            if (agendamento != null)
            {
                agendamento.CancelarAgendamento();
                _agendamentoRepository.Editar();

                return _notificacaoService.Cancelar(agendamentoId);
            }

            return false;
        }
    }
}
