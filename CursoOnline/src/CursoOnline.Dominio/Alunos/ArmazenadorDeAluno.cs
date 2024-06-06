using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.PublicosAlvo;

namespace CursoOnline.Dominio.Alunos
{
    public class ArmazenadorDeAluno
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IConversorDePublicoAlvo _conversorDePublicoAlvo;

        public ArmazenadorDeAluno(IAlunoRepositorio alunoRepositorio, IConversorDePublicoAlvo conversorDePublicoAlvo)
        {
            _alunoRepositorio = alunoRepositorio;
            _conversorDePublicoAlvo = conversorDePublicoAlvo;
        }

        public void Armazenar(AlunoDto alunoDto)
        {
            var alunoJaSalvo = _alunoRepositorio.ObterPeloCpf(alunoDto.Cpf);

            ValidadorDeRegra.Novo()
                .Quando(alunoJaSalvo != null && alunoJaSalvo.Id != alunoDto.Id, Resource.AlunoJaExiste)
                .DispararExcecaoSeExistir();

            var publicoAlvo = _conversorDePublicoAlvo.Converter(alunoDto.PublicoAlvo);

            var aluno = new Aluno(alunoDto.Nome, alunoDto.Cpf, alunoDto.Email, publicoAlvo);

            if (alunoDto.Id > 0)
            {
                aluno = _alunoRepositorio.ObterPorId(alunoDto.Id);
                aluno.AlterarNome(alunoDto.Nome);
            }

            if (alunoDto.Id == 0)
                _alunoRepositorio.Adicionar(aluno);
        }
    }
}
