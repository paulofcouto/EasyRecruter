using Easy.Core.Entities;
using Easy.Core.Repository;
using Easy.Core.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Easy.Application.Commands.AtualizarCandidato
{
    public class AtualizarCandidatoCommandHandler : IRequestHandler<AtualizarCandidatoCommand, Result>
    {
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AtualizarCandidatoCommandHandler(ICandidatoRepository candidatoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _candidatoRepository = candidatoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result> Handle(AtualizarCandidatoCommand request, CancellationToken cancellationToken)
        {
            var idUsuario = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idUsuario))
            {
                return Result.Fail("Usuário não autenticado.");
            }

            var candidato = new Candidato(idUsuario, request.UrlPublica, request.Nome, request.Cargo, "", new List<Candidato.Experiencia>(), new List<Candidato.Formacao>());

            await _candidatoRepository.EditarAssincrono(candidato);

            return Result.Ok();
        }
    }
}
