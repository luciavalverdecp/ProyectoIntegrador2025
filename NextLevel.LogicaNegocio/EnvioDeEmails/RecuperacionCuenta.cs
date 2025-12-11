using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace NextLevel.LogicaNegocio.SistemaAutenticacion
{
    public class RecuperacionCuenta
    {
        private readonly string _emailEmpresa;
        private readonly string _password;

        public RecuperacionCuenta()
        {
            _emailEmpresa = "no.replayNextLevel@gmail.com";
            _password = "qvjj rqic fgsn jsou";
        }

        public async Task EnviarCorreoVerificacionAsync(string emailDestino)
        {
            string subject = "Recuperación de cuenta";
            string recoveryLink = $"proyectointegrador2025-dnbdbdegfygpduec.eastus2-01.azurewebsites.net/Usuarios/ReiniciarContrasena?email={Uri.EscapeDataString(emailDestino)}";
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

                                <h2 style='color:#1E3A8A; margin-bottom:10px;'>Recuperación de contraseña 🔐</h2>
                                <p style='color:#555; font-size:15px; line-height:1.6;'>
                                    ¡Hola!<br>
                                    Recibimos una solicitud para restablecer tu contraseña en <strong>NextLevel</strong>.<br>
                                    Si fuiste vos, podés crear una nueva contraseña haciendo clic en el siguiente botón:
                                </p>

                                <a href='{recoveryLink}' style='
                                    display:inline-block;
                                    background-color:#1E3A8A;
                                    color:#ffffff;
                                    text-decoration:none;
                                    font-weight:bold;
                                    margin-top:20px;
                                    padding:12px 30px;
                                    border-radius:10px;
                                    font-size:15px;
                                    transition:background 0.3s ease;
                                '>Restablecer contraseña</a>

                                <p style='color:#666; font-size:14px; margin-top:25px;'>
                                    Si no solicitaste cambiar tu contraseña, podés ignorar este correo.<br>
                                    Por seguridad, el enlace expirará en <strong>30 minutos</strong>.
                                </p>

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
