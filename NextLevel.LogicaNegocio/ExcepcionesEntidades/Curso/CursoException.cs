using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso
{
    public class CursoException : Exception
    {
        public CursoException()
        {

        }
        public CursoException(string? message) : base(message)
        {

        }
        public CursoException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
