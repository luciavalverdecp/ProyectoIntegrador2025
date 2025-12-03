using System;
﻿using NextLevel.Compartidos.DTOs.Usuarios;
using NextLevel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Usuarios;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class UsuarioMapper
    {
        public static UsuarioLoginVerificacionDTO ToUsuarioLoginVerificacion(Usuario u)
        {
            return new UsuarioLoginVerificacionDTO(u.Email, u.Password, u.EstaVerificado, u.Rol);
        }

        public static UsuarioEmailDTO ToUsuarioEmailDTO(Usuario usuario)
        {
            return new UsuarioEmailDTO(usuario.Email);
        }
    }
}
