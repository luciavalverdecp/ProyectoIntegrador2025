using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Docentes;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Cursos
{
    public record CursoDTO(string Nombre, DocenteNombreDTO DocenteNombreDTO, string RutaArchivo, double Calificacion, string Descripcion, IEnumerable<Prueba> Pruebas, IEnumerable<Temario> Temarios, List<DateTime> FechasClases, IEnumerable<Estudiante> Estudiantes, int Duracion, Foro Foro, Dificultad Dificultad, double Precio, IEnumerable<Semana> Semanas);
}
