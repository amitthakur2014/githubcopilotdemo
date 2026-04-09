using Xunit;
using CopilotDemo.Controllers;

namespace CopilotDemo.Test
{
    public class UserTests
    {
        [Fact]
        public void ValidateEmail_WithValidEmail_ReturnsTrue()
        {
            // Arrange
            string validEmail = "test@example.com";

            // Act
            bool result = UserController.ValidateEmail(validEmail);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateEmail_WithInvalidEmail_ReturnsFalse()
        {
            // Arrange
            string invalidEmail = "invalid-email";

            // Act
            bool result = UserController.ValidateEmail(invalidEmail);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateEmail_WithEmptyEmail_ReturnsFalse()
        {
            // Arrange
            string emptyEmail = "";

            // Act
            bool result = UserController.ValidateEmail(emptyEmail);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateEmail_WithNullEmail_ReturnsFalse()
        {
            // Arrange
            string? nullEmail = null;

            // Act
            bool result = UserController.ValidateEmail(nullEmail ?? "");

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("user@domain.com")]
        [InlineData("test.email@domain.co.uk")]
        [InlineData("firstname.lastname@domain.com")]
        public void ValidateEmail_WithMultipleValidFormats_ReturnsTrue(string email)
        {
            // Act
            bool result = UserController.ValidateEmail(email);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("user@domain")]
        [InlineData("user.domain.com")]
        [InlineData("@domain.com")]
        [InlineData("user@.com")]
        public void ValidateEmail_WithMultipleInvalidFormats_ReturnsFalse(string email)
        {
            // Act
            bool result = UserController.ValidateEmail(email);

            // Assert
            Assert.False(result);
        }
    }
}