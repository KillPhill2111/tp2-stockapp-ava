using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Domain.Test
{
    public class TokenControllerTests
    {
        private readonly Mock<IJwAuthManager> _mockJwtAuthManager;

        private readonly TokenControllerTests _tokenControllerTests;
        public TokenControllerTests()
        {
            _mockJwtAuthManager = new Mock<IJwAuthManager>();
            _tokenControllerTests = new TokenControllerTests(_mockJwtAuthManager.Object);
        }
        [Fact]
        public void Authenticate_ReturnsOkResult_WithToken()
        {
            // Arrange
            var userLogin = new UserLogin { Username = "test", Password = "password" };
            var token = "test-token";
            _mockJwtAuthManager.Setup(m => m.GenerateToken(userLogin.Username)).Returns(token);

            // Act
            var result = _controller.Authenticate(userLogin);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnToken = Assert.IsType<string>(okResult.Value.GetType().GetProperty("Token").GetValue(okResult.Value));
            Assert.Equal(token, returnToken);
        }

        [Fact]
        public void Authenticate_ReturnsUnauthorizedResult_WhenCredentialsAreInvalid()
        {
            // Arrange
            var userLogin = new UserLogin { Username = "invalid", Password = "invalid" };

            // Act
            var result = _controller.Authenticate(userLogin);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
