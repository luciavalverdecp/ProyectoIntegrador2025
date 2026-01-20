using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Postulaciones;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Postulaciones
{
    public interface IObtenerPostulacion
    {
        PostulacionDTO Ejecutar(int id);
    }
}
