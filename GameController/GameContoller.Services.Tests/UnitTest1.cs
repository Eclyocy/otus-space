using GameContoller.Services.Tests.Mocks;
using GameController.Database.Interfaces;
using Moq;
using Xunit;

namespace GameContoller.Services.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<IUserRepository> _userRepository;

        [TestMethod]
        public void TestMethod1()
        {
            var userId = Guid.NewGuid();
            _userRepository.Setup(s=>s.Get(userId)).Returns(true);

           
            var mockUserService = new UserServiceMock();

            // Act
            var userDto = mockUserService.GetUser(userId);

            // Assert
            Assert.IsNotNull(userDto);
            Assert.Equals(userId, userDto.Id);
            Assert.Equals("MockName", userDto.Name);
        }

        [Fact]
        public void GetUser_WithExistingUser_ReturnsUserDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mockUserService = new UserServiceMock();

            // Act
            var userDto = mockUserService.GetUser(userId);

            // Assert
            Assert.IsNotNull(userDto);
            Assert.Equals(userId, userDto.Id);
            Assert.Equals("MockName", userDto.Name);
        }
    }
}
