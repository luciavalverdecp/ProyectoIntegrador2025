using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Estudiantes
{
    public class ObtenerEstudiante : IObtenerEstudiante
    {
        private readonly IRepositorioEstudiante _repoitorioEstudiante;

        public ObtenerEstudiante(IRepositorioEstudiante repoitorioEstudiante)
        {
            _repoitorioEstudiante = repoitorioEstudiante;
        }

        public EstudianteEmailDTO EjecutarEstudianteEmailDTO(string email)
        {
            try
            {
                var estudiante = _repoitorioEstudiante.FindByEmail(email);
                EstudianteEmailDTO dto = EstudianteMapper.ToEstudianteEmailDTO(estudiante);
                return dto;
            }
            catch (Exception ex)
            {
                throw new EstudianteException("Error al obtener al usuario estudiante.");
            }
        }

        public EstudianteInfoDTO EjecutarEstudianteInfoDTO(string email)
        {
            var estudiante = _repoitorioEstudiante.FindByEmail(email);
            if(estudiante == null) throw new EstudianteNoEncontradoException("Error al obtener al usuario estudiante.");
            EstudianteInfoDTO dto = EstudianteMapper.ToEstudianteInfoDTO(estudiante);
            return dto;
        }
    }
}
