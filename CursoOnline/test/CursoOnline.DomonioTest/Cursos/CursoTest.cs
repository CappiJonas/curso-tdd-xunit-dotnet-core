using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DomonioTest._Builders;
using CursoOnline.DomonioTest._Util;

namespace CursoOnline.DomonioTest.Cursos
{
    public class CursoTest
    {
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;
        private readonly Faker _faker;

        public CursoTest()
        {      
            _faker = new Faker();
            _nome = _faker.Random.Word();
            _cargaHoraria = _faker.Random.Double(50, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = _faker.Random.Double(100, 1000);
            _descricao = _faker.Lorem.Paragraph();
        }

        [Fact]
        public void DeveCriarCurso()
        {
            //Arrange
            var cursoEsperado = new
            {
                Nome =_nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor, 
                Descricao = _descricao
            };

            //Act
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            //Assert
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            // Arrange           
            // Act
            // Assert
            Assert.Throws<ExcecaoDeDominio>(() => 
                CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem(Resource.NomeInvalido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                .ComMensagem(Resource.CargaHorariaInvalida);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmValorInvalido(double valorInvalido)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Novo().ComValor(valorInvalido).Build())
                .ComMensagem(Resource.ValorInvalido);
        }

        [Fact]
        public void DeveAlterarNome()
        {
            // Arrange
            var nomeEsperado = _nome;
            var curso = CursoBuilder.Novo().Build();

            // Act
            curso.AlterarNome(nomeEsperado);

            // Assert
            Assert.Equal(nomeEsperado, curso.Nome);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInválido(string nomeInvalido)
        {
            // Arrange
            var curso = CursoBuilder.Novo().Build();

            // Act
            // Assert
            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarNome(nomeInvalido))
                .ComMensagem(Resource.NomeInvalido);
        }

        [Fact]
        public void DeveAlterarCargaHoraria()
        {
            // Arrange
            var cargaHorariaEsperada = _cargaHoraria;
            var curso = CursoBuilder.Novo().Build();

            // Act
            curso.AlterarCargaHoraria(cargaHorariaEsperada);

            // Assert
            Assert.Equal(cargaHorariaEsperada, curso.CargaHoraria);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveAlterarComCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            // Arrange
            var curso = CursoBuilder.Novo().Build();

            // Act
            // Assert
            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarCargaHoraria(cargaHorariaInvalida))
                .ComMensagem(Resource.CargaHorariaInvalida);
        }

        [Fact]
        public void DeveAlterarValor()
        {
            // Arrange
            var valorEsperado = _valor;
            var curso = CursoBuilder.Novo().Build();

            // Act
            curso.AlterarValor(valorEsperado);

            // Assert
            Assert.Equal(valorEsperado, curso.Valor);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveAlterarComValorInvalido(double valorInvalido)
        {
            // Arrange
            var curso = CursoBuilder.Novo().Build();

            // Act
            // Assert
            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarValor(valorInvalido))
                .ComMensagem(Resource.ValorInvalido);
        }
    }    
}
