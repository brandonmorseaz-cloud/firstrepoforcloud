using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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