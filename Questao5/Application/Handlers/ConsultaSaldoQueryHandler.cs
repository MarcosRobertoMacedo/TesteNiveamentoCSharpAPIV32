using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Repositories;

namespace Questao5.Application.Handlers
{
    public class ConsultaSaldoQueryHandler : IRequestHandler<ConsultaSaldoQuery, ConsultaSaldoResponse>
    {
        private IContaCorrenteRepository _contaCorrenteRepository;
        public ConsultaSaldoQueryHandler(IContaCorrenteRepository contaCorrenteRepository) 
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<ConsultaSaldoResponse> Handle(ConsultaSaldoQuery request, CancellationToken cancellationToken)
        {
            var result = await _contaCorrenteRepository.ConsultarSaldoAsync(request.IDContaCorrente);
            var response = new ConsultaSaldoResponse() { Saldo = result.Saldo, DataHoraConsulta = result.DataHoraConsulta,
            Nome = result.Nome, Numero = result.Numero, Ativo = result.Ativo};
            
            return response;
        }
    }
}
