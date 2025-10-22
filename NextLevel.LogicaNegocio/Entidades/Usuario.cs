using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.InterfacesEntidades;

namespace NextLevel.LogicaNegocio.Entidades
{
    public abstract class Usuario : IEntity, IValidable, IComparable<Usuario>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono {  get; set; }
        public Rol Rol {  get; set; }

        public Usuario() { }

        public Usuario (string email, string password, string nombreCompleto, string telefono)
        {
            Email = email;
            Password = password;
            NombreCompleto = nombreCompleto;
            Telefono = telefono;
            Rol = Rol.Estudiante;
            Validar();
            Telefono = telefono;
        }

        #region Validaciones
        public virtual void Validar()
        {
            validarEmail();
            validarPassword();
            validarNombreCompleto();
            validarTelefono();
        }

        private void validarEmail()
        {
            if (string.IsNullOrEmpty(Email)) throw new Exception("El email no puede ser vacio.");
            if (!Email.Contains("@") || !Email.Contains(".")) throw new Exception("Email invalido. Este debe contener un @ y un punto.");
        }

        private void validarPassword()
        {
            if (string.IsNullOrEmpty(Password)) throw new Exception("La contraseña no puede ser vacia.");
            if (!Password.Contains(@"^(?=.[a-z])(?=.[A-Z])(?=.*\d).+$")) throw new Exception("La contraseña debe contener al menos una mayusucla, minuscula y un numero");
            if (Password.Length < 8) throw new Exception("La contraseña debe tener al menos 8 caracteres.");
        }

        private void validarNombreCompleto()
        {
            if (string.IsNullOrEmpty(NombreCompleto)) throw new Exception("El nombre completo no puede ser vacio.");
            if (!NombreCompleto.Trim().Contains(" ")) throw new Exception("El nombre debe estar separado con un espacio del apellido.");
        }
        private void validarTelefono()
        {
            if (string.IsNullOrEmpty(Telefono)) throw new Exception("El telefono no puede ser vacio.");
            if (!Telefono.StartsWith("0") || Telefono.Length < 9) throw new Exception("Formato de telefono invalido.");
        }
        #endregion

        #region Equals - CompareTo
        public override bool Equals(object? obj)
        {
            return obj is Usuario u && u.Email == this.Email;
        }

        public int CompareTo(Usuario? other)
        {
            return this.Email.CompareTo(other.Email);
        }
        #endregion
    }
}
