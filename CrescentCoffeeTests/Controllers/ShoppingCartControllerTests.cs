using CrescentCoffeeTests.Mocks;
using CrescentCoffee.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrescentCoffee.ViewModels;
using CrescentCoffee.Models;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using CrescentCoffee.Migrations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CrescentCoffeeTests.Controllers
{
    public class ShoppingCartControllerTests
    {
        [Fact]
        public void Index_ReturnsShoppingCart()
        {
            //arrange
            var mockCoffeeRepository = RepositoryMocks.GetCoffeeRepository();
            var mockShoppingCart = RepositoryMocks.GetShoppingCart();

            var shoppingCartController = new ShoppingCartController(mockCoffeeRepository.Object, mockShoppingCart.Object);

            //act
            var result = shoppingCartController.Index();

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var shoppingViewModel = Assert.IsAssignableFrom<ShoppingCartViewModel>(viewResult.ViewData.Model);
            Assert.Equal(19.99M, shoppingViewModel.ShoppingCartTotal);
            Assert.Equal(1, shoppingViewModel.ShoppingCart.ShoppingCartItemCount);
            var cartItem = Assert.Single(shoppingViewModel.ShoppingCart.ShoppingCartItems);
            Assert.Equal(1, cartItem.Coffee.CoffeeId);
        }

        [Fact]
        public void ClearShoppingCart_ReturnsClearedCart()
        {
            //arrange
            var mockCoffeeRepository = RepositoryMocks.GetCoffeeRepository();
            var mockShoppingCart = RepositoryMocks.GetShoppingCart();

            var shoppingCartController = new ShoppingCartController(mockCoffeeRepository.Object, mockShoppingCart.Object);

            //act
            var result = (RedirectToActionResult)shoppingCartController.ClearShoppingCart();

            //assert
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void RemoveToShoppingCart_InvalidCoffeeId_ReturnsRedirect()
        {
            //arrange
            int id = 99;
            var mockCoffeeRepository = RepositoryMocks.GetCoffeeRepository();
            var mockShoppingCart = RepositoryMocks.GetEmptyShoppingCart();

            var shoppingCartController = new ShoppingCartController(mockCoffeeRepository.Object, mockShoppingCart.Object);

            //act
            var result = (RedirectToActionResult)shoppingCartController.RemoveFromShoppingCart(id);

            //assert
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }
    }
}
