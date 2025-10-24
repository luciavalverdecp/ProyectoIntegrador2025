using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class EstudianteMapper
    {
        public static EstudianteRegistroDTO ToEstudianteRegistroDTO(Estudiante estudiante)
        {
            return new EstudianteRegistroDTO(
                estudiante.Email, 
                estudiante.Password, 
                estudiante.NombreCompleto, 
                estudiante.Telefono, 
                estudiante.Cedula);
        }
        public static Estudiante FromEstudianteRegistroDTO(EstudianteRegistroDTO estudianteRegistroDTO)
        {
            return new Estudiante()
            {
                Email = estudianteRegistroDTO.Email,
                Password = estudianteRegistroDTO.Password,
                NombreCompleto = estudianteRegistroDTO.NombreCompleto,
                Telefono = estudianteRegistroDTO.Telefono,
                Cedula = estudianteRegistroDTO.Cedula
            };
        }
    }
}
