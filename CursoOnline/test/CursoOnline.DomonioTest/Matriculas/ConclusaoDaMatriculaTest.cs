using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DomonioTest._Builders;
using CursoOnline.DomonioTest._Util;

namespace CursoOnline.DomonioTest.Matriculas
{
    public class ConclusaoDaMatriculaTest
    {
        private readonly Mock<IMatriculaRepositorio> _matriculaRepositorio;
        private readonly ConclusaoDaMatricula _conclusaoDaMatricula;

        public ConclusaoDaMatriculaTest()
        {
            _matriculaRepositorio = new Mock<IMatriculaRepositorio>();
            _conclusaoDaMatricula = new ConclusaoDaMatricula(_matriculaRepositorio.Object);
        }

        [Fact]
        public void DeveInformarNotaDoAluno()
        {
            //Arrange
            const double notaDoAlunoEsperada = 8;
            var matricula = MatriculaBuilder.Novo().Build();
            _matriculaRepositorio.Setup(r => r.ObterPorId(matricula.Id)).Returns(matricula);

            //Act
            _conclusaoDaMatricula.Concluir(matricula.Id, notaDoAlunoEsperada);

            //Assert
            Assert.Equal(notaDoAlunoEsperada, matricula.NotaDoAluno);
        }

        [Fact]
        public void DeveNotificarQuandoMatriculaNaoEncontrada()
        {
            //Arrange
            Matricula? matriculaInvalida = null;
            const int matriculaIdInvalida = 1;
            const double notaDoAluno = 2;
            _matriculaRepositorio.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns(matriculaInvalida);

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                    _conclusaoDaMatricula.Concluir(matriculaIdInvalida, notaDoAluno))
                .ComMensagem(Resource.MatriculaNaoEncontrada);
        }
    }
}
