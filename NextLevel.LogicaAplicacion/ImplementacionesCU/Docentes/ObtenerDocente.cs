using NextLevel.AccesoDatos.EF;
using NextLevel.Compartidos.DTOs.Docentes;
using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.Docentes;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Docente;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Docentes
{
    public class ObtenerDocente : IObtenerDocente
    {
        private readonly IRepositorioDocente _repositorioDocente;
        public ObtenerDocente (IRepositorioDocente repositorioDocente)
        {
            _repositorioDocente = repositorioDocente;
        }
        public DocenteInfoDTO Ejecutar(string email)
        {
            try
            {
                var docente = (Docente)_repositorioDocente.FindByEmail(email);
                DocenteInfoDTO dto = DocenteMapper.ToDocenteInfoDTO(docente);
                return dto;
            }
            catch (DocenteException ex)
            {
                throw new DocenteException("Error al obtener al usuario docente.");
            }
        }
    }
}
