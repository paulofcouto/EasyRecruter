using Easy.Core.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Application.Commands.AtualizarCandidato
{
    public class AtualizarCandidatoCommand : IRequest<Result>
    {
        public string Id { get; set; }
        public string UrlPublica { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
    }
}
