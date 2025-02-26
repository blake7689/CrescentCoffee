using CrescentCoffee.Controllers;
using CrescentCoffee.Controllers.Api;
using CrescentCoffee.ViewModels;
using CrescentCoffeeTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrescentCoffeeTests.Controllers
{
    public class SearchControllerTests
    {
        [Fact]
        public void GetAll_ReturnsAllCoffees()
        {
            //arrange
            var mockCoffeeRepository = RepositoryMocks.GetCoffeeRepository();
            var searchController = new SearchController(mockCoffeeRepository.Object);

            //act
            var result = searchController.GetAll();

            //assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.NotNull(okObjectResult.Value);
            var coffeList = Assert.IsType<List<CrescentCoffee.Models.Coffee>>(okObjectResult.Value);
            Assert.Equal(3, coffeList.Count);
        }

        [Fact]
        public void GetById_InvalidCoffeeId_ReturnsNotFound()
        {
            //arrange
            int id = 99;
            var mockCoffeeRepository = RepositoryMocks.GetCoffeeRepository();
            var searchController = new SearchController(mockCoffeeRepository.Object);

            //act
            var result = searchController.GetById(id);

            //assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetById_ValidCoffeeId_ReturnsOk()
        {
            //arrange
            int id = 1;
            var mockCoffeeRepository = RepositoryMocks.GetCoffeeRepository();
            var searchController = new SearchController(mockCoffeeRepository.Object);

            //act
            var result = searchController.GetById(id);

            //assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.NotNull(okObjectResult.Value);


            var coffeeList = okObjectResult.Value;
            Assert.NotNull(coffeeList);
            var json = coffeeList.ToJson();
            Assert.Contains("CoffeeId\":1", json);
        }
    }
}
