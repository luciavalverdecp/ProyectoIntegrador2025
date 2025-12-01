using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Cursos
{
    public class ObtenerCurso : IObtenerCurso
    {
        private readonly IRepositorioCurso repositorioCurso;

        public ObtenerCurso(IRepositorioCurso repositorioCurso)
        {
            this.repositorioCurso = repositorioCurso;
        }

        public CursoDTO Ejecturar(string nombreCurso)
        {
            var cursoBuscado = repositorioCurso.FindByNombre(nombreCurso);
            if (cursoBuscado == null) throw new CursoException("Error al obtener el curso, intente nuevamente");
            
            return CursoMapper.ToCursoDTO(cursoBuscado);
        }
    }
}
