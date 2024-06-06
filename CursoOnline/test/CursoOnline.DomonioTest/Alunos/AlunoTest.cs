using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DomonioTest._Builders;
using CursoOnline.DomonioTest._Util;

namespace CursoOnline.DomonioTest.Alunos
{
    public class AlunoTest
    {
        private readonly string _nome;
        private readonly string _cpf;
        private readonly string _email;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly Faker _faker;

        public AlunoTest()
        {
            _faker = new Faker();
            _nome = _faker.Person.FullName;
            _cpf = _faker.Person.Cpf();
            _email = _faker.Person.Email;
            _publicoAlvo = PublicoAlvo.Estudante;
        }

        [Fact]
        public void DeveCriarAluno()
        {
            //Arrange
            var alunoEsperado = new
            {
                Nome = _nome,
                Cpf = _cpf,
                Email = _email,
                PublicoAlvo = _publicoAlvo
            };

            //Act
            var aluno = new Aluno(alunoEsperado.Nome, alunoEsperado.Cpf, alunoEsperado.Email, alunoEsperado.PublicoAlvo);

            //Assert
            alunoEsperado.ToExpectedObject().ShouldMatch(aluno);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NaoDeveAlunoTerNomeInvalido(string nome)
        {
            // Arrange           
            // Act
            // Assert
            Assert.Throws<ExcecaoDeDominio>(() => 
                AlunoBuilder.Novo().ComNome(nome).Build())
                .ComMensagem(Resource.NomeInvalido);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("Cpf inválido")]
        [InlineData("00000000000")]
        public void NaoDeveAlunoTerCpfInvalido(string cpf)
        {
            // Arrange           
            // Act
            // Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                AlunoBuilder.Novo().ComCpf(cpf).Build())
                .ComMensagem(Resource.CpfInvalido);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("email invalido")]
        [InlineData("email@invalido")]
        public void NaoDeveAlunoTerEmailInvalido(string email)
        {
            // Arrange           
            // Act
            // Assert
            Assert.Throws<ExcecaoDeDominio>(() =>
                AlunoBuilder.Novo().ComEmail(email).Build())
                .ComMensagem(Resource.EmailInvalido);
        }

        [Fact]
        public void DeveAlterarNome()
        {
            // Arrange
            var nomeEsperado = _nome;
            var aluno = AlunoBuilder.Novo().Build();

            // Act
            aluno.AlterarNome(nomeEsperado);

            // Assert
            Assert.Equal(nomeEsperado, aluno.Nome);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NaoDeveAlterarComNomeInvalido(string nome)
        {
            // Arrange           
            var aluno = AlunoBuilder.Novo().Build();

            // Act
            // Assert
            Assert.Throws<ExcecaoDeDominio>(() => aluno.AlterarNome(nome))
                .ComMensagem(Resource.NomeInvalido);
        }
    }
}
