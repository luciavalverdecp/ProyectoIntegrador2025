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
    }
}
