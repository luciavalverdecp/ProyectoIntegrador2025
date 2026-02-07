using Microsoft.IdentityModel.Tokens;
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
        private readonly IRepositorioEstudiante _repositorioEstudiante;

        public ObtenerCursosFiltrados(IRepositorioCurso repositorioCurso,
            IRepositorioEstudiante repositorioEstudiante)
        {
            _repositorioCurso = repositorioCurso;
            _repositorioEstudiante = repositorioEstudiante;
        }
        public IEnumerable<CursoVistaPreviaDTO> Ejecutar(string? filtro, string? opcionMenu, string? alfabetico, int? calificacion, string? docente, string? email)
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
            if (!string.IsNullOrWhiteSpace(email))
            {
                Estudiante est = _repositorioEstudiante.FindByEmail(email);
                if ( est != null)
                {
                    cursos = _repositorioCurso.FindWithStudent(est, cursos);
                }
            }
                
            return CursoMapper.ToListaDTO(cursos);
        }
    }
}
