using NextLevel.AccesoDatos.EF;
using NextLevel.Compartidos.DTOs.Docentes;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.Docentes;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Docentes
{
    public class ModificarDocente : IModificarDocente
    {
        private readonly IRepositorioDocente _repositorioDocente;

        public ModificarDocente(IRepositorioDocente repositorioDocente)
        {
            _repositorioDocente = repositorioDocente;
        }

        public void Ejecutar(DocenteInfoDTO docente)
        {
            if (docente == null)
            {
                throw new UsuarioEmailException("No existe un usuario");
            }
            Docente doc = DocenteMapper.FromDocenteInfoDTO(docente);
            doc.ValidarDatosPersonales();
            _repositorioDocente.UpdateDatosPersonales(doc);
        }
    }
}
