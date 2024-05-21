using ProyectoWheelWander.Models;
using ProyectoWheelWander.Services;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace ProyectoWheelWander.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _emailConfiguration;

        public EmailServices(IConfiguration config) {
            _emailConfiguration = config;
        }
        public void enviarEmail(EmailDTO request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailConfiguration.GetSection("Email:UserName").Value));
            email.To.Add(MailboxAddress.Parse(request.Para));
            email.Subject = request.Asunto;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Contenido };

            using var smpt = new SmtpClient();
            smpt.Connect(
                _emailConfiguration.GetSection("Email:Host").Value, 
                int.Parse(_emailConfiguration.GetSection("Email:Port").Value), 
                SecureSocketOptions.StartTls
            );

            smpt.Authenticate(
                _emailConfiguration.GetSection("Email:UserName").Value,
                _emailConfiguration.GetSection("Email:Password").Value
            );

            smpt.Send(email);

            smpt.Disconnect(true);
        }
    }

}
