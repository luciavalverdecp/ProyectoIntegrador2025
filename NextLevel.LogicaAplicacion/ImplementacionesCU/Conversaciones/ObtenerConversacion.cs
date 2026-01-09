using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Conversaciones;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.Conversaciones;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Conversaciones
{
    public class ObtenerConversacion : IObtenerConversacion
    {
        private readonly IRepositorioConversacion repositorioConversacion;
        public ObtenerConversacion(IRepositorioConversacion repositorioConversacion)
        {
            this.repositorioConversacion = repositorioConversacion;
        }

        public ConversacionDTO Ejecutar(int conversacionId)
        {
            if (conversacionId == null) throw new Exception("No se obtuvo la conversacion");
            return ConversacionMapper.ToConversacionDTO(repositorioConversacion.FindById(conversacionId));
        }
    }
}
