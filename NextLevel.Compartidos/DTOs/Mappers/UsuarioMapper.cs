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
using NextLevel.Compartidos.DTOs.Estudiantes;

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

        public static UsuarioNombreEmailDTO ToUsuarioNombreEmailDTO(Usuario usuario)
        {
            return new UsuarioNombreEmailDTO(usuario.Email, usuario.NombreCompleto);
        }
      
        public static Usuario FromUsuarioEmailDTO(UsuarioEmailDTO usuarioEmailDTO, IEnumerable<EstudianteEmailDTO> estudiantesEmailDTO)
        {
            var usuario = estudiantesEmailDTO.Where(u => u.Email == usuarioEmailDTO.email).FirstOrDefault();
            if (usuario != null) return new Estudiante() { Email = usuarioEmailDTO.email };
            return new Docente() { Email = usuarioEmailDTO.email };
        }
    }
}
