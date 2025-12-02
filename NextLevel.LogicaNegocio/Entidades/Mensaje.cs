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
        public Usuario Usuario {  get; set; }
        public string mensaje {  get; set; }
        public bool EsDelEstudiante => Usuario != null && Usuario.Rol == Rol.Estudiante;

        public Mensaje() { }
        public Mensaje(Usuario usuario, string mensaje)
        {
            Usuario = usuario;
            this.mensaje = mensaje;
        }
    }
}
