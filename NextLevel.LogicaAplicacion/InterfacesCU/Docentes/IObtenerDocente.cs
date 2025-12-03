using NextLevel.Compartidos.DTOs.Docentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Docentes
{
    public interface IObtenerDocente
    {
        DocenteInfoDTO Ejecutar(string email);
    }
}
