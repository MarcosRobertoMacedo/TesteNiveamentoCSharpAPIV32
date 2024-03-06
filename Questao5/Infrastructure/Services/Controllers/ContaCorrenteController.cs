using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly ILogger<ContaCorrenteController> _logger;

        private readonly IMediator _mediator;
        public ContaCorrenteController(ILogger<ContaCorrenteController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet(Name = "GetSaldo/{idContaCorrente}")]
        public async Task<IActionResult> Get(string idContaCorrente)
        {
            var query = new ConsultaSaldoQuery() { IDContaCorrente = idContaCorrente };

            var result = _mediator.Send(query);
            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpPost(Name = "PostMovimentarConta")]
        public async Task<IActionResult> Post([FromBody] MovimentoContaDTO movimentacaoDTO)
        {
            var query = new MovimentarContaCommand()
            {
                IDContaCorrente = movimentacaoDTO.IdContaCorrente,
                TipoMovimento = movimentacaoDTO.TipoMovimento,
                Valor = movimentacaoDTO.Valor
            };

            var result = _mediator.Send(query);
            if (result == null) { return NotFound(); }

            return Ok(result);

        }
    }
}