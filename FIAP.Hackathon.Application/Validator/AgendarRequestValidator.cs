using FIAP.Hackathon.Application.Requests;
using FluentValidation;

namespace FIAP.Hackathon.Application.Validator
{
    public class AgendarRequestValidator : AbstractValidator<AgendarRequest>
    {
        public AgendarRequestValidator()
        {
            RuleFor(p => p.PacienteId)
                .NotNull().NotEmpty().WithMessage("O paciente é obrigatório.");

            RuleFor(p => p.MedicoAgendaId)
                .NotNull().NotEmpty().WithMessage("A agenda do médico é obrigatória.");
        }
    }
}
