using CrescentCoffee.Controllers;
using CrescentCoffee.Models;
using CrescentCoffee.ViewModels;
using CrescentCoffeeTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrescentCoffeeTests.Controllers
{
    public class OrderControllerTests
    {
        [Fact]
        public void Checkout_EmptyOrder_ReturnsEmptyCart()
        {
            //arrange
            var mockOrderRepository = RepositoryMocks.GetOrderRepository();
            var mockShoppingCart = RepositoryMocks.GetEmptyShoppingCart();

            var orderController = new OrderController(mockOrderRepository.Object, mockShoppingCart.Object);

            //act
            orderController.Checkout(new Order());

            //assert
            Assert.True(orderController.ModelState.ErrorCount > 0);
            var modelState = orderController.ModelState.Values.FirstOrDefault();
            Assert.NotNull(modelState);
            Assert.Equal("Your cart is empty", modelState.Errors[0].ErrorMessage);
            Assert.True(!orderController.ModelState.IsValid);
        }

        [Fact]
        public void Checkout_HasOrder_ReturnsRedirect()
        {
            //arrange
            var mockOrderRepository = RepositoryMocks.GetOrderRepository();
            var mockShoppingCart = RepositoryMocks.GetShoppingCart();

            var orderController = new OrderController(mockOrderRepository.Object, mockShoppingCart.Object);

            //act
            var result = (RedirectToActionResult)orderController.Checkout(new Order());

            //assert
            Assert.Equal("CheckoutComplete", result.ActionName);
        }

        [Fact]
        public void CheckoutComplete_ReturnsOrderComplete()
        {
            //arrange
            var mockOrderRepository = RepositoryMocks.GetOrderRepository();
            var mockShoppingCart = RepositoryMocks.GetShoppingCart();

            var orderController = new OrderController(mockOrderRepository.Object, mockShoppingCart.Object);

            //act
            var result = orderController.CheckoutComplete();

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.ViewData);
            Assert.True(viewResult.ViewData.ContainsKey("CheckoutCompleteMessage"));
            Assert.Equal("Order Complete!", viewResult.ViewData["CheckoutCompleteMessage"]);

        }
    }
}
