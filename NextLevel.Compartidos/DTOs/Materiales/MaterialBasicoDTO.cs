using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Materiales
{
    public record MaterialBasicoDTO(string RutaArchivo, string Nombre, TipoMaterial TipoMaterial);
}
