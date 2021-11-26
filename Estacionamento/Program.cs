using System;
using static Estacionamento.Program;

namespace Estacionamento
{
    public class Program
    {
        static void Main(string[] args)
        {
            var uno = new Carro(DateTime.Now, 0.8m, "LWR-0001");
            var moto = new Moto(DateTime.Now, 0.2m, "MMM-9999");
            uno.CalcularTempo();
            moto.CalcularTempo();
            Console.WriteLine("Saida uno:" + uno.TempoPermanencia.ToString());
            Console.WriteLine("Saida moto:" + moto.TempoPermanencia.ToString());
            Console.WriteLine($"\nValor a Pagar: {uno.Pagamento():C}");
            Console.WriteLine($"\nValor a Pagar: {moto.Pagamento():C}");
        }
        public abstract class Veiculo
        {
            public DateTime Entrada { get; protected set; }
            public DateTime Saida { get; protected set; }
            public decimal TempoPermanencia { get; protected set; }
            public decimal Largura { get; protected set; }
            public string Placa { get; protected set; }
            public bool Diaria { get; protected set; } = false;
            public abstract void CalcularTempo();
        }
        public class Carro : Veiculo
        {
            public bool Lavado { get; set; }
            public Carro(DateTime entrada, decimal largura, string placa)
            {
                Entrada = entrada;
                Largura = largura;
                Placa = placa;
            }
            public Carro(DateTime entrada, decimal largura, string placa, bool diaria)
            {
                Entrada = entrada;
                Largura = largura;
                Placa = placa;
                Diaria = diaria;
            }
            public void Lavagem()
            {
                if(Diaria == false)
                {
                    throw new Exception("Precisa ter comprado a diária para ter a opção de lavagem");
                }
                Console.WriteLine("O seu carro será lavado.");
            }
            public override void CalcularTempo()
            {
                var t = DateTime.Compare(Entrada, DateTime.Now);
                if (t > 0 || t == 0)
                {
                    throw new Exception("Tempo inválido");
                }
                Saida = DateTime.Now.AddDays(1);
                var tempo = Saida - Entrada;
                TempoPermanencia = (decimal)tempo.TotalHours;
                TempoPermanencia = decimal.Round(TempoPermanencia, 2);
            }
        }
        public class Moto : Veiculo
        {
            public Moto(DateTime entrada, decimal largura, string placa)
            {
                Entrada = entrada;
                Largura = largura;
                Placa = placa;
            }
            public Moto(DateTime entrada, decimal largura, string placa, bool diaria)
            {
                Entrada = entrada;
                Largura = largura;
                Placa = placa;
                Diaria = diaria;
            }
            public override void CalcularTempo()
            {
                var t = DateTime.Compare(Entrada, DateTime.Now);
                if (t > 0 || t == 0)
                {
                    throw new Exception("Tempo inválido");
                }
                Saida = DateTime.Now.AddMinutes(5);
                var tempo = Saida - Entrada;
                TempoPermanencia = (decimal)tempo.TotalHours;
                TempoPermanencia = decimal.Round(TempoPermanencia, 2);
            }
        }
    }
    public static class VeiculoExtensions
    {
        public static decimal Pagamento(this Carro carro)
        {
            carro.CalcularTempo();
            if(carro.Diaria == true)
            {
                if(carro.Lavado == true)
                {
                    return 65m;
                }
                return 50m;
            }
            else if(carro.TempoPermanencia <= 0.25m) 
            {
                return 2m;
            }
            else if (carro.TempoPermanencia > 0.25m) 
            {
                return carro.TempoPermanencia * 10;
            }
            throw new Exception("Pagamento Invalido");
        }
        public static decimal Pagamento(this Moto moto)
        {
            moto.CalcularTempo();
            if (moto.Diaria == true)
            {
                return 25m;
            }
            else if (moto.TempoPermanencia <= 0.25m)
            {
                return 2m;
            }
            else if (moto.TempoPermanencia > 0.25m)
            {
                return moto.TempoPermanencia * 5;
            }
            throw new Exception("Pagamento Invalido");
        }
    }
}
