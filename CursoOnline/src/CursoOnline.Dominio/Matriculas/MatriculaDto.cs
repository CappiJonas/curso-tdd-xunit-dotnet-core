using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas
{
    public class MatriculaDto
    {
        public int AlunoId { get; set; }
        public int CursoId { get; set; }
        public double ValorPago { get; set; }
    }
}
