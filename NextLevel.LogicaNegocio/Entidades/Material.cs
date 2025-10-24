using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Material;
using NextLevel.LogicaNegocio.InterfacesEntidades;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Material : IEntity, IValidable
    {
        public int Id {  get; set; }
        public string Nombre { get; set; }
        public DateTime FechaAgregado {  get; set; }
        public TipoMaterial TipoMaterial { get; set; }
        [NotMapped]
        public IFormFile Archivo {  get; set; }
        public string RutaArchivo { get; set; }
        public string Texto { get; set; }
        public Material() { }

        public Material(string nombre, TipoMaterial tipoMaterial, IFormFile archivo, string texto)
        {
            Nombre = nombre;
            FechaAgregado = DateTime.Now;
            TipoMaterial = tipoMaterial;
            Archivo = archivo;
            Texto = texto;
        }

        #region Validaciones
        public void Validar()
        {
            validarNombre();
        }

        private void validarNombre()
        {
            if (string.IsNullOrEmpty(Nombre)) throw new MaterialNombreException("El nombre del archivo no puede ser vacio");
        }

        private void validarArchivoYTexto()
        {
            if (Archivo == null && string.IsNullOrEmpty(Texto)) throw new MaterialArchivoTextoException("Debe subir un archivo o un texto");
        }
        #endregion
    }
}
