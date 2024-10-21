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
            var idUsuario = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idUsuario))
            {
                return Result.Fail("Usuário não autenticado.");
            }

            var lista = new List<Candidato.Experiencia>();
            var experiencia = new Candidato.Experiencia("", "", new DateTime(2024, 10, 1), new DateTime(2024,10,1), "", "");

            lista.Add(experiencia);


            var candidato = new Candidato(idUsuario, request.URLPublica, request.Nome, request.Cargo , "", lista, new List<Candidato.Formacao>());
            
            await _candidatoRepository.CadastrarAssincrono(candidato);

            return Result.Ok();
        }
    }
}
