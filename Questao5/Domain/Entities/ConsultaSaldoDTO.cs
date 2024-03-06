using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questao5.Domain.Entities
{
    public class ConsultaSaldoDTO
    {
        public string Numero { get; set; }
        public string Nome { get; set; }
        public DateTime DataHoraConsulta { get; set; }
        public decimal Saldo { get; set; }
        public string Ativo { get; set; }
    }
}
