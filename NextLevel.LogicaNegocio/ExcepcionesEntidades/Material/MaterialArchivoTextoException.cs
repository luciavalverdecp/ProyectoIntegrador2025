using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Material
{
    public class MaterialArchivoTextoException : MaterialException
    {
        public MaterialArchivoTextoException()
        {

        }
        public MaterialArchivoTextoException(string? message) : base(message)
        {

        }
        public MaterialArchivoTextoException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
