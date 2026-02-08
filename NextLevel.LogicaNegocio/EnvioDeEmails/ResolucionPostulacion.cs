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
    public class ResolucionPostulacion
    {
        private readonly string _emailEmpresa;
        private readonly string _password;

        public ResolucionPostulacion()
        {
            _emailEmpresa = "no.replayNextLevel@gmail.com";
            _password = "qvjj rqic fgsn jsou";
        }

        public async Task EnviarResolucionCambioRolAsync(string emailDestino, string motivo, string resolucion, int nroDocente)
        {
            string subject = resolucion == "Aprobada"
                            ? "Tu solicitud de cambio de rol fue aprobada 🎉"
                            : "Tu solicitud de cambio de rol fue rechazada ❌"; 
            string gifPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "NeoSaludando.gif");

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(_emailEmpresa, "NextLevel"),
                Subject = subject,
                IsBodyHtml = true
            };
            mail.To.Add(emailDestino);

            string color = resolucion == "Aprobada" ? "#2563eb" : "#dc2626";
            string icono = resolucion == "Aprobada" ? "🎓" : "❌";

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

                                    <img src='cid:logoGif' width='160' style='border-radius:12px; margin-bottom:20px;' />

                                    <h2 style='color:{color}; margin-bottom:10px;'>
                                        {icono} Cambio de rol {resolucion}
                                    </h2>

                                    <p style='color:#444; font-size:15px; line-height:1.6;'>
                                        Hola,<br><br>
                                        Te informamos que tu solicitud de <strong>cambio de rol</strong> en
                                        <strong>NextLevel</strong> fue <strong>{resolucion.ToLower()}</strong>.
                                    </p>

                                    <div style='
                                        background:#ffffff;
                                        padding:15px;
                                        border-radius:12px;
                                        margin-top:20px;
                                        border-left:5px solid {color};
                                        text-align:left;
                                    '>
                                        <strong>Motivo:</strong>
                                        <p style='margin-top:8px; color:#555;'>{motivo}</p>
                                    </div>

                                    <div style='
                                        background:#ffffff;
                                        padding:15px;
                                        border-radius:12px;
                                        margin-top:20px;
                                        border-left:5px solid #4CAF50;
                                        text-align:left;
                                    '>
                                        <strong>📘 Información de acceso como docente</strong>
                                        <p style='margin-top:10px; color:#555; line-height:1.6;'>
                                            Se te ha asignado el siguiente <strong>número de docente</strong>:
                                            <br><br>
                                            <span style='
                                                display:inline-block;
                                                background:#f0f4ff;
                                                padding:8px 14px;
                                                border-radius:8px;
                                                font-size:16px;
                                                font-weight:bold;
                                                color:#2c3e50;
                                            '>
                                                {nroDocente}
                                            </span>
                                            <br><br>
                                            A partir de ahora, podrás ingresar al sistema utilizando este número de docente
                                            junto con la <strong>contraseña actual de tu cuenta de estudiante</strong>.
                                        </p>

                                        <p style='margin-top:12px; color:#777; font-size:14px;'>
                                            🔐 Por razones de seguridad, te recomendamos cambiar tu contraseña desde tu perfil
                                            una vez que ingreses al sistema.
                                        </p>
                                    </div>

                                    <p style='color:#666; font-size:14px; margin-top:25px;'>
                                        Si tenés alguna consulta, podés comunicarte con el equipo de soporte.
                                    </p>

                                    <hr style='margin:30px 0; border:none; height:1px; background:#ddd;' />

                                    <p style='font-size:12px; color:#888;'>
                                        © 2025 NextLevel<br>
                                        Este correo fue enviado automáticamente.
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

        public async Task EnviarResolucionAltaCursoAsync(string emailDestino, string motivo, string resolucion)
        {
            string subject = resolucion == "Aprobada"
                            ? "Tu curso fue aprobado 🎉"
                            : "Tu curso fue rechazado ❌"; 
            string gifPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "NeoSaludando.gif");

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(_emailEmpresa, "NextLevel"),
                Subject = subject,
                IsBodyHtml = true
            };
            mail.To.Add(emailDestino);

            string color = resolucion == "Aprobada" ? "#16a34a" : "#dc2626";
            string icono = resolucion == "Aprobada" ? "✅" : "❌";

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

                                    <img src='cid:logoGif' width='160' style='border-radius:12px; margin-bottom:20px;' />

                                    <h2 style='color:{color}; margin-bottom:10px;'>
                                        {icono} Curso {resolucion}
                                    </h2>

                                    <p style='color:#444; font-size:15px; line-height:1.6;'>
                                        Hola,<br><br>
                                        Queremos informarte que tu solicitud de <strong>alta de curso</strong> en
                                        <strong>NextLevel</strong> fue <strong>{resolucion.ToLower()}</strong>.
                                    </p>

                                    <div style='
                                        background:#ffffff;
                                        padding:15px;
                                        border-radius:12px;
                                        margin-top:20px;
                                        border-left:5px solid {color};
                                        text-align:left;
                                    '>
                                        <strong>Motivo:</strong>
                                        <p style='margin-top:8px; color:#555;'>{motivo}</p>
                                    </div>

                                    <p style='color:#666; font-size:14px; margin-top:25px;'>
                                        Si tenés dudas o querés realizar modificaciones, podés volver a postular cuando lo desees.
                                    </p>

                                    <hr style='margin:30px 0; border:none; height:1px; background:#ddd;' />

                                    <p style='font-size:12px; color:#888;'>
                                        © 2025 NextLevel<br>
                                        Este correo fue enviado automáticamente.
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
