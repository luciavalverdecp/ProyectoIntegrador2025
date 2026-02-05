using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Material
{
    public class MaterialNoEncontradoException : MaterialException
    {
        public MaterialNoEncontradoException()
        {

        }
        public MaterialNoEncontradoException(string? message) : base(message)
        {

        }
        public MaterialNoEncontradoException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
