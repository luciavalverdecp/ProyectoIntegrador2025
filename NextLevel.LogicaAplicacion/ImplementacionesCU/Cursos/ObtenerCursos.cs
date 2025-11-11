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
    public class ObtenerCursos : IObtenerCursos
    {
        private readonly IRepositorioCurso _repositorioCurso;

        public ObtenerCursos(IRepositorioCurso repositorioCurso)
        {
            _repositorioCurso = repositorioCurso;
        }

        public IEnumerable<CursoVistaPreviaDTO> Ejecutar()
        {
            IEnumerable<Curso> cursos = _repositorioCurso.FindAll();

            return CursoMapper.ToListaDTO(cursos);
        }
    }
}
