using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.CambioRoles
{
    public record CambioRolDTO(EstudianteEmailDTO Estudiante, List<string> NombresArchivos);
}
