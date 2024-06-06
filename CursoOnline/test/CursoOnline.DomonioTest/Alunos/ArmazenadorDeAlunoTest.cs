using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DomonioTest._Builders;
using CursoOnline.DomonioTest._Util;

namespace CursoOnline.DomonioTest.Alunos
{
    public class ArmazenadorDeAlunoTest
    {
        private readonly AlunoDto _alunoDto;
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorioMock;

        public ArmazenadorDeAlunoTest()
        {
            var faker = new Faker();
            _alunoDto = new AlunoDto()
            {
                Nome = faker.Person.FullName,
                Cpf = faker.Person.Cpf(),
                Email = faker.Person.Email,
                PublicoAlvo = "Estudante"
            };

            _alunoRepositorioMock = new Mock<IAlunoRepositorio>();
            var conversorDePublicoAlvoMock = new Mock<IConversorDePublicoAlvo>();
            _armazenadorDeAluno = new ArmazenadorDeAluno(_alunoRepositorioMock.Object, conversorDePublicoAlvoMock.Object);
        }

        [Fact]
        public void DeveAdicionarAluno()
        {
            //Arrange
            //Act
            _armazenadorDeAluno.Armazenar(_alunoDto);

            //Assert
            _alunoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Aluno>(
                    a => a.Nome == _alunoDto.Nome &&
                    a.Cpf == _alunoDto.Cpf &&
                    a.Email == _alunoDto.Email
                )
            ));
        }

        [Fact]
        public void NaoDeveAdicionarAlunoComMesmoCpfDeOutroJaSalvo()
        {
            //Arrange
            var alunoSalvo = AlunoBuilder.Novo().ComId(123).ComCpf(_alunoDto.Cpf).Build();
            _alunoRepositorioMock.Setup(r => r.ObterPeloCpf(_alunoDto.Cpf)).Returns(alunoSalvo);

            //Act
            //Assert
            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeAluno.Armazenar(_alunoDto))
                .ComMensagem(Resource.AlunoJaExiste);
        }

        [Fact]
        public void DeveAlterarNomeDoAluno()
        {
            //Arrange
            _alunoDto.Id = 123;
            var aluno = AlunoBuilder.Novo().Build();
            _alunoRepositorioMock.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(aluno);

            //Act
            _armazenadorDeAluno.Armazenar(_alunoDto);

            //Assert
            Assert.Equal(_alunoDto.Nome, aluno.Nome);
        }

        [Fact]
        public void NaoDeveEditarDemaisInformacoesDoAluno()
        {
            //Arrange
            var aluno = AlunoBuilder.Novo().Build();
            var cpfEsperado = aluno.Cpf;
            _alunoDto.Id = 123;
            _alunoRepositorioMock.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(aluno);

            //Act
            _armazenadorDeAluno.Armazenar(_alunoDto);

            //Assert
            Assert.Equal(cpfEsperado, aluno.Cpf);
        }

        [Fact]
        public void NaoDeveAdicionarNoRepositorioSeJaExiste()
        {
            //Arrange
            _alunoDto.Id = 123;
            var aluno = AlunoBuilder.Novo().Build();
            _alunoRepositorioMock.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(aluno);

            //Act
            _armazenadorDeAluno.Armazenar(_alunoDto);

            //Assert
            _alunoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Aluno>()), Times.Never);
        }
    }
}
