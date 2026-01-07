using NextLevel.Compartidos.DTOs.Pagos;
using NextLevel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class PagoMapper
    {
        public static PagoIdDTO ToPagoIdDto(Pago pago)
        {
            return new PagoIdDTO(pago.Id);
        }
    }
}
