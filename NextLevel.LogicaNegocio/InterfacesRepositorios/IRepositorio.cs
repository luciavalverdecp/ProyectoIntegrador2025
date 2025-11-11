using Olimpiadas.LogicaNegocio.InterfacesRepositorios;

namespace Olimpiadas.LogicaNegocio.InterfacesRepositorios
{
	public interface IRepositorio<T>
	{
		void Add(T obj);

		void Remove(int id);

		void Update(T obj);

		T FindById(int id);

		IEnumerable<T> FindAll();

	}

}

