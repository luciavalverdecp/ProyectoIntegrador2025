using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NombreCompleto { get; set; }
        public string Rol {  get; set; }

        public Usuario() { }

        public Usuario (string email, string password, string nombreCompleto)
        {
            Email = email;
            Password = password;
            NombreCompleto = nombreCompleto;
        }
    }
}
