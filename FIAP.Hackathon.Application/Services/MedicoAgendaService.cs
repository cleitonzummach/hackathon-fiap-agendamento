using FIAP.Hackathon.Application.Interfaces;
using FIAP.Hackathon.Application.Requests;
using FIAP.Hackathon.Application.Responses;
using FIAP.Hackathon.Domain.Entities;
using FIAP.Hackathon.Domain.Repositories;

namespace FIAP.Hackathon.Application.Services
{
    public class MedicoAgendaService : IMedicoAgendaService
    {
        private readonly IMedicoAgendaRepository _medicoAgendaRepository;
        private readonly IHttpMedicoService _medicoService;
        private readonly IHttpPacienteService _pacienteService;

        public MedicoAgendaService(IMedicoAgendaRepository medicoAgendaRepository, IHttpMedicoService medicoService, IHttpPacienteService pacienteService) 
        {
            _medicoAgendaRepository = medicoAgendaRepository;
            _medicoService = medicoService;
            _pacienteService = pacienteService;
        }

        public bool CriarAgenda(CriarAgendaRequest request)
        {
            try
            {
                MedicoAgenda medicoAgenda = new MedicoAgenda(
                    request.MedicoId,
                    request.EspecialidadeId,
                    request.DataHoraInicio,
                    request.DataHoraFim);

                return _medicoAgendaRepository.Criar(medicoAgenda);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ExcluirAgenda(Guid medicoAgendaId)
        {
            var medicoAgenda = _medicoAgendaRepository.RetornarPorId(medicoAgendaId);

            if (medicoAgenda != null)
            {
                medicoAgenda.Excluir();
                return _medicoAgendaRepository.Editar();
            }

            return false;
        }

        public async Task<List<AgendaResponse>?> RetornarAgendaDisponivel(Guid? especialidadeId)
        {
            List<AgendaResponse> list = new List<AgendaResponse>();

            var medicoAgendas = _medicoAgendaRepository.RetornarAgendaDisponivel(especialidadeId);

            if (medicoAgendas != null)
            {
                foreach (var medicoAgenda in medicoAgendas)
                {
                    MedicoResponse? medico = await _medicoService.ConsultarDadosMedico(medicoAgenda.MedicoId);
                    list.Add(new AgendaResponse(medicoAgenda, medico));
                }
            }

            return list;
        }

        public async Task<List<MedicoAgendaResponse>?> RetornarAgenda(Guid medicoId)
        {
            List<MedicoAgendaResponse> list = new List<MedicoAgendaResponse>();

            var medicoAgendas = _medicoAgendaRepository.RetornarAgenda(medicoId);

            if (medicoAgendas != null)
            {
                foreach (var medicoAgenda in medicoAgendas)
                {
                    PacienteResponse? paciente = null;

                    if (medicoAgenda.Situacao == Domain.Enums.MedicoAgendaEnum.Status.Agendado)
                    {
                        paciente = await _pacienteService.ConsultarDadosPaciente(medicoAgenda.Agendamentos.FirstOrDefault(a => a.Situacao == Domain.Enums.AgendamentoEnum.Status.Agendado).PacienteId);
                    }
                    
                    list.Add(new MedicoAgendaResponse(medicoAgenda, paciente));
                }
            }

            return list;
        }
    }
}
