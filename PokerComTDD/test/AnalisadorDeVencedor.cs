namespace PokerComTDD.test
{
    public class AnalisadorDeVencedor
    {
        private readonly IAnalisadorDeVencedorComMaiorCarta _analisadorDeVencedorComMaiorCarta;
        private readonly IAnalisadorDeVencedorComParDeCartas _analisadorDeVencedorComParDeCartas;

        public AnalisadorDeVencedor(IAnalisadorDeVencedorComMaiorCarta analisadorDeVencedorComMaiorCarta, 
                                    IAnalisadorDeVencedorComParDeCartas analisadorDeVencedorComParDeCartas)
        {
            _analisadorDeVencedorComMaiorCarta = analisadorDeVencedorComMaiorCarta;
            _analisadorDeVencedorComParDeCartas = analisadorDeVencedorComParDeCartas;
        }

        public string Analisar(List<string> cartasDoPrimeiroJogador, List<string> cartasDoSegundoJogador)
        {
            var vencedor = _analisadorDeVencedorComParDeCartas.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            if (vencedor == null)
                vencedor = _analisadorDeVencedorComMaiorCarta.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            return vencedor;
        }       
    }
}
