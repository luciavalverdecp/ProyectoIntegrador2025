using NextLevel.AccesoDatos.EF;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Cursos
{
    public class AgregarCalificacion : IAgregarCalificacion
    {
        private readonly IRepositorioCurso repositorioCurso;
        public AgregarCalificacion(IRepositorioCurso repositorioCurso)
        {
            this.repositorioCurso = repositorioCurso;
        }

        public void Ejecutar(CursoDTO curso, double puntaje)
        {
            Curso cursoActualizado = repositorioCurso.FindByNombre(curso.Nombre);
            cursoActualizado.TotalCalificaciones.Add(puntaje);
            cursoActualizado.CalcularCalificacion();
            repositorioCurso.Update(cursoActualizado);
        }
    }
}
