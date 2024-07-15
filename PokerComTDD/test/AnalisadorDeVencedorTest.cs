using Moq;

namespace PokerComTDD.test
{
    public class AnalisadorDeVencedorTest
    {
        private readonly Mock<IAnalisadorDeVencedorComMaiorCarta> _analisadorDeVencedorComMaiorCarta;
        private readonly Mock<IAnalisadorDeVencedorComParDeCartas> _analisadorDeVencedorComParDeCartas;
        private readonly AnalisadorDeVencedor _analisador;
        private readonly List<string> _cartasDoPrimeiroJogador;
        private readonly List<string> _cartasDoSegundoJogador;

        public AnalisadorDeVencedorTest()
        {
            _analisadorDeVencedorComMaiorCarta = new Mock<IAnalisadorDeVencedorComMaiorCarta>();
            _analisadorDeVencedorComParDeCartas = new Mock<IAnalisadorDeVencedorComParDeCartas>();
            _analisador = new AnalisadorDeVencedor(_analisadorDeVencedorComMaiorCarta.Object, _analisadorDeVencedorComParDeCartas.Object);
            _cartasDoPrimeiroJogador = "2O,4C,3P,6C,7C".Split(',').ToList();
            _cartasDoSegundoJogador = "3O,5C,2E,9C,7C".Split(',').ToList();
        }

        [Fact] 
        public void DeveAnalisarVencedorQueTemMaiorCarta()
        {
            //Arrange
            _analisadorDeVencedorComMaiorCarta.Setup(analisador => analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador))
                .Returns("Segundo Jogador");

            //Act
            _analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador);

            //Assert
            _analisadorDeVencedorComMaiorCarta.Verify(analisador => analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador));
        }

        [Fact]
        public void DeveAnalisarVencedorQueParDeCartas()
        {
            //Arrange
            _analisadorDeVencedorComParDeCartas.Setup(analisador => analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador))
                .Returns("Segundo Jogador");

            //Act
            var vencedor = _analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador);

            //Assert
            _analisadorDeVencedorComParDeCartas.Verify(analisador => analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador));
        }

        [Fact]
        public void NaoDeveAnalisarVencedorComMaiorCartaQuandoJogadaTerCartasComPar()
        {
            //Arrange
            _analisadorDeVencedorComParDeCartas.Setup(analisador => analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador))
                .Returns("Segundo Jogador");

            //Act
            var vencedor = _analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador);

            //Assert
            _analisadorDeVencedorComMaiorCarta
                .Verify(analisador => analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador), Times.Never);
        }
    }
}
