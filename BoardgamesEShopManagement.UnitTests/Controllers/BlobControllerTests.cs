using BoardgamesEShopManagement.API.Controllers;
using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BoardgamesEShopManagement.UnitTests.Controllers
{
    public class BlobControllerTests : CustomControllerBaseMock
    {
        private readonly IBlobService _blobServiceMock;
        private readonly BlobController _controller;

        public BlobControllerTests()
        {
            _blobServiceMock = Substitute.For<IBlobService>();
            _controller = new BlobController(_blobServiceMock)
            {
                ControllerContext = ControllerContext
            };
        }

        [Fact]
        public async Task UploadFile_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var fileModel = new FileModel
            {
                FileName = "test.txt",
                FormFile = new FormFile(new MemoryStream(), 0, 0, "test.txt", "test.txt")
            };

            _controller.ModelState.AddModelError("FormFile", "Required");

            // Act
            var result = await _controller.UploadFile(fileModel);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetFile_NonExistingBlob_ReturnsNotFoundResult()
        {
            // Arrange
            _blobServiceMock.GetBlobAsync(Arg.Any<string>()).Returns((BlobInfo)null);

            // Act
            var result = await _controller.GetFile("test.txt");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteFile_ExistingBlob_ReturnsOkResult()
        {
            // Arrange
            _blobServiceMock.DeleteBlobAsync(Arg.Any<string>()).Returns(true);

            // Act
            var result = await _controller.DeleteFile("test.txt");

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteFile_NonExistingBlob_ReturnsNotFoundResult()
        {
            // Arrange
            _blobServiceMock.DeleteBlobAsync(Arg.Any<string>()).Returns(false);

            // Act
            var result = await _controller.DeleteFile("test.txt");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}