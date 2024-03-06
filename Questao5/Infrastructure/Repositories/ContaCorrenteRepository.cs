using Dapper;
using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities;
using System.Data;
using System.Drawing;

namespace Questao5.Infrastructure.Repositories
{
    public interface IContaCorrenteRepository
    {
        public Task<ConsultaSaldoDTO> ConsultarSaldoAsync(string idContacorrente);
        public Task<string> MovimentarContaAsync(string idContaCorrente, char tipoMovimento, decimal valor);
    }

    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private IDbConnection _dbConnection;
        public ContaCorrenteRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ConsultaSaldoDTO> ConsultarSaldoAsync(string idContaCorrente)
        {
            var conta = await _dbConnection.QuerySingleOrDefaultAsync<ContaDTO>(
                 "SELECT * FROM contacorrente WHERE idcontacorrente = @IdContaCorrente AND ativo = 1",
                  new { IdContaCorrente = idContaCorrente });

            if (conta == null)
            {
                throw new KeyNotFoundException("Conta corrente não encontrada ou inativa.");
            }

            var movimentos = await _dbConnection.QueryAsync<MovimentoContaDTO>(
                "SELECT * FROM movimento WHERE idcontacorrente = @IdContaCorrente",
                new { IdContaCorrente = idContaCorrente });

            var saldo = movimentos.Where(m => m.TipoMovimento == 'C').Sum(m => m.Valor) -
                         movimentos.Where(m => m.TipoMovimento == 'D').Sum(m => m.Valor);

            var resultado = new ConsultaSaldoDTO
            {
                Numero = conta.Numero,
                Nome = conta.Nome,
                DataHoraConsulta = DateTime.Now,
                Saldo = saldo,
                Ativo = conta.Ativo,
            };

            return resultado;
        }

        public async Task<string> MovimentarContaAsync(string idContaCorrente, char tipoMovimento, decimal valor)
        {

            if (valor <= 0)
            {
                throw new Exception("Valor deve ser positivo.");
            }
            if (tipoMovimento != 'C' && tipoMovimento != 'D')
            {
                throw new Exception("Tipo de movimento inválido.");
            }

            var conta = await _dbConnection.QuerySingleOrDefaultAsync<ContaDTO>(
                 "SELECT * FROM contacorrente WHERE idcontacorrente = @IdContaCorrente AND ativo = 1",
                  new { IdContaCorrente = idContaCorrente });

            if (conta == null)
            {
                throw new Exception("Conta corrente não encontrada ou inativa.");
            }

            var idMovimento = Guid.NewGuid().ToString();
            await _dbConnection.ExecuteAsync(
                "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@IdMovimento, @IdContaCorrente, datetime('now'), @TipoMovimento, @Valor)",
                new { IdMovimento = idMovimento, IdContaCorrente = idContaCorrente, TipoMovimento = tipoMovimento, Valor = valor });


            return idMovimento;
        }
    }
}
