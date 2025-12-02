using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.InterfacesEntidades;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Foro 
    {
        public int Id { get; set; }
        public List<Mensaje> Mensajes { get; set; }

        

    }
}
