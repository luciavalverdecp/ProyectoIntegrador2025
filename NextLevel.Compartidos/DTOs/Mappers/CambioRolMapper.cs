using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.CambioRoles;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class CambioRolesMapper
    {
        public static CambioRolDTO ToCambioRolDTO(CambioRol cambioRol)
        {
            return new CambioRolDTO(
                EstudianteMapper.ToEstudianteEmailDTO(cambioRol.Estudiante),
                cambioRol.Archivos);
        }
        public static CambioRol FromCambioRolDTO(CambioRolDTO cambioRolDTO)
        {
            return new CambioRol()
            {
                Estudiante = EstudianteMapper.FromEstudianteEmailDTO(cambioRolDTO.Estudiante)
            };
        }
    }
}
