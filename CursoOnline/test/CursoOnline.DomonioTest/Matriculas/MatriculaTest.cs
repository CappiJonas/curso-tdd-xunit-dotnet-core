using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DomonioTest._Builders;
using CursoOnline.DomonioTest._Util;

namespace CursoOnline.DomonioTest.Matriculas
{
    public class MatriculaTest
    {
        [Fact]
        public void DeveCriarMatricula()
        {
            //Arrange
            var curso = CursoBuilder.Novo().Build();
            var matriculaEsperada = new
            {
                Aluno = AlunoBuilder.Novo().Build(),
                Curso = curso,
                ValorPago = curso.Valor
            };

            //Act
            var matricula = new Matricula(matriculaEsperada.Aluno, matriculaEsperada.Curso, matriculaEsperada.ValorPago);

            //Assert
            matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemAluno()
        {
            //Arrange
            Aluno? alunoInvalido = null;

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComAluno(alunoInvalido).Build())
                .ComMensagem(Resource.AlunoInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemCurso()
        {
            //Arrange
            Curso? cursoInvalido = null;

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComCurso(cursoInvalido).Build())
                .ComMensagem(Resource.CursoInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorPagoInvalido()
        {
            //Arrange
            const double valorPagoInvalido = 0;

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComValorPago(valorPagoInvalido).Build())
                .ComMensagem(Resource.ValorPagoInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorPagoMaiorQueValorDoCurso()
        {
            //Arrange
            var curso = CursoBuilder.Novo().ComValor(100).Build();
            var valorPagoMaiorQueCurso = curso.Valor + 1;

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(valorPagoMaiorQueCurso).Build())
                .ComMensagem(Resource.ValorPagoMaiorQueValorDoCurso);
        }

        [Fact]
        public void DeveIndicarQueHouveDescontoNaMatricula()
        {
            //Arrange
            var curso = CursoBuilder.Novo().ComValor(100).Build();
            var valorPagoComDesconto = curso.Valor - 1;

            //Act
            var matricula = MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(valorPagoComDesconto).Build();

            //Assert
            Assert.True(matricula.TemDesconto);
        }

        [Fact]
        public void NaoDevePublicoAlvoDeAlunoECursoSeremDiferentes()
        {
            //Arrange
            var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empregado).Build();
            var aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Universitario).Build();

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComAluno(aluno).ComCurso(curso).Build())
                .ComMensagem(Resource.PublicoAlvoDiferentes);
        }

        [Fact]
        public void DeveInformarANotaDoAlunoParaMatricula()
        {
            //Arrange
            const double notaDoAlunoEsperada = 9.5;
            var matricula = MatriculaBuilder.Novo().Build();

            //Act
            matricula.InformarNota(notaDoAlunoEsperada);

            //Assert
            Assert.Equal(notaDoAlunoEsperada, matricula.NotaDoAluno);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        public void NãoDeveInformarNotaComUmaNotaInvalida(double notaDoAlunoInvalida)
        {
            //Arrange
            var matricula = MatriculaBuilder.Novo().Build();

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                matricula.InformarNota(notaDoAlunoInvalida))
                .ComMensagem(Resource.NotaDoAlunoInvalida);
        }

        [Fact]
        public void DeveIndicarQueCursoFoiConcluido()
        {
            //Arrange
            const double notaDoAlunoEsperada = 9.5;
            var matricula = MatriculaBuilder.Novo().Build();

            //Act
            matricula.InformarNota(notaDoAlunoEsperada);

            //Assert
            Assert.True(matricula.CursoConcluido);
        }

        [Fact]
        public void DeveCancelarMatricula()
        {
            //Arrange
            var matricula = MatriculaBuilder.Novo().Build();

            //Act
            matricula.Cancelar();

            //Assert
            Assert.True(matricula.Cancelada);
        }

        [Fact]
        public void NaoDeveInformarNotaQuandoMatriculaEstiverCancelada()
        {
            //Arrange
            const double notaDoAluno = 3;
            var matricula = MatriculaBuilder.Novo().ComCancelada(true).Build();

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                    matricula.InformarNota(notaDoAluno))
                .ComMensagem(Resource.MatriculaCancelada);
        }

        [Fact]
        public void NaoDeveCancelarQuandoMatriculaEstiverConcluida()
        {
            //Arrange
            var matricula = MatriculaBuilder.Novo().ComConcluido(true).Build();

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                    matricula.Cancelar())
                .ComMensagem(Resource.MatriculaConcluida);
        }
    }
}
