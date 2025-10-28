using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using static System.Net.WebRequestMethods;

namespace NextLevel.LogicaNegocio.SistemaAutenticacion
{
    public class VerificacionEmail
    {
        private readonly string _emailEmpresa;
        private readonly string _password;

        public VerificacionEmail()
        {
            _emailEmpresa = "no.replayNextLevel@gmail.com";
            _password = "qvjj rqic fgsn jsou";
        }

        public void EnviarCorreoVerificacion(string emailDestino, string token)
        {
            string subject = "Verificá tu cuenta";
            string verificationLink = $"https://localhost:7127/Usuarios/VerificarEmail?token={token}";
            string cancelVerification = $"https://localhost:7127/Usuarios/CancelarVerificacion?token={token}";
            string gifPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "NeoSaludando.gif");

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(_emailEmpresa, "NextLevel"),
                Subject = subject,
                IsBodyHtml = true
            };
            mail.To.Add(emailDestino);

            string htmlBody = $@"
                <div style='text-align:center; font-family:Arial,sans-serif;'>
                    <img src='cid:logoGif' alt='NextLevel' width='300' style='border-radius:10px;'>
                    <h2>¡Hola!</h2>
                    <p>Gracias por registrarte en <strong>NextLevel</strong>.</p>
                    <p>Para activar tu cuenta, hacé clic en el botón de abajo:</p>
                    <a href='{verificationLink}' style='
                        display:inline-block;
                        padding:12px 24px;
                        background-color:#007BFF;
                        color:white;
                        text-decoration:none;
                        border-radius:8px;
                        font-weight:bold;
                    '>Verificar mi cuenta</a>
                    <p style='margin-top:20px; font-size:14px; color:#555;'>
                        ¿No realizaste esta solicitud? <a href='{cancelVerification}'>Hacé clic aquí.</a>
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
                smtp.Send(mail);
            }
        }
    }
}
