namespace PokerComTDD.test
{
    public class AnalisadorDeVencedorComMaiorCartaTest
    {
        [Theory]
        [InlineData("2O,4C,3P,6C,7C", "3O,5C,2E,9C,7C", "Segundo Jogador")]
        [InlineData("3O,5C,2E,9C,7C", "2O,4C,3P,6C,7C", "Primeiro Jogador")]
        [InlineData("3O,5C,2E,9C,7C", "2O,4C,3P,6C,10E", "Segundo Jogador")]
        [InlineData("3O,5C,2E,9C,VP", "2O,4C,3P,6C,AE", "Segundo Jogador")]
        public void DeveAnalisarVencedorQuandoTiverMaiorCarta(string cartasDoPrimeiroJogadorString, string cartasDoSegundoJogadorString,
            string vencedorEsperado)
        {
            //Arrange
            var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorString.Split(',').ToList();
            var cartasDoSegundoJogador = cartasDoSegundoJogadorString.Split(',').ToList();
            var analisador = new AnalisadorDeVencedorComMaiorCarta();

            //Act
            var vencedor = analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            //Assert
            Assert.Equal(vencedorEsperado, vencedor);
        }
    }
}
