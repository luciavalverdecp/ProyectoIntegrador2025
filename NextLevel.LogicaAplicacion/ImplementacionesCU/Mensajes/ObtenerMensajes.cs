using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.AccesoDatos.EF;
using NextLevel.Compartidos.DTOs.Conversaciones;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.Compartidos.DTOs.Mensajes;
using NextLevel.LogicaAplicacion.InterfacesCU.Mensajes;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Mensajes
{
    public class ObtenerMensajes : IObtenerMensajes
    {
        private readonly IRepositorioMensaje repositorioMensaje;
        public ObtenerMensajes(IRepositorioMensaje repositorioMensaje)
        {
            this.repositorioMensaje = repositorioMensaje;
        }

        public IEnumerable<MensajeDTO> Ejecutar(ConversacionDTO conversacion)
        {
            if (conversacion == null) throw new Exception("No se pudo obtener la conversacion");
            var mensajes = repositorioMensaje.GetByConversacion(ConversacionMapper.FromConversacionDTO(conversacion));
            return MensajeMapper.ToListMensajesDTO(mensajes);
        }
    }
}
