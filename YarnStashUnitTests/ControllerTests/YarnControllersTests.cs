using System;
using Xunit;
using YarnStash.Controllers;
using YarnStash.Data;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace YarnStashUnitTests.ControllerTests
{
    public class YarnControllersTests
    {
        private readonly YarnController _yarnController;

        public YarnControllersTests()
        {
            var contextOptions = new DbContextOptionsBuilder<YarnContext>().UseInMemoryDatabase("inMemoryDatabase").Options;
            var context = new YarnContext(contextOptions);
            _yarnController = new YarnController(context);
        }

        [Fact]
        public async void Details_NullID_ReturnNoContent()
        {
            //Arrange

            //Act
            var result = await _yarnController.Details(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
