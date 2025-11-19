using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Prueba
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public IEnumerable<Calificacion> Calificaciones { get; set; }

        public Prueba(string nombre, DateTime fecha)
        {
            Nombre = nombre;
            Fecha = fecha;
            Calificaciones = new List<Calificacion>();
        }
    }
}
