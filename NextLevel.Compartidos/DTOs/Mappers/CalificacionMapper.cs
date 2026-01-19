using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Calificaciones;
using NextLevel.Compartidos.DTOs.Pruebas;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class CalificacionMapper
    {
        public static CalificacionDTO ToCalificacionDTO(Calificacion calificacion)
        {
            return new CalificacionDTO(calificacion.Puntaje, EstudianteMapper.ToEstudianteEmailDTO(calificacion.Estudiante));
        }

        public static IEnumerable<CalificacionDTO> ToListCalificacionDTO(IEnumerable<Calificacion> calificaciones)
        {
            if (calificaciones == null || calificaciones.Count() == 0) return new List<CalificacionDTO>();
            return calificaciones.Select(c => new CalificacionDTO(c.Puntaje, EstudianteMapper.ToEstudianteEmailDTO(c.Estudiante))).ToList();
        }
    }
}
