using NextLevel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.EnvioDeEmails
{
    public class PagoConfirmado
    {
        private readonly string _emailEmpresa;
        private readonly string _password;

        public PagoConfirmado()
        {
            _emailEmpresa = "no.replayNextLevel@gmail.com";
            _password = "qvjj rqic fgsn jsou";
        }

        public async Task EnviarAvisoPagoRealizadoAsync(string emailDestino, string cursoNombre)
        {
            string subject = "¡Pago realizado exitosamente!";

            string linkCurso = $"https://nextlevel-gtcgfsgnczbgenga.eastus2-01.azurewebsites.net/Cursos/VisualizarCurso?nombreCurso={cursoNombre}";

            string gifPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "NeoSaludando.gif");

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(_emailEmpresa, "NextLevel"),
                Subject = subject,
                IsBodyHtml = true
            };
            mail.To.Add(emailDestino);

            string htmlBody = $@"
        <div style='
            text-align:center;
            font-family: Segoe UI, Arial, sans-serif;
            background-color:#f5f7fa;
            padding:30px;
            border-radius:16px;
            max-width:600px;
            margin:auto;
            box-shadow:0 2px 12px rgba(0,0,0,0.1);
        '>
            <img src='cid:logoGif' alt='NextLevel' width='180' style='border-radius:12px; margin-bottom:20px;' />

            <h2 style='color:#1E3A8A; margin-bottom:10px;'>¡Inscripción confirmada! 🎉</h2>

            <p style='color:#555; font-size:15px; line-height:1.6;'>
                Tu inscripción al siguiente curso fue confirmada correctamente:<br>
                <strong style='color:#1E3A8A; font-size:17px;'>{cursoNombre}</strong>
            </p>

            <p style='color:#444; font-size:15px; margin-top:15px;'>
            </p>

            <a href='{linkCurso}' style='
                display:inline-block;
                background-color:#1E3A8A;
                color:#ffffff;
                text-decoration:none;
                font-weight:bold;
                margin-top:25px;
                padding:12px 30px;
                border-radius:10px;
                font-size:15px;
                transition:background 0.3s ease;
            '>Ver curso</a>

            <hr style='margin:30px 0; border:none; height:1px; background:#ddd;' />

            <p style='font-size:12px; color:#888;'>
                © 2025 NextLevel. Todos los derechos reservados.<br>
                Este correo fue enviado automáticamente, por favor no respondas.
            </p>
        </div>";

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

            if (System.IO.File.Exists(gifPath))
            {
                LinkedResource gifResource = new LinkedResource(gifPath, MediaTypeNames.Image.Gif)
                {
                    ContentId = "logoGif",
                    TransferEncoding = TransferEncoding.Base64
                };
                htmlView.LinkedResources.Add(gifResource);
            }

            mail.AlternateViews.Add(htmlView);

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential(_emailEmpresa, _password);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }
    }
}
