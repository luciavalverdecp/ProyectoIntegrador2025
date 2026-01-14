using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class CursoMapper
    {
        public static CursoVistaPreviaDTO ToCursoVistaPreviaDTO(Curso curso)
        {
            return new CursoVistaPreviaDTO(
                curso.Id,
                curso.Nombre,
                DocenteMapper.ToDocenteNombreDTO(curso.Docente),
                curso.Imagen,
                curso.Calificacion,
                curso.Descripcion);
        }
        public static Curso FromCursoVistaPreviaDTO(CursoVistaPreviaDTO cursoVistaPreviaDTO)
        {
            return new Curso(DocenteMapper.FromDocenteNombreDTO(cursoVistaPreviaDTO.DocenteNombreDTO),
                cursoVistaPreviaDTO.Imagen,
                cursoVistaPreviaDTO.Descripcion);
        }

        public static CursoAltaDTO ToCursoAltaDTO(Curso curso)
        {
            return new CursoAltaDTO(curso.Id, 
                curso.Nombre, 
                DocenteMapper.ToDocenteNombreDTO(curso.Docente), 
                curso.Imagen, 
                curso.FechaInicio, 
                curso.FechaFin, 
                curso.Descripcion, 
                TemarioMapper.ToListaDTO(curso.Temarios), 
                curso.Precio, 
                curso.Dificultad);
        }

        public static Curso FromCursoAltaDTO(CursoAltaDTO cursoAltaDTO)
        {
            return new Curso(cursoAltaDTO.Nombre,
                DocenteMapper.FromDocenteNombreDTO(cursoAltaDTO.DocenteNombreDTO),
                cursoAltaDTO.Imagen,
                cursoAltaDTO.FechaInicio,
                cursoAltaDTO.FechaFin,
                cursoAltaDTO.Descripcion,
                TemarioMapper.FromListaDTO(cursoAltaDTO.Temarios),
                cursoAltaDTO.Precio,
                cursoAltaDTO.Dificultad);
        }

        public static IEnumerable<CursoVistaPreviaDTO> ToListaDTO(IEnumerable<Curso> cursos) 
        {
            return cursos.Select(curso => new CursoVistaPreviaDTO(
                curso.Id,
                curso.Nombre,
                DocenteMapper.ToDocenteNombreDTO(curso.Docente),
                curso.Imagen,
                curso.Calificacion,
                curso.Descripcion));
        }

        public static IEnumerable<CursoNombreDTO> ToListCursoNombreDTO(List<Curso> cursos)
        {
            return cursos.Select(curso => new CursoNombreDTO(curso.Nombre));
        }
        public static IEnumerable<Curso> FromListCursoNombreDTO(IEnumerable<CursoNombreDTO> cursos) 
        {
            if (cursos == null || cursos.Count() == 0)
            {
                return new List<Curso>();
            } 
                
            return cursos.Select(curso => new Curso()
            {
                Nombre = curso.Nombre
            });
        }

        public static CursoNombreDTO ToCursoNombreDTO(Curso curso)
        {
            return new CursoNombreDTO(curso.Nombre);
        }
        public static Curso FromCursoNombreDTO(CursoNombreDTO curso)
        {
            return new Curso()
            {
                Nombre = curso.Nombre
            };
        }
        public static CursoDTO ToCursoDTO(Curso curso)
        {
            return new CursoDTO(curso.Nombre,
                DocenteMapper.ToDocenteNombreDTO(curso.Docente),
                curso.Imagen,
                curso.Calificacion,
                curso.Descripcion,
                PruebaMapper.ToListPruebaDTO(curso.Pruebas),
                TemarioMapper.ToListaDTO(curso.Temarios),
                curso.FechasClases,
                EstudianteMapper.ToListEstudianteEmailDTO(curso.Estudiantes),
                curso.Duracion,
                ForoMapper.ToForoDTO(curso.Foro), 
                curso.Dificultad,
                curso.Precio,
                SemanaMapper.ToListSemanaNumeroDTO(curso.Semanas),
                curso.FechaInicio, 
                curso.FechaFin);
        }
        public static Curso FromCursoDTO(CursoDTO cursoDTO)
        {
            return new Curso()
            {
                Nombre = cursoDTO.Nombre,
                Docente = DocenteMapper.FromDocenteNombreDTO(cursoDTO.DocenteNombreDTO),
                Calificacion = cursoDTO.Calificacion,
                Descripcion = cursoDTO.Descripcion,
                Pruebas = PruebaMapper.FromListPruebaDTO(cursoDTO.Pruebas).ToList(),
                Temarios = TemarioMapper.FromListaDTO(cursoDTO.Temarios).ToList(),
                FechasClases = cursoDTO.FechasClases,
                Estudiantes = EstudianteMapper.FromListEstudianteEmailDTO(cursoDTO.Estudiantes).ToList(),
                Duracion = cursoDTO.Duracion,
                Foro = ForoMapper.FromForoDTO(cursoDTO.Foro),
                Dificultad = cursoDTO.Dificultad,
                Precio = cursoDTO.Precio,
                Semanas = SemanaMapper.FromListSemanaNumeroDTO(cursoDTO.Semanas).ToList(),
                FechaInicio = cursoDTO.FechaInico,
                FechaFin = cursoDTO.FechaFin,
            };
        }
    }

}
