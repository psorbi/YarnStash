using System;
using Xunit;
using YarnStash.Controllers;
using YarnStash.Data;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YarnStashUnitTests.DataFixtures;

namespace YarnStashUnitTests.ControllerTests
{
    public class YarnControllersTests : IClassFixture<YarnSeedDataFixture>
    {
        private readonly YarnController _yarnController;

        public YarnControllersTests(YarnSeedDataFixture yarnSeed)
        {
            //creating new yarn controller to test
            _yarnController = new YarnController(yarnSeed.context);
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

        [Fact]
        public async void Details_IDNotFound_ReturnNotFound()
        {
            //Arrange
            int nonexistID = 3;

            //Act
            var result = await _yarnController.Details(nonexistID);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Details_validID_ReturnView()
        {
            //Arrange
            int existingID = 2;

            //Act
            var result = await _yarnController.Details(existingID);

            //Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
