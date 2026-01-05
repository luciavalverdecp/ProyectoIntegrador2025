using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Pruebas;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class PruebaMapper
    {
        public static IEnumerable<PruebaDTO> ToListPruebaDTO(IEnumerable<Prueba> pruebas)
        {
            if (pruebas == null || pruebas.Count() == 0) return new List<PruebaDTO>();
            return pruebas.Select(p => new PruebaDTO(p.Nombre, p.Fecha, CalificacionMapper.ToListCalificacionDTO(p.Calificaciones))).ToList();
        }
        public static IEnumerable<Prueba> FromListPruebaDTO(IEnumerable<PruebaDTO> pruebas)
        {
            if (pruebas == null || pruebas.Count() == 0) return new List<Prueba>();
            return pruebas.Select(p => new Prueba(p.Nombre, p.Fecha));
        }
    }
}
