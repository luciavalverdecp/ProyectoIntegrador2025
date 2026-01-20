using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.Compartidos.DTOs.Postulaciones;
using NextLevel.LogicaAplicacion.InterfacesCU.Postulaciones;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using Olimpiadas.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Postulaciones
{
    public class ObtenerPostulacion : IObtenerPostulacion
    {
        private readonly IRepositorioPostulacion _repositorioPostulacion;
        public ObtenerPostulacion(IRepositorioPostulacion repositorioPostulacion)
        {
            _repositorioPostulacion = repositorioPostulacion;
        }

        public PostulacionDTO Ejecutar(int id)
        {
            //TODO validacion de id?
            var postulacion = _repositorioPostulacion.FindById(id);
            return PostulacionMapper.ToPostulacionDTO(postulacion);
        }
    }
}
