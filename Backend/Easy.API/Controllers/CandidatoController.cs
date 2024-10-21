using Easy.Application.Commands.AtualizarCandidato;
using Easy.Application.Commands.CadastrarCandidato;
using Easy.Application.Commands.DeletarCandidato;
using Easy.Application.Queries.ObterCandidatoPorId;
using Easy.Application.Queries.ObterCandidatosUsuarioLogado;
using Easy.Application.ViewModel;
using Easy.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Easy.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidatoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET: api/candidatos
        [HttpGet("candidatos")]
        public async Task<ActionResult<List<CandidatoViewModel>>> Get()
        {
            var query = new ObterCandidatosUsuarioLogadoQuery();

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }
        
        //GET: api/candidato/5
        [HttpGet("candidato/{id}")]
        public async Task<ActionResult<CandidatoDetalhadoViewModel>> Get(string id)
        {
            var query = new ObterCandidatoPorIdQuery(id);

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }
        
        // POST: api/Candidato
        [HttpPost]
        public async Task<ActionResult<Candidato>> Post([FromBody] CadastrarCandidatoCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok("Candidato cadastrado com sucesso.");
            }
            else
            {
                if (result.Error.Any())
                {
                    return BadRequest(result.Error);
                }

                return StatusCode(500, "Ocorreu um erro inesperado ao cadastrar o candidato.");
            }
        }

        // PUT: api/Candidato/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Candidato>> Put(string id, [FromBody] AtualizarCandidatoCommand command)
        {
            command.Id = id;
            
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok("Candidato alterado com sucesso.");
            }
            else
            {
                if (result.Error.Any())
                {
                    return BadRequest(result.Error);
                }

                return StatusCode(500, "Ocorreu um erro inesperado ao editar o candidato.");
            }
        }
        
        // DELETE: api/Candidato/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var command = new DeletarCandidatoCommand(id);

            var result = await _mediator.Send(command);
        
            if (!result.IsSuccess)
            {
                return NotFound();
            }
        
            return NoContent();
        }
    }
}


