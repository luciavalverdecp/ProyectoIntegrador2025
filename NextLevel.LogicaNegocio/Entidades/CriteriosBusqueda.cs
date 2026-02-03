using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class CriteriosBusqueda
    {
        public string? Categoria { get; set; }
        public string? Dificultad { get; set; }
        public int? DuracionMax { get; set; }
        public string? NombreDocente { get; set; }
        public DateTime? FechaInicio { get; set; }
        public double? Calificacion {  get; set; }
        public double? Precio {  get; set; }
    }
}
