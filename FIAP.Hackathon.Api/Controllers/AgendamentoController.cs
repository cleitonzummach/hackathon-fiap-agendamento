using FIAP.Hackathon.Application.Interfaces;
using FIAP.Hackathon.Application.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace FIAP.Hackathon.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AgendamentoController : Controller
    {
        private readonly IMedicoAgendaService _medicoAgendaService;
        private readonly IAgendamentoService _agendamentoService;
        private readonly IHttpMedicoService _medicoService;
        private readonly IHttpPacienteService _pacienteService;
        private readonly IValidator<CriarAgendaRequest> _criarAgendaRequestValidator;
        private readonly IValidator<AgendarRequest> _agendarRequestValidator;

        public AgendamentoController(
            IMedicoAgendaService medicoAgendaService, 
            IAgendamentoService agendamentoService, 
            IHttpMedicoService medicoService, 
            IHttpPacienteService pacienteService,
            IValidator<CriarAgendaRequest> criarAgendaRequestValidator,
            IValidator<AgendarRequest> agendarRequestValidator) 
        {
            _medicoAgendaService = medicoAgendaService;
            _agendamentoService = agendamentoService;
            _medicoService = medicoService;
            _pacienteService = pacienteService;
            _criarAgendaRequestValidator = criarAgendaRequestValidator;
            _agendarRequestValidator = agendarRequestValidator;
        }

        #region Médico

        [HttpPost("medico/{medicoId}/criar")]
        public async Task<IActionResult> CriarAgenda(Guid medicoId, [FromBody] CriarAgendaRequest request)
        {
            if (!AutenticarRequisicao(medicoId.ToString()))
                return Unauthorized();

            bool valido = await _medicoService.MedicoValido(medicoId);

            if (!valido)
                return BadRequest();

            var validationResult = _criarAgendaRequestValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { Property = e.PropertyName, Message = e.ErrorMessage }).ToList();
                return BadRequest(new { Errors = errors });
            }

            var sucesso = _medicoAgendaService.CriarAgenda(request);

            if (sucesso)
                return Ok();

            return BadRequest("Ocorreu um erro ao criar o agendamento.");
        }

        [HttpDelete("medico/{medicoId}/excluir/{medicoAgendaId}")]
        public async Task<IActionResult> ExcluirAgenda([Required] Guid medicoId, [Required] Guid medicoAgendaId)
        {
            if (!AutenticarRequisicao(medicoId.ToString()))
                return Unauthorized();

            bool valido = await _medicoService.MedicoValido(medicoId);

            if (!valido)
                return BadRequest();

            var sucesso = _medicoAgendaService.ExcluirAgenda(medicoAgendaId);

            if (sucesso)
                return Ok();

            return BadRequest("Ocorreu um erro ao excluir o agendamento.");
        }

        [HttpGet("medico/{medicoId}")]
        public async Task<IActionResult> RetornarAgenda(Guid medicoId)
        {
            if (!AutenticarRequisicao(medicoId.ToString()))
                return Unauthorized();

            bool valido = await _medicoService.MedicoValido(medicoId);

            if (!valido)
                return BadRequest();

            var dados = await _medicoAgendaService.RetornarAgenda(medicoId);
            
            return Ok(dados);
        }

        #endregion

        #region Paciente

        [HttpGet("paciente/pesquisar")]
        public async Task<IActionResult> PesquisarAgendaDisponivel(Guid? especialidadeId)
        {
            var dados = await _medicoAgendaService.RetornarAgendaDisponivel(especialidadeId);
            return Ok(dados);
        }

        [HttpPost("paciente/{pacienteId}/agendar")]
        public async Task<IActionResult> Agendar(Guid pacienteId, [FromBody] AgendarRequest request)
        {
            if (!AutenticarRequisicao(pacienteId.ToString()))
                return Unauthorized();

            bool valido = await _pacienteService.PacienteValido(pacienteId);

            if (!valido)
                return BadRequest();

            var validationResult = _agendarRequestValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { Property = e.PropertyName, Message = e.ErrorMessage }).ToList();
                return BadRequest(new { Errors = errors });
            }

            var sucesso = _agendamentoService.Agendar(request);

            if (sucesso)
                return Ok();

            return BadRequest("Ocorreu um erro ao criar o agendamento.");
        }

        [HttpGet("paciente/{pacienteId}")]
        public async Task<IActionResult> ConsultarAgendamentos([Required] Guid pacienteId)
        {
            if (!AutenticarRequisicao(pacienteId.ToString()))
                return Unauthorized();

            bool valido = await _pacienteService.PacienteValido(pacienteId);

            if (!valido)
                return BadRequest();

            var dados = await _agendamentoService.ConsultarAgendamento(pacienteId);
            
            return Ok(dados);
        }

        [HttpPut("paciente/{pacienteId}/reagendar/{agendamentoId}")]
        public async Task<IActionResult> ReagendarAgendamento([Required] Guid pacienteId, [Required] Guid agendamentoId, [FromBody] ReagendarRequest request)
        {
            if (!AutenticarRequisicao(pacienteId.ToString()))
                return Unauthorized();

            bool valido = await _pacienteService.PacienteValido(pacienteId);

            if (!valido)
                return BadRequest();

            var sucesso = _agendamentoService.CancelarAgendamento(agendamentoId);

            if (sucesso) 
            {
                sucesso = _agendamentoService.Agendar(new AgendarRequest()
                {
                    PacienteId = pacienteId,
                    MedicoAgendaId = request.MedicoAgendaId
                });

                if (sucesso)
                    return Ok();
            }

            return BadRequest("Não foi possível realizar o reagendamento.");
        }

        [HttpDelete("paciente/{pacienteId}/cancelar/{agendamentoId}")]
        public async Task<IActionResult> CancelarAgendamento([Required] Guid pacienteId, [Required] Guid agendamentoId)
        {
            if (!AutenticarRequisicao(pacienteId.ToString()))
                return Unauthorized();

            bool valido = await _pacienteService.PacienteValido(pacienteId);

            if (!valido)
                return BadRequest();

            var sucesso = _agendamentoService.CancelarAgendamento(agendamentoId);

            if (sucesso)
                return Ok();

            return BadRequest("Não foi possível cancelar o agendamento.");
        }

        #endregion

        private bool AutenticarRequisicao(string id)
        {
            string loginId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return loginId == id;
        }
    }
}