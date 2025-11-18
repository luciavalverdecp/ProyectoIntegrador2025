using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Cursos
{
    public class ObtenerCursosFiltrados : IObtenerCursosFiltrados
    {
        private readonly IRepositorioCurso _repositorioCurso;

        public ObtenerCursosFiltrados(IRepositorioCurso repositorioCurso)
        {
            _repositorioCurso = repositorioCurso;
        }
        public IEnumerable<CursoVistaPreviaDTO> Ejecutar(string? filtro, string? opcionMenu, string? alfabetico, int? calificacion, string? docente)
        {
            IEnumerable<Curso> cursos = _repositorioCurso.FindAll();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                cursos = _repositorioCurso.FindWithFilter(filtro, cursos);
            }
            if (!string.IsNullOrWhiteSpace(opcionMenu))
            {
                cursos = _repositorioCurso.FindWithCategory(opcionMenu, alfabetico, calificacion, docente, cursos);
            }
                
            return CursoMapper.ToListaDTO(cursos);
        }
    }
}
