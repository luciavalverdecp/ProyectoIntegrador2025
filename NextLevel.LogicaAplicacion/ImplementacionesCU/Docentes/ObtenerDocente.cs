using NextLevel.Compartidos.DTOs.Docentes;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.Docentes;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Docente;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Docentes
{
    public class ObtenerDocente : IObtenerDocente
    {
        private readonly IRepositorioDocente _repositorioDocente;
        public ObtenerDocente(IRepositorioDocente repositorioDocente)
        {
            _repositorioDocente = repositorioDocente;
        }
        public DocenteInfoDTO Ejecutar(string email)
        {
            var docente = (Docente)_repositorioDocente.FindByEmail(email);
            if (docente == null) throw new DocenteNoEncontradoException("Error al obtener al usuario docente.");
            DocenteInfoDTO dto = DocenteMapper.ToDocenteInfoDTO(docente);
            return dto;
        }
    }
}
