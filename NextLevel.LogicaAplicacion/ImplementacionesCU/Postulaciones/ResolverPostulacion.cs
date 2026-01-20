using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaAplicacion.InterfacesCU.Postulaciones;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.EnvioDeEmails;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Postulaciones
{
    public class ResolverPostulacion : IResolverPostulacion
    {
        private readonly IRepositorioPostulacion _repositorioPostulacion;
        public ResolverPostulacion(IRepositorioPostulacion repositorioPostulacion)
        {
            _repositorioPostulacion = repositorioPostulacion;
        }

        public void Ejecutar(int id, string motivo, string resolucion)
        {
            var postulacion = _repositorioPostulacion.FindById(id);
            if(postulacion.AltaCurso != null)
            {
                Docente docente = postulacion.AltaCurso.Curso.Docente;
                ResolucionPostulacion resolucionPost = new ResolucionPostulacion();
                resolucionPost.EnviarResolucionAltaCursoAsync(docente.Email, motivo, resolucion);
            }
            else
            {
                Estudiante estudiante = postulacion.CambioRol.Estudiante;
                ResolucionPostulacion resolucionPost = new ResolucionPostulacion();
                resolucionPost.EnviarResolucionCambioRolAsync(estudiante.Email, motivo, resolucion);
            }
            postulacion.Estado = resolucion.ToString();
            _repositorioPostulacion.Update(postulacion);
        }
    }
}
