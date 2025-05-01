using FIAP.Hackathon.Application.Requests;
using FluentValidation;

namespace FIAP.Hackathon.Application.Validator
{
    public class CriarAgendaRequestValidator : AbstractValidator<CriarAgendaRequest>
    {
        public CriarAgendaRequestValidator()
        {
            RuleFor(p => p.MedicoId)
                .NotNull().NotEmpty().WithMessage("O médico é obrigatório.");

            RuleFor(p => p.EspecialidadeId)
                .NotNull().NotEmpty().WithMessage("A especialidade é obrigatória.");

            RuleFor(p => p.DataHoraInicio)
                .NotNull().NotEmpty().WithMessage("A data/hora inicial é obrigatório.");

            RuleFor(p => p.DataHoraFim)
                .NotNull().NotEmpty().WithMessage("A data/hora final é obrigatório.");

            RuleFor(p => p.DataHoraInicio)
                .LessThan(p => p.DataHoraFim).WithMessage("A data/hora inicial deve ser menor que a data/hora final.");
        }
    }
}
