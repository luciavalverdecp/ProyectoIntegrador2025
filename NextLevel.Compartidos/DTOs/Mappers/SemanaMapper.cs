using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Semanas;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class SemanaMapper
    {
        public static SemanaNumeroDTO ToSemanaNumeroDTO(Semana semana)
        {
            return new SemanaNumeroDTO(semana.Numero);
        }

        public static IEnumerable<SemanaDTO> ToListSemanaNumeroDTO(IEnumerable<Semana> semanas)
        {
            if (semanas == null || semanas.Count() == 0) return new List<SemanaDTO>();
            return semanas.Select(s => new SemanaDTO(s.Numero, MaterialMapper.ToListMaterialBasicoDTO(s.Materiales), s.FechaInicio));
        }
        public static IEnumerable<Semana> FromListSemanaNumeroDTO(IEnumerable<SemanaDTO> semanas)
        {
            if (semanas == null || semanas.Count() == 0) return new List<Semana>();
            return semanas.Select(s => new Semana()
            {
                Numero = s.Numero,
                Materiales = MaterialMapper.FromListMaterialBasicoDTO(s.Materiales).ToList(),
                FechaInicio = s.FechaInicio
            });
        }
    }
}
