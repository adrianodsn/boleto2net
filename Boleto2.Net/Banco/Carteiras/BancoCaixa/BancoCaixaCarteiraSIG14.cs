﻿using System;
using Boleto2Net.Extensions;
using static System.String;

namespace Boleto2Net
{
    [CarteiraCodigo("SIG14")]
    internal class BancoCaixaCarteiraSIG14 : ICarteira<BancoCaixa>
    {
        internal static Lazy<ICarteira<BancoCaixa>> Instance { get; } = new Lazy<ICarteira<BancoCaixa>>(() => new BancoCaixaCarteiraSIG14());

        private BancoCaixaCarteiraSIG14()
        {

        }

        public void FormataNossoNumero(Boleto boleto)
        {
            // Carteira SIG14: Dúvida: Se o Cliente SEMPRE emite o boleto, pois o nosso número começa com 14, o que significa Título Registrado emissão Empresa:
            // O nosso número não pode ser em branco.
            if (IsNullOrWhiteSpace(boleto.NossoNumero))
                throw new Exception("Nosso Número não informado.");
            
            // O nosso número deve estar formatado corretamente (com 17 dígitos e iniciando com o "14"),
            if (boleto.NossoNumero.Length == 17 && !boleto.NossoNumero.StartsWith("14"))
                throw new Exception($"Nosso Número ({boleto.NossoNumero}) deve iniciar com \"14\" e conter 17 dígitos.");

            // Ou deve ser informado com até 15 posições (será formatado para 17 dígitos pelo Boleto.Net).
            if (boleto.NossoNumero.Length > 15)
                throw new Exception($"Nosso Número ({boleto.NossoNumero}) deve iniciar com \"14\" e conter 17 dígitos.");

            boleto.NossoNumero = $"14{boleto.NossoNumero.PadLeft(15, '0')}";
            boleto.NossoNumeroDV = boleto.NossoNumero.CalcularDVCaixa();
            boleto.NossoNumeroFormatado = $"{boleto.NossoNumero}-{boleto.NossoNumeroDV}";
        }
    }
}
