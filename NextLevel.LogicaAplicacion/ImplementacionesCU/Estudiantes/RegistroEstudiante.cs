using NextLevel.AccesoDatos.EF;
using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiante;
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
    public class RegistroEstudiante : IRegistroEstudiante
    {
        private readonly IRepositorioEstudiante _repositorioEstudiante;
        private readonly IRepositorioUsuario _repositorioUsuario;
        public RegistroEstudiante(IRepositorioEstudiante repositorioEstudiante, IRepositorioUsuario repositorioUsuario)
        {
            _repositorioEstudiante = repositorioEstudiante;
            _repositorioUsuario = repositorioUsuario;
        }
        public void Ejecutar(EstudianteRegistroDTO estudianteDTO)
        {
            Usuario usuExistente = _repositorioUsuario.FindByEmail(estudianteDTO.Email);
            if (usuExistente == null && estudianteDTO.Password.Length >= 8)
            {
                Estudiante estudiante = EstudianteMapper.FromEstudianteRegistroDTO(estudianteDTO);
                _repositorioEstudiante.Add(estudiante);
            }
            else if (estudianteDTO.Password.Length < 8)
            {
                throw new UsuarioPasswordException("La contraseña debe contener al menos 8 caracteres.");
            }
            else
            {
                throw new UsuarioEmailException("El usuario ya existe.");
            }
        }
    }
}
