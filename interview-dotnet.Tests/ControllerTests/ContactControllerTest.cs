using interview_dotnet.Controllers;
using interview_dotnet.Interfaces;
using interview_dotnet.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace interview_dotnet.Tests.ControllerTests
{
    public class ContactControllerTest
    {

        public Mock<IContactRepository> _contactRepository;

        public Mock<ILogger<ContactController>> _logger;


        public ContactController _controller;

        public ContactControllerTest()
        {
            _contactRepository = new Mock<IContactRepository>();
            _logger = new Mock<ILogger<ContactController>>();
            _controller = new ContactController(_contactRepository.Object, _logger.Object);
        }


        [Fact]
        public async Task SaveContact_RepositoryReturnsResult_ReturnsSuccess()
        {
            //Arrange 
            _contactRepository.Setup(x => x.AddContact(It.IsAny<ContactRequest>()));

            //Act 
            var response = await _controller.SaveContact(new ContactRequest
            {
                FirstName = "John",
                LastName = "Smith",
                 PhoneNumber = "986467762474"
            });

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkResult>(response);
            _contactRepository.Verify(x => x.AddContact(It.IsAny<ContactRequest>()), Times.Once); 
        }
         

        [Fact]
        public async Task SaveContact_RepositoryThrowsException_ReturnsNull()
        {
            //Arrange
            _contactRepository.Setup(x => x.AddContact(It.IsAny<ContactRequest>())).Throws(new Exception());

            //Act 
            IActionResult response = null;
            try
            {
                response = await _controller.SaveContact(new ContactRequest
                {
                    FirstName = "John",
                    LastName = "Smith",
                    PhoneNumber = "986467762474"
                });
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Null(response);
                _contactRepository.Verify(x => x.AddContact(It.IsAny<ContactRequest>()), Times.Once);
            }
        }

        [Fact]
        public async Task UpdloadContact_RepositoryReturnsResult_ReturnsSuccess()
        {
            //Arrange 
            var fileName = "task2-file.csv";

            var file = AppDomain.CurrentDomain.BaseDirectory;

            var parentDirectory = Directory.GetParent(file)?.Parent?.Parent?.Parent?.FullName;

            var path = Path.Combine(parentDirectory, fileName);

            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            var iformFile = new FormFile(fileStream, 0, fileStream.Length
                , "file", fileName);
             
            //Act 
            var response = await _controller.UploadContact(iformFile);

            //Assert 
            Assert.NotNull(response);
            Assert.IsType<List<ContactRequest>>(response.Value);
            Assert.Equal(response.Value.Count(), 4);
        }
    }
}
