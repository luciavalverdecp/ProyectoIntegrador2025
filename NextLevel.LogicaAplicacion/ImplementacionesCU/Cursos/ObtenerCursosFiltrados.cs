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
        public IEnumerable<CursoVistaPreviaDTO> Ejecutar(string filtro)
        {
            IEnumerable<Curso> cursos = _repositorioCurso.FindWithFilter(filtro);

            return CursoMapper.ToListaDTO(cursos);
        }
    }
}
