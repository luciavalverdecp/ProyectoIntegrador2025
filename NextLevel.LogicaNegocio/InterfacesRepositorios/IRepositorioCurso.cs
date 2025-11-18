using NextLevel.LogicaNegocio.Entidades;
using Olimpiadas.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioCurso : IRepositorio<Curso>
    {
        IEnumerable<Curso> FindWithFilter(string filter, IEnumerable<Curso> lista);
        IEnumerable<Curso> FindWithCategory(string categoria, string? alfabetico, int? calificacion, string? docente, IEnumerable<Curso> lista);
    }
}
