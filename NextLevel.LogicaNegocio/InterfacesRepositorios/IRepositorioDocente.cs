using NextLevel.LogicaNegocio.Entidades;
using Olimpiadas.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioDocente : IRepositorio<Docente>
    {
        Usuario FindByEmail(string email);
        Docente GetDocenteByNroDocente(int nroDocente);
        void UpdateDatosPersonales(Docente docente);
    }
}
