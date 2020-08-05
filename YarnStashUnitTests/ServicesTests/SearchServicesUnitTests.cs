using System;
using System.Linq;
using Xunit;
using YarnStash.Interfaces;
using YarnStash.Models;
using YarnStash.Services;
using YarnStashUnitTests.DataFixtures;

namespace YarnStashUnitTests.ServicesTests
{
    public class SearchServicesUnitTests : IClassFixture<SearchServicesDataFixture>
    {
        private readonly IQueryable<YarnModel> _yarns;
        private readonly ISearchServices _searchServices;

        public SearchServicesUnitTests(SearchServicesDataFixture yarnSeed)
        {
            //queryable list of yarns
            _yarns = from y in yarnSeed.context.Yarn select y;

            _searchServices = new SearchServices();
        }



        [Fact]
        public void SortYarn_SortOrderManDesc_ReturnSortList()
        {
            //Arrange
            var expected = _yarns.OrderByDescending(y => y.Manufacturer.ToLower());

            //Act
            var result = _searchServices.SortYarn(_yarns, "manufacturer_desc");

            //Assert - compare expected list to result list
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SortYarn_SortOderNameAsc_ReturnSortList()
        {
            //Arrange
            var expected = _yarns.OrderBy(y => y.Name.ToLower());

            //Act
            var result = _searchServices.SortYarn(_yarns, "Name");

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SortYarn_SortOrderNameDesc_ReturnSortList()
        {
            //Arrange
            var expected = _yarns.OrderByDescending(y => y.Name.ToLower());

            //Act
            var result = _searchServices.SortYarn(_yarns, "name_desc");

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SortYarn_SortOrderAmountAsc_ReturnSortList()
        {
            //Arrange
            var expected = _yarns.OrderBy(y => y.Amount);

            //Act
            var result = _searchServices.SortYarn(_yarns, "Amount");

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SortYarn_SortOrderAmountDesc_ReturnSortList()
        {
            //Arrange
            var expected = _yarns.OrderByDescending(y => y.Amount);

            //Act
            var result = _searchServices.SortYarn(_yarns, "amount_desc");

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SortYarn_SortOrderSizeAsc_ReturnSortList()
        {
            //Arrange
            var expected = _yarns.OrderBy(y => y.Size);

            //Act
            var result = _searchServices.SortYarn(_yarns, "Size");

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SortYarn_SortOrderSizeDesc_ReturnSortList()
        {
            //Arrange
            var expected = _yarns.OrderByDescending(y => y.Size);

            //Act
            var result = _searchServices.SortYarn(_yarns, "size_desc");

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SortYarn_SortOrderManAsc_ReturnSortList()
        {
            //Arrange
            var expected = _yarns.OrderBy(y => y.Manufacturer);

            //Act
            var result = _searchServices.SortYarn(_yarns, null);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SearchByInput_SuccessfulSearch_ReturnFoundItem()
        {
            //Arrange
            string searchString = "Name 1";
            IQueryable<YarnModel> expected = _yarns.Where(x => x.Name == searchString);
           
            //Act
            var result = _searchServices.SearchByInput(_yarns, searchString);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
