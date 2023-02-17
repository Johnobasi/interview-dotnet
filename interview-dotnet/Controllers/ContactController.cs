using CsvHelper;
using CsvHelper.Configuration;
using interview_dotnet.Interfaces;
using interview_dotnet.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace interview_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        private readonly ILogger<ContactController> _logger;

        public ContactController(IContactRepository contactRepository, ILogger<ContactController> logger)
        {
            _contactRepository = contactRepository;
            _logger = logger;
        }

        [HttpPost("Contact")]
        public async Task<IActionResult> SaveContact([FromBody] ContactRequest contactRequest)
        {
            _logger.LogInformation($"Creating new contact with details {contactRequest}");

            await _contactRepository.AddContact(contactRequest);

            _logger.LogInformation($"Created new contact with details {contactRequest}");

            return Ok();
        }

        [HttpPost("uploadContact")]
        public async Task<ActionResult<IEnumerable<ContactRequest>>> UploadContact([FromForm] IFormFile file)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            List<ContactRequest> output = new();

            using var fileStream = file.OpenReadStream();
            using var reader = new StreamReader(fileStream);
            using var csvReader = new CsvReader(reader, config);

            output = csvReader.GetRecords<ContactRequest>().ToList();

            await Parallel.ForEachAsync(output, async (x, token) =>
            {
                _logger.LogInformation($"{x}");
                await Task.CompletedTask;
            });

            return output;
        }
    }
}
