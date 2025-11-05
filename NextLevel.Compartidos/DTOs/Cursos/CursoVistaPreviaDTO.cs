using NextLevel.Compartidos.DTOs.Docentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.Compartidos.DTOs.Cursos
{
    public record CursoVistaPreviaDTO(int Id, string Nombre, DocenteNombreDTO DocenteNombreDTO, string RutaArchivo, double Calificacion, string Descripcion);
}
