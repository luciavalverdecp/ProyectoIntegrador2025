using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario
{
    public class UsuarioEstaVerificadoException : UsuarioException
    {
        public UsuarioEstaVerificadoException()
        {

        }
        public UsuarioEstaVerificadoException(string? message) : base(message)
        {

        }
        public UsuarioEstaVerificadoException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
