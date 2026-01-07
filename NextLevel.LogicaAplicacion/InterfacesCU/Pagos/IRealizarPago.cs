using NextLevel.Compartidos.DTOs.Pagos;
using NextLevel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Pagos
{
    public interface IRealizarPago
    {
        //PagoIdDTO CrearPago(string usuarioEmail, string cursoNombre, MetodoPago metodoPago);
        PagoIdDTO ProcesarPagoSandbox(CrearPagoDTO crearPagoDTO);
    }
}
