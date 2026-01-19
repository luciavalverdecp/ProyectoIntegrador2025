using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.Compartidos.DTOs.Postulaciones;
using NextLevel.LogicaAplicacion.InterfacesCU.Postulaciones;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Postulaciones
{
    public class ObtenerPostulacionesAdmin : IObtenerPostulacionesAdmin
    {
        private IRepositorioAdministrador _repositorioAdministrador;
        public ObtenerPostulacionesAdmin(IRepositorioAdministrador repositorioAdministrador)
        {
            _repositorioAdministrador = repositorioAdministrador;
        }
        public IEnumerable<PostulacionDTO> Ejecutar(UsuarioEmailDTO admin)
        {
            Administrador administrador = _repositorioAdministrador.FindByEmail(admin.email);
            return PostulacionMapper.ToListPostulacionDTO(administrador.Postulaciones);
        }
    }
}
