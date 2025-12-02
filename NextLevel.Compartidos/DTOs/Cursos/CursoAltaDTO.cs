using NextLevel.Compartidos.DTOs.Docentes;
using NextLevel.Compartidos.DTOs.Temarios;
using NextLevel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.Compartidos.DTOs.Cursos
{
    public record CursoAltaDTO(int Id, string Nombre, DocenteNombreDTO DocenteNombreDTO, string Imagen, DateTime FechaInicio, DateTime FechaFin, string Descripcion, IEnumerable<TemarioVistaPreviaDTO> Temarios, double Precio, Dificultad Dificultad);
}
