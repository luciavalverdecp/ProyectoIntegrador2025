using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Mensaje
    {
        public int Id { get; set; }
        public int ConversacionId { get; set; }
        public Conversacion Conversacion { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaEnvio { get; set; }

        public bool EsDelEstudiante => Usuario != null && Usuario.Rol == Rol.Estudiante;

        public Mensaje() { }
        public Mensaje(Conversacion conversacion, Usuario usuario, string mensaje)
        {
            this.Conversacion = conversacion;
            this.Usuario = usuario;
            this.Contenido = mensaje;
            FechaEnvio = DateTime.Now;
        }
    }
}
