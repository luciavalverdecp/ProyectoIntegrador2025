using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso
{
    public class CursoPrecioException : CursoException
    {
        public CursoPrecioException()
        {

        }
        public CursoPrecioException(string? message) : base(message)
        {

        }
        public CursoPrecioException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
