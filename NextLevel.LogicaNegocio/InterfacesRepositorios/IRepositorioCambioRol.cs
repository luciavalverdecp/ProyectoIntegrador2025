using NextLevel.LogicaNegocio.Entidades;
using Olimpiadas.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioCambioRol : IRepositorio<CambioRol>
    {
        public CambioRol FindByEmail(string email);
    }
}
