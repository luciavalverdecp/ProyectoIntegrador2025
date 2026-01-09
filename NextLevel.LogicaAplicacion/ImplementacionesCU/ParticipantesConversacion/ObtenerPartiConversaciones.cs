using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.Compartidos.DTOs.ParticipantesConversacion;
using NextLevel.LogicaAplicacion.InterfacesCU.ParticipantesConversacion;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.ParticipantesConversacion
{
    public class ObtenerPartiConversaciones : IObtenerPartiConversaciones
    {
        private readonly IRepositorioParticiapanteConversacion repositorioParticiapanteConversacion;
        private readonly IRepositorioDocente repositorioDocente;
        private readonly IRepositorioCurso repositorioCurso;
        private readonly IRepositorioEstudiante repositorioEstudiante;

        public ObtenerPartiConversaciones(IRepositorioParticiapanteConversacion repositorioParticiapanteConversacion, 
            IRepositorioDocente repositorioDocente,
            IRepositorioCurso repositorioCurso,
            IRepositorioEstudiante repositorioEstudiante)
        {
            this.repositorioParticiapanteConversacion = repositorioParticiapanteConversacion;
            this.repositorioDocente = repositorioDocente;
            this.repositorioCurso = repositorioCurso;
            this.repositorioEstudiante = repositorioEstudiante;
        }

        public IEnumerable<ParticipanteConversacionDTO> Ejecutar(string nombreCurso, string emailLogueado)
        {
            var curso = repositorioCurso.FindByNombre(nombreCurso);
            int.TryParse(emailLogueado, out int nroDocente);
            Usuario usuario = null;
            if (nroDocente != 0)
            {
                usuario = repositorioDocente.GetDocenteByNroDocente(int.Parse(emailLogueado));
            }
            else
            {
                usuario = repositorioEstudiante.FindByEmail(emailLogueado);
            }
            var listado = repositorioParticiapanteConversacion.GetPartConvCurso(curso, usuario);
            if (listado != null) return new List<ParticipanteConversacionDTO>();
            return ParticipanteConversacionMapper.ToListParticipanteConversacionDTO(listado);
        }

        public ParticipanteConversacionDTO Ejecutar2(string nombreCurso, string emailLogueado)
        {
            var curso = repositorioCurso.FindByNombre(nombreCurso);
            Usuario usuario = repositorioEstudiante.FindByEmail(emailLogueado);
            var participante = repositorioParticiapanteConversacion.GetPartConvEstudianteCurso(curso, usuario);

            if (participante == null)
                return null;

            return ParticipanteConversacionMapper.ToParticipanteConversacionDTO(participante);
        }
    }
}
