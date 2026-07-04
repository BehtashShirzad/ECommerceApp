using ECommerce.Infrastructure.Contracts;
using ECommerce.Infrastructure.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ECommerce.Infrastructure.Services.Notification;

public class EmailClient(IOptions<EmailProviderOptions>options):IEmailClient
{
   private readonly EmailProviderOptions _emailProviderOptions=options.Value;
    public async Task SendEmailAsync(string email,
        string toName,
        string subject,
        string message,
        CancellationToken cancellationToken,
        EmailBodyType emailBodyType=EmailBodyType.Plain)
    {
        var mimeMessage = new MimeMessage ();
        mimeMessage.From.Add (new MailboxAddress (_emailProviderOptions.SenderName, _emailProviderOptions.FromEmail));
        mimeMessage.To.Add (new MailboxAddress (toName, email));
        mimeMessage.Subject = subject;
        mimeMessage.Body = new TextPart(emailBodyType.ToString().ToLower())
        {
            Text = message
        };
        using var client = new SmtpClient ();
        await client.ConnectAsync(
            _emailProviderOptions.SmtpAddress,
            _emailProviderOptions.SmtpPort,
            SecureSocketOptions.StartTls,
            cancellationToken);
           
        await client.AuthenticateAsync (_emailProviderOptions.Username, _emailProviderOptions.Password,cancellationToken);

        var response=await client.SendAsync (mimeMessage,cancellationToken);
        await  client.DisconnectAsync(true,cancellationToken);
    }

   
}