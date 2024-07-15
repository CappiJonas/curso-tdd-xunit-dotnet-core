
namespace PokerComTDD.test
{
    public class AnalisadorDeVencedorComParDeCartasTest
    {
        private readonly AnalisadorDeVencedorComParDeCartas _analisador;

        public AnalisadorDeVencedorComParDeCartasTest()
        {
            _analisador = new AnalisadorDeVencedorComParDeCartas();
        }

        [Theory]
        [InlineData("2O,2C,3P,6C,7C", "3O,5C,2E,9C,7C", "Primeiro Jogador")]
        [InlineData("3O,5C,2E,9C,7C", "2O,2C,3P,6C,7C", "Segundo Jogador")]
        [InlineData("2O,2C,3P,6C,7C", "DO,DC,2E,9C,7C", "Segundo Jogador")]
        [InlineData("DO,DC,2E,9C,7C", "2O,2C,3P,6C,7C", "Primeiro Jogador")]
        public void DeveAnalisarVencedorQuandoTiverUmParDeCartasDoMesmoValor(string cartasDoPrimeiroJogadorString, string cartasDoSegundoJogadorString,
           string vencedorEsperado)
        {
            //Arrange
            var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorString.Split(',').ToList();
            var cartasDoSegundoJogador = cartasDoSegundoJogadorString.Split(',').ToList();

            //Act
            var vencedor = _analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            //Assert
            Assert.Equal(vencedorEsperado, vencedor);
        }

        [Fact]
        public void NaoDeveAnalisarVencedorQuandoJogadoresNaoTemParDeCartas()
        {
            //Arrange
            var cartasDoPrimeiroJogador = "2O,4C,3P,6C,7C".Split(',').ToList();
            var cartasDoSegundoJogador = "3O,5C,2E,9C,7C".Split(',').ToList();

            //Act
            var vencedor = _analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            //Assert
            Assert.Null(vencedor);
        }
    }
}
