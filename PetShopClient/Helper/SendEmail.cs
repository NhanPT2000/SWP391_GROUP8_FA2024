using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;

namespace PetShopClient.Helper
{
    public class SendEmail
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SendEmail(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendConfirmationEmail(string email, Guid userId, string scheme, string host)
        {
            string confirmationLink = $"{scheme}://{host}/Access/ConfirmEmail?userId={userId}";
            string subject = "Xác thực email của bạn";
            string body = $"Vui lòng xác nhận email của bạn bằng cách nhấn đường link: <a href='{confirmationLink}'>Confirm Email</a>";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("phamnhan.27122000@gmail.com");
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("phamnhan.27122000@gmail.com", "kqnw txys wkuo kdts");
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
        }
    }
}
