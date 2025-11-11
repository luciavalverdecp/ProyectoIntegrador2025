using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.Compartidos.DTOs.Usuarios
{
    public record UsuarioRegistroDTO(string Email, string Password, string NombreCompleto, string Telefono);
}
