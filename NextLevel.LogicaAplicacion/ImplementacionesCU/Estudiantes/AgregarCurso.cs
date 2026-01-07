using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Estudiantes
{
    public class AgregarCurso : IAgregarCurso
    {
        private readonly IRepositorioEstudiante _repositorioEstudiante;
        private readonly IRepositorioCurso _repositorioCurso;

        public AgregarCurso(IRepositorioEstudiante repositorioEstudiante, IRepositorioCurso repositorioCurso)
        {
            _repositorioEstudiante = repositorioEstudiante;
            _repositorioCurso = repositorioCurso;
        }

        public void Ejecutar(string email, string cursoNombre)
        {
            Estudiante estudianteExistente = _repositorioEstudiante.FindByEmail(email);
            Curso curso = _repositorioCurso.FindByNombre(cursoNombre);

            if (estudianteExistente == null)
            {
                throw new EstudianteException("El estudiante ingresado no existe.");
            }
            if (curso == null)
            {
                throw new CursoException("El curso ingresado no existe.");
            }

            estudianteExistente.Cursos.Add(curso);
            _repositorioEstudiante.Update(estudianteExistente);
        }
    }
}
