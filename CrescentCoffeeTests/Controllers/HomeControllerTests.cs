using CrescentCoffee.Controllers;
using CrescentCoffee.Models;
using CrescentCoffee.ViewModels;
using CrescentCoffeeTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrescentCoffeeTests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsCoffeesOfTheWeek()
        {
            //arrange
            var mockCoffeeRepository = RepositoryMocks.GetCoffeeRepository();
            var homeController = new HomeController(mockCoffeeRepository.Object);

            //act
            var result = homeController.Index();

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var homeViewModel = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Equal(2, homeViewModel.CoffeesOfTheWeek.Count());
        }
    }
}
