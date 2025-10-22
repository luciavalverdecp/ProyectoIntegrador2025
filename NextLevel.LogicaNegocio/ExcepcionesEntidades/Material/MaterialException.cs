using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Material
{
    public class MaterialException : Exception
    {
        public MaterialException()
        {

        }
        public MaterialException(string? message) : base(message)
        {

        }
        public MaterialException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
