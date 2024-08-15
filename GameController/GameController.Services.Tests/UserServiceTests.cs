using AutoMapper;
using GameController.Database.Interfaces;
using GameController.Database.Models;
using GameController.Services.Exceptions;
using GameController.Services.Models.User;
using GameController.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace GameController.Services.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        #region private fields

        private UserService _userService;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<ILogger<UserService>> _loggerMock;
        private Mock<IMapper> _mapperMock;
        private readonly Guid userId = Guid.NewGuid();
        private const string _name = "Test User";
        private const string _passwordHash = "Test User";

        #endregion

        #region setup

        [SetUp]
        public void SetupUserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<UserService>>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_userRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        #endregion

        #region tests for CreateUser
        [Test]
        public void CreateUser_ReturnUserDto_WhenUserСorrect()
        {
            // Arrange
            var user = new User { Id = userId, Name = _name, PasswordHash = _passwordHash };
            var createUserDto = new CreateUserDto { Name = _name, PasswordHash = _passwordHash };
            var userDto = new UserDto { Id = userId, Name = _name };
            _mapperMock.Setup(m => m.Map<User>(createUserDto)).Returns(user);
            _userRepositoryMock.Setup(repo => repo.Create(user)).Returns(user);
            _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(userDto);


            // Act
            var result = _userService.CreateUser(createUserDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.EqualTo(userDto));
                Assert.That(result.Id, Is.EqualTo(userDto.Id));
                Assert.That(result.Name, Is.EqualTo(userDto.Name));

                // Verify calls
                _userRepositoryMock.Verify(repo => repo.Create(user), Times.Once);
                _mapperMock.Verify(m => m.Map<User>(createUserDto), Times.Once);
                _mapperMock.Verify(m => m.Map<UserDto>(user), Times.Once);

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        #endregion

        #region tests for GetUser

        [Test]
        public void GetUser_ReturnsUserDto_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Name = _name, PasswordHash = _passwordHash };
            var userDto = new UserDto { Id = userId, Name = _name };
            _userRepositoryMock.Setup(repo => repo.Get(userId)).Returns(user);
            _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(new UserDto { Id = userId, Name = user.Name });

            // Act
            var result = _userService.GetUser(userId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(userDto.Id));
                Assert.That(result.Name, Is.EqualTo(userDto.Name));

                // Verify calls
                _userRepositoryMock.Verify(repo => repo.Get(userId), Times.Once);

                _mapperMock.Verify(m => m.Map<UserDto>(user), Times.Once);

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void GetUser_ThrowsNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.Get(userId)).Returns((User)null);

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotFoundException>(() => _userService.GetUser(userId));

                _userRepositoryMock.Verify(repo => repo.Get(userId), Times.Once);
                _userRepositoryMock.VerifyNoOtherCalls();

                _mapperMock.Verify(m => m.Map<UserDto>(It.IsAny<User>()), Times.Never);

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));
            });
        }

        #endregion

        #region tests for GetUsers

        [Test]
        public void GetUsers_ReturnsListUserDto_WhenUserExists()
        {
            // Arrange
            List<User> users = new List<User>() {new User { Id = Guid.NewGuid(), Name = _name, PasswordHash = _passwordHash } };
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Name = _name, PasswordHash = _passwordHash };
            var userDto = new UserDto { Id = userId, Name = _name };

            _userRepositoryMock.Setup(repo => repo.GetAll()).Returns(users);
            _mapperMock.Setup(m => m.Map<List<UserDto>>(users)).Returns(new List<UserDto>() { new UserDto { Id = userId, Name = user.Name }});

            // Act
            var result = _userService.GetUsers();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.EqualTo(users.Count));
                foreach (var  item in result) 
                {
                    Assert.That(item.Id, Is.EqualTo(userDto.Id));
                    Assert.That(item.Name, Is.EqualTo(userDto.Name));
                }

                // Verify calls
                _userRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);

                _mapperMock.Verify(m => m.Map<List<UserDto>>(users), Times.Once);

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        #endregion

        #region tests for UpdateUser

        [Test]
        public void UpdateUser_ReturnsUserDto_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Name = _name, PasswordHash = _passwordHash };
            var updateUserDto = new UpdateUserDto { Name = "Test User1", PasswordHash = "hashedPassword1" };

            _userRepositoryMock.Setup(repo => repo.Get(userId)).Returns(user);
            _userRepositoryMock.Setup(repo => repo.Update(user));
            _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(new UserDto { Id = userId, Name = updateUserDto.Name });

            // Act
            var result = _userService.UpdateUser(userId, updateUserDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Name, Is.EqualTo(updateUserDto.Name));

                _userRepositoryMock.Verify(repo => repo.Get(userId), Times.Once);
                _userRepositoryMock.Verify(repo => repo.Update(user), Times.Once);
                _userRepositoryMock.VerifyNoOtherCalls();

                _mapperMock.Verify(m => m.Map<UserDto>(user), Times.Once);

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void UpdateUser_ThrowsNotModifiedException_WhenDataIsNotModified()
        {
            // Arrange
            var user = new User { Id = userId, Name = "Test User", PasswordHash = "hashedPassword" };
            var updateUserDto = new UpdateUserDto { Name = "Test User", PasswordHash = "hashedPassword" };

            _userRepositoryMock.Setup(repo => repo.Get(userId)).Returns(user);

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotModifiedException>(() => _userService.UpdateUser(userId, updateUserDto));

                _userRepositoryMock.Verify(repo => repo.Get(userId), Times.Once);
                _userRepositoryMock.VerifyNoOtherCalls();

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        #endregion

        #region tests for DeleteUser

        [Test]
        public void DeleteUser_ReturnsTrue_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.Delete(userId)).Returns(true);

            // Act
            var result = _userService.DeleteUser(userId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);

                _userRepositoryMock.Verify(repo => repo.Delete(userId), Times.Once);
                _userRepositoryMock.VerifyNoOtherCalls();

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void DeleteUser_ReturnsFalse_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.Delete(userId)).Returns(false);

            // Act
            var result = _userService.DeleteUser(userId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.False);

                _userRepositoryMock.Verify(repo => repo.Delete(userId), Times.Once);
                _userRepositoryMock.VerifyNoOtherCalls();

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        #endregion
    }
}
