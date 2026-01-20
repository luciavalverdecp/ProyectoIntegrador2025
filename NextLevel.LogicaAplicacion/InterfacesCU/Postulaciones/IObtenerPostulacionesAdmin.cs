using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Postulaciones;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Postulaciones
{
    public interface IObtenerPostulacionesAdmin
    {
        IEnumerable<PostulacionDTO> Ejecutar(UsuarioEmailDTO admin);
    }
}
