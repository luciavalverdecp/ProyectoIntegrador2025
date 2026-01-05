using Microsoft.EntityFrameworkCore;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.Compartidos.DTOs.Pagos;
using NextLevel.LogicaAplicacion.InterfacesCU.Pagos;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.EnvioDeEmails;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using NextLevel.LogicaNegocio.SistemaAutenticacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Pagos
{
    public class RealizarPago : IRealizarPago
    {
        private readonly IRepositorioPago repositorioPago;
        private readonly IRepositorioEstudiante repositorioEstudiante;
        private readonly IRepositorioCurso repositorioCurso;
        public RealizarPago(IRepositorioPago repositorioPago,
            IRepositorioEstudiante repositorioEstudiante, 
            IRepositorioCurso repositorioCurso)
        {
            this.repositorioPago = repositorioPago;
            this.repositorioEstudiante = repositorioEstudiante;
            this.repositorioCurso = repositorioCurso;
        }

        //public PagoIdDTO CrearPago(string usuarioEmail, string cursoNombre, MetodoPago metodoPago)
        //{
        //    var usuario = repositorioEstudiante.FindByEmail(usuarioEmail);
        //    var curso = repositorioCurso.FindByNombre(cursoNombre);

        //    if (usuario == null || curso == null)
        //        throw new Exception("Datos inválidos");

        //    Pago pago = new Pago(usuario, curso, curso.Precio, metodoPago);
        //    repositorioPago.Add(pago);

        //    PagoIdDTO pagoIdDTO = PagoMapper.ToPagoIdDto(pago);
        //    return pagoIdDTO;
        //}

        public PagoIdDTO ProcesarPagoSandbox(CrearPagoDTO dto)
        {
            var usuario = repositorioEstudiante.FindByEmail(dto.UsuarioEmail);
            var curso = repositorioCurso.FindByNombre(dto.CursoNombre);

            if (usuario == null || curso == null)
                throw new Exception("Usuario o curso inválido");

            // 2️⃣ Validar tarjeta (formato + lógica real)
            if (!TarjetaValida(dto.DatosTarjetaDTO))
                throw new Exception("Datos de tarjeta inválidos");

            // 3️⃣ Crear pago (pendiente)
            var pago = new Pago(usuario, curso, dto.MetodoPago);
            PagoConfirmado pagoConfirmado = new PagoConfirmado();
            Task.Run(() => pagoConfirmado.EnviarAvisoPagoRealizadoAsync(dto.UsuarioEmail, dto.CursoNombre));
            repositorioPago.Add(pago);

            // 4️⃣ Simulación de pasarela
            pago.EstadoPago = TarjetaExiste(dto.DatosTarjetaDTO.Numero)
                ? EstadoPago.Aprobado
                : EstadoPago.Rechazado;

            repositorioPago.Update(pago);

            return PagoMapper.ToPagoIdDto(pago);
        }

        // -----------------------------
        // Validaciones
        // -----------------------------

        private bool TarjetaValida(DatosTarjetaDTO tarjeta)
        {
            if (!ValidarLuhn(tarjeta.Numero))
                return false;

            if (tarjeta.CVV.Length != 3)
                return false;

            if (tarjeta.MesVencimiento < 1 || tarjeta.MesVencimiento > 12)
                return false;

            // Fecha de vencimiento = último día del mes
            var fechaVencimiento = new DateTime(
                tarjeta.AnioVencimiento,
                tarjeta.MesVencimiento,
                DateTime.DaysInMonth(tarjeta.AnioVencimiento, tarjeta.MesVencimiento)
            );

            return fechaVencimiento >= DateTime.Today;
        }



        private bool TarjetaExiste(string numero)
        {
            // Sandbox explícito
            return numero == "4111111111111111";
        }

        private bool ValidarLuhn(string numero)
        {
            int suma = 0;
            bool alternar = false;

            for (int i = numero.Length - 1; i >= 0; i--)
            {
                int n = numero[i] - '0';
                if (alternar)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }
                suma += n;
                alternar = !alternar;
            }

            return suma % 10 == 0;
        }
    }
}
