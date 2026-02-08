using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Postulacion
{
    public class PostulacionException : Exception
    {
        public PostulacionException()
        {

        }
        public PostulacionException(string? message) : base(message)
        {

        }
        public PostulacionException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
