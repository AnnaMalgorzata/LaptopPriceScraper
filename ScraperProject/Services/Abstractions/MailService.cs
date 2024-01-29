using Microsoft.Extensions.Options;
using ScraperProject.Dtos;
using System.Net;
using System.Net.Mail;

namespace ScraperProject.Services.Abstractions;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;
    public MailService(IOptions<MailSettings> mailSettingsOptions)
    {
        _mailSettings = mailSettingsOptions.Value;
    }

    public async Task SendMail(MailData mailData)
    {
        var client = new SmtpClient(_mailSettings.Server, _mailSettings.Port)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_mailSettings.SenderEmail, _mailSettings.SenderPassword),
        };
        try
        {
            await client.SendMailAsync(new MailMessage(
                from: _mailSettings.SenderEmail,
                to: mailData.ReceiverEmail,
                subject: mailData.EmailSubject,
                body: mailData.EmailBody));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
