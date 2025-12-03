using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Materiales;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class MaterialMapper
    {
        public static IEnumerable<MaterialBasicoDTO> ToListMaterialBasicoDTO(IEnumerable<Material> materiales)
        {
            return materiales.Select(m => new MaterialBasicoDTO(m.RutaArchivo, m.Nombre, m.TipoMaterial));
        }
    }
}
