using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.AltasCurso;
using NextLevel.Compartidos.DTOs.CambioRoles;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Postulaciones
{
    public record PostulacionDTO(int Id, CambioRolDTO? CambioRolDTO, AltaCursoDTO? CursoAltaDTO, string Estado);
}
