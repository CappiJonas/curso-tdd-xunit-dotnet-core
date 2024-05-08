using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DomonioTest._Builders;
using CursoOnline.DomonioTest._Util;

namespace CursoOnline.DomonioTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;

        public ArmazenadorDeCursoTest()
        {
            var fake = new Faker();
            _cursoDto = new CursoDto
            {
                Nome = fake.Random.Words(),
                Descricao = fake.Lorem.Paragraph(),
                CargaHoraria = fake.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(1000, 2000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            //Arrange
            //Act
            _armazenadorDeCurso.Armazenar(_cursoDto);

            //Assert
            _cursoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Curso>(
                    c => c.Nome == _cursoDto.Nome &&
                    c.Descricao == _cursoDto.Descricao
                    )
                ));
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            //Arrange
            var publicoAlvoInvalido = "Médico";
            _cursoDto.PublicoAlvo = publicoAlvoInvalido;

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem(Resource.PublicoAlvoInvalido);
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            //Arrange
            var cursoJaSalvo = CursoBuilder.Novo().ComId(432).ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem(Resource.NomeDoCursoJaExiste);
        }

        [Fact]
        public void DeveAlterarDadosDoCurso()
        {
            //Arrange
            _cursoDto.Id = 323;
            var curso = CursoBuilder.Novo().Build();
            _cursoRepositorioMock.Setup(r => r.ObterPorId(_cursoDto.Id)).Returns(curso);

            //Act
            _armazenadorDeCurso.Armazenar(_cursoDto);

            //Assert
            Assert.Equal(_cursoDto.Nome, curso.Nome);
            Assert.Equal(_cursoDto.Valor, curso.Valor);
            Assert.Equal(_cursoDto.CargaHoraria, curso.CargaHoraria);
        }

        [Fact]
        public void NaoDeveAdicionarNoRepositorioQuandoCursoJaExiste()
        {
            //Arrange
            _cursoDto.Id = 323;
            var curso = CursoBuilder.Novo().Build();
            _cursoRepositorioMock.Setup(r => r.ObterPorId(_cursoDto.Id)).Returns(curso);

            //Act
            _armazenadorDeCurso.Armazenar(_cursoDto);

            //Assert
            _cursoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Curso>()), Times.Never);
        }
    }
}
