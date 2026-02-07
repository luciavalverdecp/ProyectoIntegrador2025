using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.InterfacesEntidades;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Temario:IEntity
    {
        public int Id { get; set; }
        public string Tema { get; set; }
        public Curso Curso { get; set; }
        public int CursoId { get; set; }
        public Temario() { }
        public Temario(string tema)
        {
            Tema = tema;
        }
        public Temario(string tema, Curso curso)
        {
            Tema = tema;
            Curso = curso;
        }
    }
}
