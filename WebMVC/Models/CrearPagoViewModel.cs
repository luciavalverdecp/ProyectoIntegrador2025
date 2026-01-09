using NextLevel.LogicaNegocio.Entidades;

namespace WebMVC.Models
{
    public class CrearPagoViewModel
    {
        public string CursoNombre { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public DatosTarjetaViewModel Tarjeta { get; set; }
    }
}
