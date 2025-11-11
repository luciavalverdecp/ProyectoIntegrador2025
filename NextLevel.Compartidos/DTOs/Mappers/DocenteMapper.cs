using NextLevel.Compartidos.DTOs.Docentes;
using NextLevel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class DocenteMapper
    {
        public static DocenteNombreDTO ToDocenteNombreDTO(Docente docente)
        {
            return new DocenteNombreDTO(
                docente.NombreCompleto,
                docente.NroDocente);
        }
        public static Docente FromDocenteNombreDTO(DocenteNombreDTO docenteNombreDTO)
        {
            return new Docente()
            {
                NombreCompleto = docenteNombreDTO.NombreCompleto,
                NroDocente = docenteNombreDTO.NroDocente
            };
        }
    }

}
