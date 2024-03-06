using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentarContaCommand : IRequest<MovimentarContaResponse>
    {
        public string IDContaCorrente {  get; set; }
        public char TipoMovimento { get; set; }
        public decimal Valor { get; set; }
    }
}
