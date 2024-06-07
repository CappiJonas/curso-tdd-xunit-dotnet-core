using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DomonioTest._Builders;
using CursoOnline.DomonioTest._Util;

namespace CursoOnline.DomonioTest.Matriculas
{
    public class CriacaoDaMatriculaTest
    {
        private readonly Mock<IAlunoRepositorio> _alunoRepositorio;
        private readonly Mock<ICursoRepositorio> _cursoRepositorio;
        private readonly Mock<IMatriculaRepositorio> _matriculaRepositorio;
        private readonly CriacaoDaMatricula _criacaoDaMatricula;
        private readonly MatriculaDto _matriculaDto;
        private readonly Aluno _aluno;
        private readonly Curso _curso;

        public CriacaoDaMatriculaTest()
        {
            _alunoRepositorio = new Mock<IAlunoRepositorio>();
            _cursoRepositorio = new Mock<ICursoRepositorio>();
            _matriculaRepositorio = new Mock<IMatriculaRepositorio>();

            _aluno = AlunoBuilder.Novo().ComId(23).Build();
            _alunoRepositorio.Setup(r => r.ObterPorId(_aluno.Id)).Returns(_aluno);
            _curso = CursoBuilder.Novo().ComId(45).Build();
            _cursoRepositorio.Setup(r => r.ObterPorId(_curso.Id)).Returns(_curso);
            _matriculaDto = new MatriculaDto { AlunoId = _aluno.Id, CursoId = _curso.Id, ValorPago = _curso.Valor };

            _criacaoDaMatricula = new CriacaoDaMatricula(_alunoRepositorio.Object, _cursoRepositorio.Object, _matriculaRepositorio.Object);
        }

        [Fact]
        public void DeveNotificarQuandoCursoNaoForEncontrado()
        {
            //Arrange
            Curso? cursoInvalido = null;          
            _cursoRepositorio.Setup(r => r.ObterPorId(_matriculaDto.CursoId)).Returns(cursoInvalido);
           
            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                _criacaoDaMatricula.Criar(_matriculaDto))
                .ComMensagem(Resource.CursoNaoEncontrado);
        }

        [Fact]
        public void DeveNotificarQuandoAlunoNaoForEncontrado()
        {
            //Arrange
            Aluno? alunoInvalido = null;
            _alunoRepositorio.Setup(r => r.ObterPorId(_matriculaDto.AlunoId)).Returns(alunoInvalido);

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                _criacaoDaMatricula.Criar(_matriculaDto))
                .ComMensagem(Resource.AlunoNaoEncontrado);
        }

        [Fact]
        public void DeveAdicionarMatricula()
        {
            //Arrange
            //Act
            _criacaoDaMatricula.Criar(_matriculaDto);

            //Assert
            _matriculaRepositorio.Verify(r => r.Adicionar(It.Is<Matricula>(m => m.Aluno == _aluno && m.Curso == _curso)));
        }
    }
}
