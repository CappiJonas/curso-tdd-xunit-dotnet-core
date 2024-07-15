using ExpectedObjects;
using System.Drawing;

namespace PokerComTDD.test
{
    public class CartaTest
    {
        [Theory]
        [InlineData("A", "O", 14)]
        [InlineData("10", "E", 10)]
        [InlineData("2", "P", 2)]
        public void DeveCriarUmaCarta(string valorDaCarta, string naipeDaCarta, int peso)
        {
            //Arrange
            var cartaEsperada = new
            {
                Valor = valorDaCarta,
                Naipe = naipeDaCarta, 
                Peso = peso
            };

            //Act
            var carta = new Carta(cartaEsperada.Valor + cartaEsperada.Naipe);

            //Assert
            cartaEsperada.ToExpectedObject().ShouldMatch(carta);
        }

        [Theory]
        [InlineData("V", 11)]
        [InlineData("D", 12)]
        [InlineData("R", 13)]
        [InlineData("A", 14)]
        public void DeveCriarUmaCartaComPeso(string valorDaCarta, int pesoEsperado)
        {
            //Arrange
            //Act
            var carta = new Carta(valorDaCarta + "E");

            //Assert
            Assert.Equal(pesoEsperado, carta.Peso);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("15")]
        [InlineData("-1")]
        public void DeveValidarValorDaCarta(string valorDaCartaInvalido)
        {
            //Arrange
            //Act
            //Assert
            var mensagemDeErro = Assert.Throws<Exception>(() => new Carta(valorDaCartaInvalido + "O")).Message;
            Assert.Equal("Valor da carta inválido.", mensagemDeErro);
        }
        
        [Theory]
        [InlineData("A")]
        [InlineData("Z")]
        public void DeveValidarNaipeDaCarta(string naipeDaCartaInvalida)
        {
            //Arrange
            //Act
            //Assert
            var mensagemDeErro = Assert.Throws<Exception>(() => new Carta("2" + naipeDaCartaInvalida)).Message;
            Assert.Equal("Naipe da carta inválido.", mensagemDeErro);
        }
    }
}
