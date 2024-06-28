using Easy.Core.Result;
using MediatR;

namespace Easy.Application.Commands.CadastrarCandidato
{
    public class CadastrarCandidatoCommand : IRequest<Result>
    {
        public string URLPublica { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
    }
}
