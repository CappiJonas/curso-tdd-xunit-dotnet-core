using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DomonioTest._Builders;
using CursoOnline.DomonioTest._Util;

namespace CursoOnline.DomonioTest.Matriculas
{
    public class CancelamentoDaMatriculaTest
    {
        private readonly Mock<IMatriculaRepositorio> _matriculaRepositorio;
        private readonly CancelamentoDaMatricula _cancelamentoDaMatricula;

        public CancelamentoDaMatriculaTest()
        {
            _matriculaRepositorio = new Mock<IMatriculaRepositorio>();
            _cancelamentoDaMatricula = new CancelamentoDaMatricula(_matriculaRepositorio.Object);
        }

        [Fact]
        public void DeveCancelarMatricula()
        {
            //Arrange
            var matricula = MatriculaBuilder.Novo().Build();
            _matriculaRepositorio.Setup(r => r.ObterPorId(matricula.Id)).Returns(matricula);

            //Act
            _cancelamentoDaMatricula.Cancelar(matricula.Id);

            //Assert
            Assert.True(matricula.Cancelada);
        }

        [Fact]
        public void DeveNotificarQuandoMatriculaNaoEncontrada()
        {
            //Arrange
            Matricula? matriculaInvalida = null;
            const int matriculaIdInvalida = 1;
            _matriculaRepositorio.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns(matriculaInvalida);

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                    _cancelamentoDaMatricula.Cancelar(matriculaIdInvalida))
                .ComMensagem(Resource.MatriculaNaoEncontrada);
        }
    }
}
