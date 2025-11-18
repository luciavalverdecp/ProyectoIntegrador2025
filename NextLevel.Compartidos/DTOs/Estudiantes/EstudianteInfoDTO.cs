using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.CambioRoles;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Estudiantes
{
    public record EstudianteInfoDTO(string Email, string Password, string NombreCompleto, string Cedula, IEnumerable<CursoNombreDTO> Cursos, string Telefono);
}
