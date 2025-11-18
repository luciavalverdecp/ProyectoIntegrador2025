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
                estudiante.Cedula);
        }
        public static Estudiante FromEstudianteRegistroDTO(EstudianteRegistroDTO estudianteRegistroDTO)
        {
            return new Estudiante(estudianteRegistroDTO.Email, 
                estudianteRegistroDTO.Password, 
                estudianteRegistroDTO.NombreCompleto, 
                estudianteRegistroDTO.Cedula);
        }

        public static EstudianteInfoDTO ToEstudianteInfoDTO(Estudiante estudiante)
        {
            return new EstudianteInfoDTO(
                estudiante.Email,
                estudiante.Password,
                estudiante.NombreCompleto,
                estudiante.Cedula,
                CursoMapper.ToListCursoNombreDTO(estudiante.Cursos),
                estudiante.Telefono
                );
        }

        public static EstudianteEmailDTO ToEstudianteEmailDTO(Estudiante estudiante)
        {
            return new EstudianteEmailDTO(estudiante.Email);
        }
    }
}
