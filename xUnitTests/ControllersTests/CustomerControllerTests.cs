using CustomerMVC.Controllers;
using CustomerMVC.Models;
using CustomerMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace xUnitTests.Controllers
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _mockService;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            _mockService = new Mock<ICustomerService>();
            _controller = new CustomerController(_mockService.Object);
        }

        private Customer GetValidCustomer() => new Customer
        {
            FirstName = "Name",
            LastName = "Lastname",
            Email = "mail@gmail.com",
            Phone = "+380123456789",
            Address = "Street"
        };

        private List<Customer> GetMockCustomers() => new List<Customer>
        {
            new Customer { FirstName = "F1", LastName = "L1", Email = "f1@gmail.com" },
            new Customer { FirstName = "F2", LastName = "L2", Email = "f2@gmail.com" }
        };

        [Fact]
        public void Index_ReturnsViewResult_WithListOfCustomers()
        {
            var mockCustomers = GetMockCustomers();
            _mockService.Setup(service => service.GetAllCustomers()).Returns(mockCustomers);

            var result = _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Customer>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            var result = _controller.Create();

            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Post_ValidCustomer_RedirectsToIndex()
        {
            var customer = GetValidCustomer();
            _mockService.Setup(service => service.AddCustomer(It.IsAny<Customer>())).Returns(customer);

            var result = _controller.Create(customer);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void Create_Post_InvalidCustomer_ReturnsViewWithModel()
        {
            var customer = new Customer();
            _controller.ModelState.AddModelError("FirstName", "First name is required.");

            var result = _controller.Create(customer);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Customer>(viewResult.Model);
            Assert.Equal(customer, model);
        }

        [Fact]
        public void Create_Post_CustomerThrowsException_ReturnsViewWithErrorMessage()
        {
            var customer = GetValidCustomer();
            _mockService.Setup(service => service.AddCustomer(It.IsAny<Customer>())).Throws(new ArgumentException("An error occurred"));

            var result = _controller.Create(customer);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Customer>(viewResult.Model);
            Assert.Equal(customer, model);
            Assert.Equal("An error occurred", _controller.ModelState[""].Errors[0].ErrorMessage);
        }
    }
}
