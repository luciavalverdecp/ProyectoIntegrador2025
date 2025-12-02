using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Cursos
{
    public class ObtenerCursosDocente : IObtenerCursosDocente
    {
        private readonly IRepositorioCurso _repositorioCurso;
        private readonly IRepositorioDocente _repositorioDocente;

        public ObtenerCursosDocente(IRepositorioCurso repositorioCurso, IRepositorioDocente repositorioDocente)
        {
            _repositorioCurso = repositorioCurso;
            _repositorioDocente = repositorioDocente;
        }

        public IEnumerable<CursoVistaPreviaDTO> Ejecutar(string email)
        {
            var usuario = _repositorioDocente.FindByEmail(email);
            IEnumerable<Curso> cursos = _repositorioCurso.GetByDocente(usuario);
            return CursoMapper.ToListaDTO(cursos);
        }
    }
}
