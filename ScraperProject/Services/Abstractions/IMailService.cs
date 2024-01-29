using ScraperProject.Dtos;

namespace ScraperProject.Services.Abstractions;

internal interface IMailService
{
    Task SendMail(MailData mailData);
}
