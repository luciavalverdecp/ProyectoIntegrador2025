using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using NextLevel.LogicaNegocio.SistemaAutenticacion;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Usuarios
{
    public class RecuperarCuenta : IRecuperarCuenta
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public RecuperarCuenta(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

        public void Ejecutar(string email)
        {
            Usuario usu = _repositorioUsuario.FindByEmail(email);
            if(usu != null)
            {
                if (!usu.EstaVerificado) throw new UsuarioException("Se debe verificar la cuenta antes de poder realizar esta acción.");
                usu.RecuperarCuentaVencimiento = DateTime.UtcNow.AddMinutes(30);
                _repositorioUsuario.Update(usu);
                RecuperacionCuenta enviarRecuperacion = new RecuperacionCuenta();
                Task.Run(() => enviarRecuperacion.EnviarCorreoVerificacionAsync(email));
            }
            else
            {
                throw new UsuarioException("El email ingresado no pertenece a ninguna cuenta. Intente nuevamente.");
            }
        }
        public void ValidarVencimientoLink(string email)
        {
            var usuario = _repositorioUsuario.FindByEmail(email);
            if (usuario != null && 
                (usuario.RecuperarCuentaVencimiento == new DateTime() || 
                usuario.RecuperarCuentaVencimiento < DateTime.UtcNow))
                throw new UsuarioException("Link expirado");
        }

        public void CambiarContrasena(string email, string password)
        {
            var usuario = _repositorioUsuario.FindByEmail(email);
            if (usuario != null)
            {
                try
                {
                    usuario.Password = password;
                    usuario.Validar();
                    usuario.RecuperarCuentaVencimiento = new DateTime();
                    _repositorioUsuario.UpdateContrasena(usuario);
                }
                catch (UsuarioException ex)
                {
                    throw new UsuarioException(ex.Message);
                }
            }
        }
    }
}
