using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.AltaCurso
{
    public class AltaCursoArchivosException : AltaCursoException
    {
        public AltaCursoArchivosException()
        {

        }
        public AltaCursoArchivosException(string? message) : base(message)
        {

        }
        public AltaCursoArchivosException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
