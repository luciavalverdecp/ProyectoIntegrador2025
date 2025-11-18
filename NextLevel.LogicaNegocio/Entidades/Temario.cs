using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Temario
    {
        public int Id {  get; set; }
        public string Tema {  get; set; }
        public Curso Curso { get; set; }
        public int CursoId { get; set; }

        public Temario() { }

        public Temario(string tema, Curso curso)
        {
            Tema = tema;
            Curso = curso;
        }
    }
}
