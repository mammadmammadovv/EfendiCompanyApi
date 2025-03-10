using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using Core.Constants;

namespace GPS.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(string recipientEmail, string subject, string body);
    Task SendFileAsync(string recipientEmail, string subject, string body, string filePath);
}
public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly string _senderEmail;
    private readonly string _senderPassword;

    public EmailService()
    {
        _senderEmail = Statics.SenderEmail;
        _senderPassword = Statics.SenderPassword;
        _smtpClient = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(Statics.SenderEmail, Statics.SenderPassword)
        };
    }

    public async Task SendEmailAsync(string recipientEmail, string subject, string body)
    {
        try
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_senderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(recipientEmail);
            await _smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
            throw;
        }
    }

    public async Task SendFileAsync(string recipientEmail, string subject, string body, string fileUrl)
    {
        string tempFilePath = null;
        try
        {
            // Download the file from the URL
            tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(fileUrl));

            using (HttpClient httpClient = new HttpClient())
            {
                var fileContent = await httpClient.GetByteArrayAsync(fileUrl);
                await File.WriteAllBytesAsync(tempFilePath, fileContent);
            }

            using (var mailMessage = new MailMessage
            {
                From = new MailAddress(_senderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            })
            {
                mailMessage.To.Add(recipientEmail);

                // Attach the locally downloaded file
                using (var attachment = new Attachment(tempFilePath, MediaTypeNames.Text.Html))
                {
                    mailMessage.Attachments.Add(attachment);

                    // Send the email
                    await _smtpClient.SendMailAsync(mailMessage);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
            throw;
        }
        finally
        {
            // Clean up the temporary file after sending the email
            if (tempFilePath != null && File.Exists(tempFilePath))
            {
                try
                {
                    File.Delete(tempFilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete temporary file: {ex.Message}");
                }
            }
        }
    }

}
