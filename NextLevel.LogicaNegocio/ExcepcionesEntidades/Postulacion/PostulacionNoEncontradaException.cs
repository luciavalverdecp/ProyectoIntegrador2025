using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Postulacion
{
    public class PostulacionNoEncontradaException : PostulacionException
    {
        public PostulacionNoEncontradaException()
        {

        }
        public PostulacionNoEncontradaException(string? message) : base(message)
        {

        }
        public PostulacionNoEncontradaException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
