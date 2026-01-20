using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Postulaciones
{
    public interface IResolverPostulacion
    {
        void Ejecutar(int id, string motivo, string resolucion);
    }
}
