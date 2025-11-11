using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.InterfacesEntidades;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Administrador : Usuario, IEntity
    {
        public Administrador() { }
        public Administrador(string email, string password, string nombreCompleto, string telefono) : base(email, password, nombreCompleto, telefono)
        {
            base.Validar();
        }
    }
}
