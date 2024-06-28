using Easy.Application.ViewModel;
using Easy.Core.Entities;
using Easy.Core.Repository;
using Easy.Core.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Easy.Application.Commands.CadastrarCandidato
{
    public class CadastrarCandidatoCommandHandler : IRequestHandler<CadastrarCandidatoCommand, Result>
    {
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CadastrarCandidatoCommandHandler(ICandidatoRepository candidatoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _candidatoRepository = candidatoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result> Handle(CadastrarCandidatoCommand request, CancellationToken cancellationToken)
        {
            var email = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Result.Fail("Usuário não autenticado.");
            }

            var candidato = new Candidato(request.URLPublica, email, request.Nome, request.Cargo);
            
            await _candidatoRepository.CadastrarAssincrono(candidato);

            return Result.Ok();
        }
    }
}
