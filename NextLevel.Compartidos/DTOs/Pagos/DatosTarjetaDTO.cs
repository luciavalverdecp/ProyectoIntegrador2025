using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.Compartidos.DTOs.Pagos
{
    public record DatosTarjetaDTO(string Numero, string Titular, int MesVencimiento, int AnioVencimiento, string CVV);
}
