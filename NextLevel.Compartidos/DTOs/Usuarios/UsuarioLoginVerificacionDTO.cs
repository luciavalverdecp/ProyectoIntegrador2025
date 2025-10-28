using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Usuarios
{
    public record UsuarioLoginVerificacionDTO(string Email, string Password, bool EstaVerificado, Rol Rol);
}
