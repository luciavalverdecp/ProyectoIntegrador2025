using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Mensajeria
    {
        public Usuario Receptor {  get; set; }
        public Usuario Emisor { get; set; }
        public Curso Curso { get; set; }
        public List<Mensaje> Mensajes { get; set; }

        public Mensajeria() { }
        public Mensajeria (Usuario receptor, Usuario emisor, Curso curso)
        {
            this.Receptor = receptor;
            this.Emisor = emisor;
            this.Curso = curso;
            Mensajes = new List<Mensaje>();
        }

        #region Metodos
        public void AgregarMensaje(Mensaje mensaje)
        {
            Mensajes.Add(mensaje);
        }
        #endregion
    }
}
