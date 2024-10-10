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

            public async Task SendConfirmationEmail(string email, Guid userId)
            {
                var request = _httpContextAccessor.HttpContext.Request;
                string confirmationLink = _linkGenerator.GetUriByAction(
                    _httpContextAccessor.HttpContext,
                    action: "ConfirmEmail",
                    controller: "Access",
                    values: new { userId = userId },
                    scheme: request.Scheme
                );

                string subject = "Confirm your email";
                string body = $"Please confirm your email by clicking this link: <a href='{confirmationLink}'>Confirm Email</a>";

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
