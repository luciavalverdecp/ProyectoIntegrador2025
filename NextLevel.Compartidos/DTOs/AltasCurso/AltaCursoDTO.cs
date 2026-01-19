using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.AltasCurso
{
    public record AltaCursoDTO(CursoAltaDTO AltaDTO, List<Archivo> Archivos);
}
