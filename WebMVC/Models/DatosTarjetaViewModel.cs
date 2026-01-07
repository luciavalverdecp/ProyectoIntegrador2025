namespace WebMVC.Models
{
    public class DatosTarjetaViewModel
    {
        public string NumeroTarjeta { get; set; }
        public string NombreTitular { get; set; }
        public int MesVencimiento { get; set; }
        public int AnioVencimiento { get; set; }
        public string Cvv { get; set; }
    }
}
