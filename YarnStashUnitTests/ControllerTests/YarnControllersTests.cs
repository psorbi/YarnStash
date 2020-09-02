using System;
using Xunit;
using YarnStash.Controllers;
using YarnStash.Data;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YarnStashUnitTests.DataFixtures;
using System.Linq;
using YarnStash.Models;
using YarnStash.Interfaces;

namespace YarnStashUnitTests.ControllerTests
{
    public class YarnControllersTests : IClassFixture<YarnSeedDataFixture>
    {
        private readonly YarnController _yarnController;
        private readonly Mock<ISearchServices> _mockSearchServices;
        private readonly YarnContext _yarnContext;

        public YarnControllersTests(YarnSeedDataFixture yarnSeed)
        {
            //create the _mockSearchServies
            _mockSearchServices = new Mock<ISearchServices>();

            //creating new yarn controller to test
            _yarnController = new YarnController(yarnSeed.context, _mockSearchServices.Object);

            _yarnContext = yarnSeed.context;

        }


        //-----Index-----

        [Fact]
        public void Index_GoodCall_ReturnView()
        {
            //Act
            var result = _yarnController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }
        
        //-----Search-----

        [Fact]
        public async void Search_MatchFound_ReturnView()
        {
            //Arrange
            string searchString = "Berroco";

            //Act
            var result = await _yarnController.Search(searchString);

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Search_NoMatchFound_ReturnView()
        {
            //Arrange
            string searchString = "Lion";

            //Act
            var result = await _yarnController.Search(searchString);

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Search_EmptySearch_ReturnView()
        {
            //Arrange
            string searchString = "";

            //Act
            var result = await _yarnController.Search(searchString);

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Search_NullSearch_ReturnBadRequest()
        {
            //Arrange
            string searchString = null;

            //Act
            var result = await _yarnController.Search(searchString);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }


        //-----Details-----

        [Fact]
        public async void Details_NullID_ReturnBadRequest()
        {
            //Arrange

            //Act
            var result = await _yarnController.Details(null);

            //Assert
            Assert.IsType<BadRequestResult>(result);
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


        //-----Create-----

        [Fact]
        public async void Create_ValidModel_ReturnRedirectToAction()
        {
            //Arrange
            YarnModel testModel = new YarnModel();
            testModel.Manufacturer = "Test Yarn";
            testModel.Name = "Yarny";
            testModel.Amount = 100;
            testModel.Color = "black";
            testModel.Size = SizeEnum.Aran;

            //Act
            var result = await _yarnController.Create(testModel);

            //Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void Create_InvalidModel_ReturnView()
        {
            //Arrange
            YarnModel testModel = null;
            
            //Act
            var result = await _yarnController.Create(testModel);

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        //----- Edit with one arguement -----

        [Fact]
        public async void Edit_NullID_ReturnBadRequest()
        {
            //Act
            var result = await _yarnController.Edit(null);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Edit_IDNotFound_ReturnNotFound()
        {
            //Arrange
            int nonexistID = 3;

            //Act
            var result = await _yarnController.Edit(nonexistID);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Edit_ValidID_ReturnView()
        {
            //Arrange
            int existingID = 2;

            //Act
            var result = await _yarnController.Edit(existingID);

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        //----- Edit with 2 arguements -----
        [Fact]
        public async void Edit_WrongID_ReturnNotFound()
        {
            //Arrange
            YarnModel testModel = new YarnModel();
            testModel.id = 35;
            int wrongID = 101;

            //Act
            var result = await _yarnController.Edit(wrongID, testModel);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Edit_NonexistantYarnModel_ReturnNotFound()
        {
            //Arrange
            YarnModel testModel = new YarnModel();
            int nonExistID = 102;

            //Act
            var result = await _yarnController.Edit(nonExistID, testModel);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Edit_ValidYarnModel_ReturnRedirectToAction()
        {
            //Arrange
            YarnModel testModel = await _yarnContext.Yarn.FirstOrDefaultAsync(m => m.id == 1);
            int goodID = 1;

            //Act
            var result = await _yarnController.Edit(goodID, testModel);

            //Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void Edit_InvalidModel_ReturnView()
        {
            //Arrange
            YarnModel testModel = new YarnModel();
            testModel.id = 66;
            testModel.Manufacturer = null;
            int validID = 66;

            //Act
            var result = await _yarnController.Edit(validID, testModel);

            //Assert
            Assert.IsType<ViewResult>(result);
        }
        


        //-----Delete----

        [Fact]
        public async void Delete_NullID_ReturnBadRequest()
        {
            //Act
            var result = await _yarnController.Delete(null);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Delete_IDNotFound_ReturnNotFound()
        {
            //Arrange
            int nonexistID = 100;

            //Act
            var result = await _yarnController.Delete(nonexistID);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Delete_ValidID_ReturnView()
        {
            //Arrange
            int existingID = 2;

            //Act
            var result = await _yarnController.Delete(existingID);

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void DeleteConfirmed_ValidID_ReturnRedirectToAction()
        {
            //Arrange
            YarnModel testModel = new YarnModel();
            testModel.Manufacturer = "All Yarn";
            testModel.Name = "Softy";
            testModel.Amount = 200;
            testModel.Color = "yellow";
            testModel.Size = SizeEnum.Lace;
            testModel.id = 20;
            _yarnContext.Yarn.Add(testModel);
            int validID = 20;

            //Act
            var result = await _yarnController.DeleteConfirmed(validID);

            //Assert
            Assert.IsType<RedirectToActionResult>(result);

        }


    }
}
