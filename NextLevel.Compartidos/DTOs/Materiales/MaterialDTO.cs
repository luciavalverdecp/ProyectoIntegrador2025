using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NextLevel.Compartidos.DTOs.Materiales
{
    public record MaterialDTO(string Nombre, IFormFile Archivo, string Texto);
}
