using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Cursos
{
    public class AgregarClase : IAgregarClase
    {
        private readonly IRepositorioCurso repositorioCurso;
        public AgregarClase(IRepositorioCurso repositorioCurso)
        {
            this.repositorioCurso = repositorioCurso;
        }

        public void Ejecutar(AgendarClaseDTO claseAgregada)
        {
            if (claseAgregada.Fecha == null ||
                claseAgregada.Fecha == new DateTime() ||
                claseAgregada.Fecha < DateTime.Now) 
                    throw new CursoFechaException("Seleccione una fecha y horarios validos");
            if (claseAgregada.CursoNombre == null) throw new CursoException("Error al obtener el curso, intente nuevamente");
            var Curso = repositorioCurso.FindByNombre(claseAgregada.CursoNombre);
            if (Curso.FechasClases.Contains(claseAgregada.Fecha)) throw new CursoFechaException("Ya tienes registrada una clase para ese dia y fecha.");
            Curso.FechasClases.Add(claseAgregada.Fecha);
            repositorioCurso.Update(Curso);
        }
    }
}
