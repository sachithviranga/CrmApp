using CrmApp.Application.Interfaces;
using CrmApp.Server.Controllers;
using CrmApp.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CrmApp.Server.Tests
{
    /// <summary>
    /// Unit tests for CustomerController.
    /// Verifies controller actions and their responses using mocked ICustomerService.
    /// </summary>
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _mockService;
        private readonly CustomerController _controller;

        /// <summary>
        /// Initializes test class and controller with mocked service.
        /// </summary>
        public CustomerControllerTests()
        {
            _mockService = new Mock<ICustomerService>();
            _controller = new CustomerController(_mockService.Object);
        }

        /// <summary>
        /// Tests that GetAllCustomers returns OkObjectResult with correct customer list.
        /// </summary>
        [Fact]
        public async Task GetAll_ShouldReturnOk_WithCustomerList()
        {
            // Arrange
            _mockService.Setup(s => s.GetAllCustomersAsync()).ReturnsAsync(TestData.Customers);

            // Act
            var result = await _controller.GetAllCustomers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<CustomerDto>>(okResult.Value);

            Assert.NotNull(returnValue);
            Assert.Equal(TestData.Customers.Count, returnValue.Count());

            var value = returnValue.ToList();

            // Compare each property for all customers
            for (int i = 0; i < TestData.Customers.Count; i++)
            {
                var expected = TestData.Customers[i];
                var actual = value[i];
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.FirstName, actual.FirstName);
                Assert.Equal(expected.LastName, actual.LastName);
                Assert.Equal(expected.Email, actual.Email);
                Assert.Equal(expected.FullName, actual.FullName);
                Assert.Equal(expected.CreatedDate, actual.CreatedDate);
                Assert.Equal(expected.UpdatedDate, actual.UpdatedDate);
                Assert.Equal(expected.PhoneNumber, actual.PhoneNumber);
                Assert.Equal(expected.Address, actual.Address);
                Assert.Equal(expected.City, actual.City);
                Assert.Equal(expected.State, actual.State);
                Assert.Equal(expected.Country, actual.Country);
            }

            _mockService.Verify(s => s.GetAllCustomersAsync(), Times.Once);
        }

        /// <summary>
        /// Tests that GetCustomer returns OkObjectResult when customer exists.
        /// </summary>
        [Fact]
        public async Task GetById_ShouldReturnOk_WhenCustomerExists()
        {
            // Arrange
            const int id = 1;
            var customer = TestData.Customers.Find(a => a.Id == id);
            _mockService.Setup(s => s.GetCustomerByIdAsync(id)).ReturnsAsync(customer);

            // Act
            var result = await _controller.GetCustomer(id);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CustomerDto>>(result);
            var value = Assert.IsType<CustomerDto>(actionResult.Value);

            Assert.Equal(customer, value);
            _mockService.Verify(s => s.GetCustomerByIdAsync(id), Times.Once);
        }

        /// <summary>
        /// Tests that GetCustomer returns NotFoundResult when customer does not exist.
        /// </summary>
        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            const int id = 100;
            _mockService.Setup(s => s.GetCustomerByIdAsync(id)).ReturnsAsync((CustomerDto?)null);

            // Act
            var result = await _controller.GetCustomer(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            _mockService.Verify(s => s.GetCustomerByIdAsync(id), Times.Once);
        }

        /// <summary>
        /// Tests that CreateCustomer returns CreatedAtActionResult when valid.
        /// </summary>
        [Fact]
        public async Task Create_ShouldReturnCreated_WhenValid()
        {
            // Arrange
            const int id = 1;
            var request = new CreateCustomerRequest { FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com" };
            var response = TestData.Customers.Find(a => a.Id == id);

            _mockService.Setup(s => s.CreateCustomerAsync(request)).ReturnsAsync(response!);

            // Act
            var result = await _controller.CreateCustomer(request);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var value = Assert.IsType<CustomerDto>(createdResult.Value);

            Assert.Equal(response?.Id, value.Id);
            Assert.Equal(response?.FirstName, value.FirstName);
            Assert.Equal(response?.LastName, value.LastName);
            Assert.Equal(response?.Email, value.Email);
            _mockService.Verify(s => s.CreateCustomerAsync(request), Times.Once);
        }

        /// <summary>
        /// Tests that UpdateCustomer returns OkObjectResult when customer exists.
        /// </summary>
        [Fact]
        public async Task Update_ShouldReturnOk_WhenCustomerExists()
        {
            // Arrange
            const int id = 1;
            var request = new UpdateCustomerRequest { Id = id, FirstName = "Updated", LastName = "User", Email = "updated@example.com" };
            var response = new CustomerDto { Id = id, FirstName = "Updated", LastName = "User", Email = "updated@example.com" };

            _mockService.Setup(s => s.UpdateCustomerAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateCustomer(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<CustomerDto>(okResult.Value);
            Assert.Equal("Updated", value.FirstName);
            Assert.Equal("User", value.LastName);
            Assert.Equal("updated@example.com", value.Email);
            _mockService.Verify(s => s.UpdateCustomerAsync(request), Times.Once);
        }

        /// <summary>
        /// Tests that UpdateCustomer returns NotFoundResult when customer does not exist.
        /// </summary>
        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            const int id = 100;
            var request = new UpdateCustomerRequest { Id = id, FirstName = "Brown", LastName = "Garcia", Email = "Garcia@example.com" };
            _mockService.Setup(s => s.UpdateCustomerAsync(request)).ReturnsAsync((CustomerDto?)null);

            // Act
            var result = await _controller.UpdateCustomer(request);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            _mockService.Verify(s => s.UpdateCustomerAsync(request), Times.Once);
        }

        /// <summary>
        /// Tests that DeleteCustomer returns NoContentResult when deleted.
        /// </summary>
        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenDeleted()
        {
            // Arrange
            const int id = 1;
            _mockService.Setup(s => s.DeleteCustomerAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteCustomer(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.DeleteCustomerAsync(id), Times.Once);
        }

        /// <summary>
        /// Tests that DeleteCustomer returns NotFoundResult when not deleted.
        /// </summary>
        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenNotDeleted()
        {
            // Arrange
            const int id = 1;
            _mockService.Setup(s => s.DeleteCustomerAsync(id)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteCustomer(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _mockService.Verify(s => s.DeleteCustomerAsync(id), Times.Once);
        }

        /// <summary>
        /// Tests that GetPaged returns OkObjectResult with correct paged result.
        /// </summary>
        [Fact]
        public async Task GetPaged_ReturnsOkResult_WithPagedResult()
        {
            // Arrange
            const int page = 1;
            const int pageSize = 10;

            var pagedResult = new PagedResult<CustomerDto>
            {
                Items = [.. TestData.Customers.Take(pageSize)],
                TotalCount = TestData.Customers.Count,
                Page = page,
                PageSize = pageSize
            };
            _mockService.Setup(s => s.GetPagedAsync(page, pageSize)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.GetPaged(page, pageSize);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PagedResult<CustomerDto>>(okResult.Value);
            Assert.Equal(pageSize, returnValue.Items.Count);
            Assert.Equal(TestData.Customers.Count, returnValue.TotalCount);
            Assert.Equal(page, returnValue.Page);
            Assert.Equal(pageSize, returnValue.PageSize);
            _mockService.Verify(s => s.GetPagedAsync(page, pageSize), Times.Once);
        }
    }
}
