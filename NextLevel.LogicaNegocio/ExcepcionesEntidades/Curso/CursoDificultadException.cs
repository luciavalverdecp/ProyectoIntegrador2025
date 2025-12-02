using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso
{
    public class CursoDificultadException : CursoException
    {
        public CursoDificultadException()
        {

        }
        public CursoDificultadException(string? message) : base(message)
        {

        }
        public CursoDificultadException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
