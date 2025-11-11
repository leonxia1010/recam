using System;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Remp.Application.Interfaces;
using Remp.Model.Settings;

namespace Remp.Application.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {

        var message = new MimeMessage();
        var mailBoxAddress = new MailboxAddress(_emailSettings.Name, _emailSettings.SenderEmail);
        message.From.Add(mailBoxAddress);
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;

        var builder = new BodyBuilder() { HtmlBody = body };
        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailSettings.SmtpServer,
                _emailSettings.SmtpPort,
                _emailSettings.SmtpPort == 587
                    ? MailKit.Security.SecureSocketOptions.StartTls
                    : MailKit.Security.SecureSocketOptions.SslOnConnect);

            await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password);
            await client.SendAsync(message);
        }
        catch (Exception e)
        {
            //TODO update global exception handler
            throw new Exception(e.Message);
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}
