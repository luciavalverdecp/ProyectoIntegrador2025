using NextLevel.LogicaNegocio.InterfacesEntidades;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Semana : IEntity
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public DateTime FechaInicio { get; set; }

        public List<Material> Materiales { get; set; } = new();

        public Semana() { }
        public Semana(int numero)
        {
            Numero = numero;
            FechaInicio = DateTime.Now;
            Materiales = new List<Material>();
        }
    }
}
