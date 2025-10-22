using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.InterfacesEntidades;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Material : IEntity, IValidable
    {
        public int Id {  get; set; }
        public string Nombre { get; set; }
        public DateTime FechaAgregado {  get; set; }
        public TipoMaterial TipoMaterial { get; set; }

        public Material() { }

        public Material(string nombre, TipoMaterial tipoMaterial)
        {
            Nombre = nombre;
            FechaAgregado = DateTime.Now;
            TipoMaterial = tipoMaterial;
        }

        #region Validaciones
        public void Validar()
        {
            validarNombre();
        }

        private void validarNombre()
        {
            if (string.IsNullOrEmpty(Nombre)) throw new Exception("El nombre del archivo no puede ser vacio");
        }
        #endregion
    }
}
