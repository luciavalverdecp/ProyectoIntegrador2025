using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.Compartidos.DTOs.Estudiantes
{
    public record EstudianteRegistroDTO(string Email, string Password, string NombreCompleto, string Telefono, string Cedula);
}
