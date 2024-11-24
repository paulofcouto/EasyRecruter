using Easy.Core.Entities;
using Easy.Core.Repository;
using Easy.Core.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Easy.Application.Commands.SalvarDadosCandidato
{
    public class SalvarDadosCandidatoCommandHandler : IRequestHandler<SalvarDadosCandidatoCommand, Result>
    {
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SalvarDadosCandidatoCommandHandler(ICandidatoRepository candidatoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _candidatoRepository = candidatoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result> Handle(SalvarDadosCandidatoCommand request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return Result.Fail("Token JWT não encontrado.");
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var idUsuario = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            if (string.IsNullOrEmpty(idUsuario))
            {
                return Result.Fail("Usuário não identificado no token.");
            }

            var listaExperiencias = request.Experiencias?
                .Select(experiencia =>
                {
                    var listaCargos = experiencia.Cargos?
                        .Select(cargo =>
                        {
                            if (!string.IsNullOrEmpty(cargo.Periodo))
                            {
                                StringHelpers.ExtrairDatasLinkedin(cargo.Periodo, out DateTime dataInicial, out DateTime dataFinal);
                                return new Candidato.Cargo(
                                    cargo.Titulo!,
                                    dataInicial == DateTime.MinValue ? null : dataInicial,
                                    dataFinal == DateTime.MinValue ? null : dataFinal,
                                    cargo.Descricao!
                                );
                            }
                            return null;
                        })
                        .Where(cargo => cargo != null)
                        .ToList();

                    return new Candidato.Experiencia(
                        experiencia.Empresa!,
                        experiencia.Local!,
                        listaCargos
                    );
                })
                .Where(experiencia => experiencia != null)
                .ToList();

            var listaFormacoes = request.Formacoes?
                .Select(formacao =>
                {
                    if (!string.IsNullOrEmpty(formacao.Periodo))
                    {
                        StringHelpers.ExtrairDatasLinkedin(formacao.Periodo, out DateTime dataInicial, out DateTime dataFinal);
                        return new Candidato.Formacao(
                            formacao.Instituicao,
                            formacao.Curso,
                            dataInicial == DateTime.MinValue ? null : dataInicial,
                            dataFinal == DateTime.MinValue ? null : dataFinal
                        );
                    }
                    return null;
                })
                .Where(formacao => formacao != null)
                .ToList();


            var candidato = new Candidato(idUsuario, request.UrlPublica, request.Nome, request.DescricaoProfissional, Convert.FromBase64String(request.Foto), request.Sobre.Replace("<!---->", ""), listaExperiencias, listaFormacoes);

            await _candidatoRepository.CadastrarAssincrono(candidato);

            return Result.Ok();
        }
    }
}
