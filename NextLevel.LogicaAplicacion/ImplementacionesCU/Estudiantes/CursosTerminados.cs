using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Estudiantes
{
    public class CursosTerminados : ICursosTerminados
    {
        private readonly IRepositorioCurso _repositorioCurso;
        private readonly IRepositorioEstudiante _repositorioEstudiante;
        public CursosTerminados(IRepositorioCurso repositorioCurso, IRepositorioEstudiante repositorioEstudiante)
        {
            _repositorioCurso = repositorioCurso;
            _repositorioEstudiante = repositorioEstudiante;
        }

        public List<CursoNombreDTO> Ejecutar(EstudianteInfoDTO estudianteDTO)
        {
            try
            {
                List<CursoNombreDTO> resultado = new List<CursoNombreDTO>();
                var estudiante = _repositorioEstudiante.FindByEmail(estudianteDTO.Email);
                foreach(CursoNombreDTO c in estudianteDTO.Cursos)
                {
                    var curso = _repositorioCurso.FindByNombre(c.Nombre);
                    if (_repositorioEstudiante.TerminoCurso(curso, estudiante)) resultado.Add(c);
                }
                return resultado;
            }
            catch(Exception ex)
            {
                throw new EstudianteException("Error al obtener el usuario. TerminoCurso");
            }
        }
    }
}
