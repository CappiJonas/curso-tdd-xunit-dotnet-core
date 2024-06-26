﻿using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas
{
    public class Matricula : Entidade
    {
        public Aluno? Aluno { get; }
        public Curso? Curso { get; }
        public double ValorPago { get; }
        public bool TemDesconto { get; }
        public double NotaDoAluno { get; private set; }
        public bool CursoConcluido { get; private set; }
        public bool Cancelada { get; private set; }

        public Matricula(Aluno? aluno, Curso? curso, double valorPago)
        {
            ValidadorDeRegra.Novo()
                .Quando(aluno == null, Resource.AlunoInvalido)
                .Quando(curso == null, Resource.CursoInvalido)
                .Quando(valorPago < 1, Resource.ValorPagoInvalido)
                .Quando(valorPago > curso?.Valor, Resource.ValorPagoMaiorQueValorDoCurso)
                .Quando(aluno?.PublicoAlvo != curso?.PublicoAlvo, Resource.PublicoAlvoDiferentes)
                .DispararExcecaoSeExistir();

            Aluno = aluno;
            Curso = curso;
            ValorPago = valorPago;
            TemDesconto = ValorPago < Curso!.Valor;
        }

        public void InformarNota(double notaDoAluno)
        {
            ValidadorDeRegra.Novo()
                .Quando(notaDoAluno < 0 || notaDoAluno > 10, Resource.NotaDoAlunoInvalida)
                .Quando(Cancelada, Resource.MatriculaCancelada)
                .DispararExcecaoSeExistir();

            NotaDoAluno = notaDoAluno;
            CursoConcluido = true;
        }

        public void Cancelar()
        {
            ValidadorDeRegra.Novo()
                .Quando(CursoConcluido, Resource.MatriculaConcluida)
                .DispararExcecaoSeExistir();

            Cancelada = true;
        }
    }
}
