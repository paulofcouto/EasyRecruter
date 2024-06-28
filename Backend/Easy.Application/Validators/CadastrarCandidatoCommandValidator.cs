using Easy.Application.Commands.CadastrarCandidato;
using Easy.Application.Commands.CadastrarUsuario;
using FluentValidation;

namespace Easy.Application.Validators
{
    public class CadastrarCandidatoCommandValidator : AbstractValidator<CadastrarCandidatoCommand>
    {
        public CadastrarCandidatoCommandValidator()
        {
            RuleFor(t => t.Nome).MaximumLength(250).WithMessage("Nome não pode ter mais do que 250 caracteres!");


            RuleFor(usuario => usuario.Cargo).MaximumLength(250).WithMessage("Cargo não pode ter mais do que 250 caracteres!");
        }
    }
}
