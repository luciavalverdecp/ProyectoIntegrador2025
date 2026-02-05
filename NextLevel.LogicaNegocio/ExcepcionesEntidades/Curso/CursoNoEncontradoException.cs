using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso
{
    public class CursoNoEncontradoException : CursoException
    {
        public CursoNoEncontradoException()
        {

        }
        public CursoNoEncontradoException(string? message) : base(message)
        {

        }
        public CursoNoEncontradoException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
