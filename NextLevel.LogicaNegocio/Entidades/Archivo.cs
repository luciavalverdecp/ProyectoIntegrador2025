using NextLevel.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Archivo : IEntity
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public string Ruta { get; set; }
        public long Peso { get; set; }
        public string ContentType { get; set; }
        public Archivo() { }
        public Archivo(string nombreArchivo, string ruta, long peso, string contentType)
        {
            NombreArchivo = nombreArchivo;
            Ruta = ruta;
            Peso = peso;
            ContentType = contentType;
        }
    }
}
