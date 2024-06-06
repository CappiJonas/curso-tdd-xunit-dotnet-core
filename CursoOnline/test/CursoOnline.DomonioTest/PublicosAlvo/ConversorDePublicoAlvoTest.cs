using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DomonioTest._Util;

namespace CursoOnline.DomonioTest.PublicosAlvo
{
    public class ConversorDePublicoAlvoTest
    {
        private readonly ConversorDePublicoAlvo _conversor;

        public ConversorDePublicoAlvoTest()
        {
            _conversor = new ConversorDePublicoAlvo();
        }

        [Theory]
        [InlineData(PublicoAlvo.Empregado, "Empregado")]
        [InlineData(PublicoAlvo.Empreendedor, "Empreendedor")]
        [InlineData(PublicoAlvo.Universitario, "Universitario")]
        [InlineData(PublicoAlvo.Estudante, "Estudante")]
        public void DeveConverterPublicoAlvo(PublicoAlvo publicoAlvoEsperado, string publicoAlvoEmString)
        {
            //Arrange
            //Act
            var publicoAlvoConvertido = _conversor.Converter(publicoAlvoEmString);

            //Assert
            Assert.Equal(publicoAlvoEsperado, publicoAlvoConvertido);
        }

        [Fact]
        public void NaoDeveConverterQuandoPublicoAlvoEhInvalido()
        {
            //Arrange
            const string publicoAlvoInvalido = "Invalido";

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => _conversor.Converter(publicoAlvoInvalido))
                .ComMensagem(Resource.PublicoAlvoInvalido);
        }
    }
}
