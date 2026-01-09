using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
using Olimpiadas.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioParticiapanteConversacion:IRepositorio<ParticipanteConversacion>
    {
        ParticipanteConversacion GetPartConv(Conversacion conversacion, Usuario usuario);

        IEnumerable<ParticipanteConversacion> GetPartConvCurso(Curso curso, Usuario usuario);

        ParticipanteConversacion GetPartConvEstudianteCurso(Curso curso, Usuario usuario);
    }
}
