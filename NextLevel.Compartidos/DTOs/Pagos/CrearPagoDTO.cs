using NextLevel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.Compartidos.DTOs.Pagos
{
    public record CrearPagoDTO(string UsuarioEmail, string CursoNombre, MetodoPago MetodoPago, DatosTarjetaDTO DatosTarjetaDTO);
}
