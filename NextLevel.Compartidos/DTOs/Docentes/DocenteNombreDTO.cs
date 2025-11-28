using NextLevel.LogicaNegocio.ValueObject.Docente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.Compartidos.DTOs.Docentes
{
    public record DocenteNombreDTO(string NombreCompleto, int NroDocente);
}
