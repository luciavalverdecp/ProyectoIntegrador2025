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
                curso.RutaArchivo,
                curso.Calificacion,
                curso.Descripcion);
        }
        public static Curso FromCursoVistaPreviaDTO(CursoVistaPreviaDTO cursoVistaPreviaDTO)
        {
            return new Curso(DocenteMapper.FromDocenteNombreDTO(cursoVistaPreviaDTO.DocenteNombreDTO),
                cursoVistaPreviaDTO.RutaArchivo,
                cursoVistaPreviaDTO.Descripcion);
        }

        public static IEnumerable<CursoVistaPreviaDTO> ToListaDTO(IEnumerable<Curso> cursos) 
        {
            return cursos.Select(curso => new CursoVistaPreviaDTO(
                curso.Id,
                curso.Nombre,
                DocenteMapper.ToDocenteNombreDTO(curso.Docente),
                curso.RutaArchivo,
                curso.Calificacion,
                curso.Descripcion));
        }

        public static IEnumerable<CursoNombreDTO> ToListCursoNombreDTO(List<Curso> cursos)
        {
            return cursos.Select(curso => new CursoNombreDTO(curso.Nombre));
        }

        
    }

}
