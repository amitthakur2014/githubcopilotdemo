using Xunit;
using CopilotDemo.Controllers;
using CopilotDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CopilotDemo.Test
{
    public class UserTests
    {
        private readonly UserController _userController;

        public UserTests()
        {
            _userController = new UserController();
        }

        [Fact]
        public void GetUser_WithDefaultId_ReturnsOkResultWithUser()
        {
            // Act
            var result = _userController.GetUser();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<UserModel>(okResult.Value);
            
            Assert.Equal(1, returnedUser.Id);
            Assert.Equal("Amit", returnedUser.Name);
            Assert.Equal("amit.thakur@example.com", returnedUser.Email);
        }

        [Fact]
        public void GetUser_WithSpecificId_ReturnsOkResultWithCorrectId()
        {
            // Act
            var result = _userController.GetUser(5);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<UserModel>(okResult.Value);
            
            Assert.Equal(5, returnedUser.Id);
            Assert.Equal("Amit", returnedUser.Name);
            Assert.Equal("amit.thakur@example.com", returnedUser.Email);
        }

        [Fact]
        public void GetUser_WithInvalidId_ReturnsBadRequest()
        {
            // Act
            var result = _userController.GetUser(0);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User ID must be greater than 0", badRequest.Value);
        }

        [Fact]
        public void GetUser_WithNegativeId_ReturnsBadRequest()
        {
            // Act
            var result = _userController.GetUser(-1);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User ID must be greater than 0", badRequest.Value);
        }

        [Fact]
        public void ValidateUser_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var user = new UserModel
            {
                Id = 1,
                Name = "John Doe",
                Email = "john@example.com"
            };

            // Act
            var result = _userController.ValidateUser(user);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void UserModel_WithValidData_IsValid()
        {
            // Arrange
            var user = new UserModel
            {
                Id = 1,
                Name = "John Doe",
                Email = "john@example.com"
            };
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(user, context, results, true);

            // Assert
            Assert.True(isValid);
            Assert.Empty(results);
        }

        [Fact]
        public void UserModel_WithMissingEmail_IsInvalid()
        {
            // Arrange
            var user = new UserModel
            {
                Id = 1,
                Name = "John Doe",
                Email = null
            };
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(user, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.NotEmpty(results);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("Email is required"));
        }

        [Fact]
        public void UserModel_WithInvalidEmail_IsInvalid()
        {
            // Arrange
            var user = new UserModel
            {
                Id = 1,
                Name = "John Doe",
                Email = "invalid-email"
            };
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(user, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void UserModel_WithMissingName_IsInvalid()
        {
            // Arrange
            var user = new UserModel
            {
                Id = 1,
                Name = null,
                Email = "john@example.com"
            };
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(user, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.NotEmpty(results);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("Name is required"));
        }

        [Fact]
        public void UserModel_WithShortName_IsInvalid()
        {
            // Arrange
            var user = new UserModel
            {
                Id = 1,
                Name = "A",
                Email = "john@example.com"
            };
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(user, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.NotEmpty(results);
        }
    }
}