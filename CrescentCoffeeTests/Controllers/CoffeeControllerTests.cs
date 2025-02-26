using CrescentCoffee.Controllers;
using CrescentCoffee.ViewModels;
using CrescentCoffeeTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Xunit.Assert;

namespace CrescentCoffeeTests.Controllers
{
    public class CoffeeControllerTests
    {
        [Fact]
        public void CoffeeList_EmptyCoffeeList_ReturnsAllCoffees()
        {
            //arrange
            var mockCoffeeRepository = RepositoryMocks.GetCoffeeRepository();
            var mockCoffeeTypeRepository = RepositoryMocks.GetCoffeeTypeRepository();

            var coffeeController = new CoffeeController(mockCoffeeRepository.Object, mockCoffeeTypeRepository.Object);

            //act
            var result = coffeeController.CoffeeList("");

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var coffeeListViewModel = Assert.IsAssignableFrom<CoffeeListViewModel>(viewResult.ViewData.Model);
            Assert.Equal(3, coffeeListViewModel.Coffees.Count());
        }

        [Fact]
        public void CoffeeList_InvalidCoffeType_ReturnsCoffeesAllCoffees()
        {
            //arrange
            var mockCoffeeRepository = RepositoryMocks.GetCoffeeRepository();
            var mockCoffeeTypeRepository = RepositoryMocks.GetCoffeeTypeRepository();

            var coffeeController = new CoffeeController(mockCoffeeRepository.Object, mockCoffeeTypeRepository.Object);

            //act
            var result = coffeeController.CoffeeList("Folgers");

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var coffeeListViewModel = Assert.IsAssignableFrom<CoffeeListViewModel>(viewResult.ViewData.Model);
            Assert.Equal(3, coffeeListViewModel.Coffees.Count());
        }

        [Fact]
        public void CoffeeList_ValidCoffeType_ReturnsCoffeesOfType()
        {
            //arrange
            var mockCoffeeRepository = RepositoryMocks.GetCoffeeRepository();
            var mockCoffeeTypeRepository = RepositoryMocks.GetCoffeeTypeRepository();

            var coffeeController = new CoffeeController(mockCoffeeRepository.Object, mockCoffeeTypeRepository.Object);

            //act
            var result = coffeeController.CoffeeList("Arabica");

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var coffeeListViewModel = Assert.IsAssignableFrom<CoffeeListViewModel>(viewResult.ViewData.Model);
            Assert.Equal(2, coffeeListViewModel.Coffees.Count());
        }

        

        [Fact]
        public void Details_HasInvalidCoffeeId_ReturnsNotFound()
        {
            //arrange
            int id = 99;
            var mockCoffeeRepository = RepositoryMocks.GetCoffeeRepository(id);
            var mockCoffeeTypeRepository = RepositoryMocks.GetCoffeeTypeRepository();

            var coffeeController = new CoffeeController(mockCoffeeRepository.Object, mockCoffeeTypeRepository.Object);

            //act
            var result = coffeeController.Details(id);

            //assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [Fact]
        public void Details_HasValidCoffeeId_ReturnsDetails()
        {
            //arrange
            int id = 2;
            string name = "Moon Beans";
            var mockCoffeeRepository = RepositoryMocks.GetCoffeeRepository(id);
            var mockCoffeeTypeRepository = RepositoryMocks.GetCoffeeTypeRepository();

            var coffeeController = new CoffeeController(mockCoffeeRepository.Object, mockCoffeeTypeRepository.Object);
            
            //act
            var result = coffeeController.Details(id);

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var DynamicModels = Assert.IsAssignableFrom<System.Dynamic.ExpandoObject>(viewResult.ViewData.Model);
            var coffeeModel = DynamicModels.FirstOrDefault().Value as CrescentCoffee.Models.Coffee;
            Assert.NotNull(coffeeModel);
            Assert.Equal(name, coffeeModel.Name);
            Assert.Equal(id, coffeeModel.CoffeeId);
        }
    }
}
