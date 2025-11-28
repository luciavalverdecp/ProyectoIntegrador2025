using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.AltaCurso
{
    public class AltaCursoException : Exception
    {
        public AltaCursoException()
        {

        }
        public AltaCursoException(string? message) : base(message)
        {

        }
        public AltaCursoException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
