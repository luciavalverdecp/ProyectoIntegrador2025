using NextLevel.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class AltaCurso : IEntity, IComparable<AltaCurso>
    {
        public int Id { get; set; }
        public Curso Curso { get; set; }
        public List<Archivo> Archivos { get; set; }
        public AltaCurso() { }
        public AltaCurso(Curso curso, List<Archivo> archivos)
        {
            Curso = curso;
            Archivos = archivos;
        }
        public int CompareTo(AltaCurso? other)
        {
            throw new NotImplementedException();
        }
    }
}
