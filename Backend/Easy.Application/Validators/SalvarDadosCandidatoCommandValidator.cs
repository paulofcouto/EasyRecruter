using FluentValidation;

namespace Easy.Application.Commands.SalvarDadosCandidato
{
    public class SalvarDadosCandidatoCommandValidator : AbstractValidator<SalvarDadosCandidatoCommand>
    {
        public SalvarDadosCandidatoCommandValidator()
        {
            RuleFor(x => x.UrlPublica)
                .NotEmpty().WithMessage("A URL pública é obrigatória.")
                .Matches(@"^https:\/\/www\.linkedin\.com\/in\/[a-zA-Z0-9\-]+\/?$")
                .WithMessage("A URL pública deve ter o formato https://www.linkedin.com/in/{nome}/.");
        }
    }
}
