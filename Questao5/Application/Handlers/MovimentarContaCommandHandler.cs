using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Infrastructure.Repositories;

namespace Questao5.Application.Handlers
{
    public class MovimentarContaCommandHandler : IRequestHandler<MovimentarContaCommand, MovimentarContaResponse>
    {
        private IContaCorrenteRepository _contaCorrenteRepository;
        public MovimentarContaCommandHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<MovimentarContaResponse> Handle(MovimentarContaCommand request, CancellationToken cancellationToken)
        {
            var result = await _contaCorrenteRepository.MovimentarContaAsync(request.IDContaCorrente, request.TipoMovimento, request.Valor);
            var response = new MovimentarContaResponse() { IDMovimento = result };
            
            return response;
        }
    }
}
