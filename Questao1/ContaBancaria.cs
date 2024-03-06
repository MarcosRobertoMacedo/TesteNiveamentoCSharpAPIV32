using System.Globalization;
using System;

namespace Questao1
{
    public class ContaBancaria
    {
        private int _numeroConta;
        private string _titular;
        private double _quantia;

        public ContaBancaria(int numeroConta, string titular)
        {
            _numeroConta = numeroConta;
            _titular = titular;
            _quantia = 0;
        }

        public ContaBancaria(int numeroConta, string titular, double depositoInicial) : this(numeroConta, titular)
        {
            Deposito(depositoInicial);
        }

        public int NumeroConta { get { return _numeroConta; } }
        public string Titular { get { return _titular; } set { _titular = value; } }
        public double Quantia { get { return _quantia; } }

        public void Deposito(double valor)
        {
            _quantia += valor;
        }

        public void Saque(double valor)
        {
            double taxa = 3.5;
            if (_quantia >= valor + taxa)
            {
                _quantia -= valor + taxa;
            }
            else
            {
                Console.WriteLine("Saldo insuficiente para realizar o saque.");
            }
        }

        public override string ToString()
        {
            return $"Conta {_numeroConta}, Titular: {_titular}, Saldo: ${_quantia:F2}";
        }
    }
}
