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
            var email = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Result.Fail("Usuário não autenticado.");
            }

            var candidato = new Candidato(request.UrlPublica, email, request.Nome, request.Cargo);

            await _candidatoRepository.EditarAssincrono(candidato);

            return Result.Ok();
        }
    }
}
