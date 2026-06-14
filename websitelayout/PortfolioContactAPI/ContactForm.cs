using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Communication.Email;

namespace PortfolioContactAPI;

public class ContactForm
{
    private readonly ILogger<ContactForm> _logger;

    public ContactForm(
        ILogger<ContactForm> logger)
    {
        _logger = logger;
    }

    [Function("ContactForm")]
    public async Task<IActionResult> Run(
        [HttpTrigger(
            AuthorizationLevel.Anonymous,
            "post")]
        HttpRequest req)
    {
        _logger.LogInformation(
            "Processing contact form.");

        string requestBody =
            await new StreamReader(req.Body)
                .ReadToEndAsync();

                ContactRequest? contact =
            JsonSerializer.Deserialize<ContactRequest>(
                requestBody);

        if (contact == null)
        {
            return new BadRequestObjectResult(
                "Invalid request.");
        }

        string connectionString = 
            Environment.GetEnvironmentVariable(
                "ACS_CONNECTION_STRING");

        var emailClient =
            new EmailClient(connectionString);  

        var emailMessage = 
            new EmailMessage(
                senderAddress:
                    "DoNotReply@c7f314d0-8b9d-4081-bcb2-8713367c5973.azurecomm.net",
                    content:
                        new EmailContent(
                            $"Portfolio Contact From {contact.Name}")
                        {
                            PlainText =
$"""
Name: {contact.Name} 

Email: {contact.Email}

Message
{contact.Message}
"""
                        },
                    recipients:  
                    new EmailRecipients(
                        [
                            new EmailAddress(
                                "brandonmorsework@gmail.com")
                    
                        
            ])); 

        await emailClient.SendAsync(
            Azure.WaitUntil.Completed,
            emailMessage);

        _logger.LogInformation(
            $"Name: {contact.Name}");

        _logger.LogInformation(
            $"Email: {contact.Email}");

        _logger.LogInformation(
            $"Message: {contact.Message}");

        return new OkObjectResult(
            $"Message received from {contact.Name}");
    }
}