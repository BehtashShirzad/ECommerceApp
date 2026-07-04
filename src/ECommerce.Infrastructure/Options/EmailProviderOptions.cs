namespace ECommerce.Infrastructure.Options;

public class EmailProviderOptions
{
    public string FromEmail { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }

    public string SmtpAddress { get;set; }
    public int SmtpPort { get;set; }
    public string SenderName { get;set; }
}