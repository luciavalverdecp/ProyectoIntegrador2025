using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Estudiantes
{
    public class ObtenerMisCursos : IObtenerMisCursos
    {
        private readonly IRepositorioEstudiante _repositorioEstudiante;
        public ObtenerMisCursos(IRepositorioEstudiante repositorioEstudiante)
        {
            _repositorioEstudiante = repositorioEstudiante;
        }


        public IEnumerable<CursoVistaPreviaDTO> Ejecutar(string email)
        {
            var estudiante = _repositorioEstudiante.FindByEmail(email);
            if (estudiante.Cursos == null) return new List<CursoVistaPreviaDTO>();
            return CursoMapper.ToListaDTO(estudiante.Cursos);
        }
    }
}
