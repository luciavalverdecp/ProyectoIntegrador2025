using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.InterfacesCU.IA
{
    public interface IConsultarConIA
    {
        Task<string> Ejecutar(string mensaje);
    }
}
