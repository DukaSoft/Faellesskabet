using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace Faellesskabet_Web.Services
{
    public class EmailSettings
    {
        public bool IsDevelopment { get; set; }
        public bool UseSsl { get; set; }
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string Password { get; set; }
        public string AdminEmail { get; set; }
    }

    public interface IEmailSender
        {
            /// <summary>
            /// Sends an email to the <paramref name="email" /> address with <paramref name="subject" />,
            /// <paramref name="textMessage" /> and <paramref name="htmlMessage" />.
            /// </summary>
            /// <param name="email">The send to email address.</param>
            /// <param name="subject">The email's subject.</param>
            /// <param name="textMessage">The email's MimeKit.BodyBuilder.TextBody.</param>
            /// <param name="htmlMessage">The email's MimeKit.BodyBuilder.HtmlBody.</param>
            Task SendEmailAsync(string email, string subject, string textMessage, string htmlMessage);

            /// <summary>
            /// Sends an email to the address set with the readonly <see cref="EmailSettings.AdminEmail" /> property
            /// with <paramref name="subject" /> and <paramref name="textMessage" /> including HttpContext properties.
            /// </summary>
            /// <param name="subject">The email's subject.</param>
            /// <param name="textMessage">The email's MimeMessage.Body.TextPart.</param>
            /// <remarks>
            /// Uses <see cref="HttpContextAccessor" /> to extract current context properties to include when an email sent
            /// to the <see cref="EmailSettings.AdminEmail" /> address.
            /// </remarks>    
            //Task SendAdminEmailAsync(string subject, string textMessage);
        }

	public class EmailSender : IEmailSender
	{
        private readonly EmailSettings _emailSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmailSender(IOptions<EmailSettings> emailSettings, IHttpContextAccessor httpContextAccessor)
        {
            _emailSettings = emailSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendEmailAsync(string email, string subject, string textMessage, string htmlMessage)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            mimeMessage.To.Add(MailboxAddress.Parse(email));

            mimeMessage.Subject = subject;
            var builder = new BodyBuilder { TextBody = textMessage, HtmlBody = htmlMessage };
            mimeMessage.Body = builder.ToMessageBody();

            try
            {
                using var client = new SmtpClient();
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                //if (_emailSettings.IsDevelopment)
                    await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, _emailSettings.UseSsl)
                        .ConfigureAwait(false);
                //else
                //    await client.ConnectAsync(_emailSettings.MailServer).ConfigureAwait(false);

                // Note: only needed if the SMTP server requires authentication
                await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password).ConfigureAwait(false);
                await client.SendAsync(mimeMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
