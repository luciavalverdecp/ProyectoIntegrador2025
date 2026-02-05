using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Estudiantes
{
    public class ModificarEstudiante : IModificarEstudiante
    {
        private readonly IRepositorioEstudiante _repositorioEstudiante;

        public ModificarEstudiante(IRepositorioEstudiante repositorioEstudiante)
        {
            _repositorioEstudiante = repositorioEstudiante;
        }

        public void Ejecutar(EstudianteInfoDTO estudiante)
        {
            if (estudiante == null)
            {
                throw new UsuarioEmailException("No existe un usuario");
            }
            Estudiante est = EstudianteMapper.FromEstudianteInfoDTO(estudiante);
            Estudiante estudianteBD = _repositorioEstudiante.FindByEmail(estudiante.Email);
            est.Id = estudianteBD.Id;
            est.ValidarDatosPersonales();
            _repositorioEstudiante.Update(est);
        }
    }
}
