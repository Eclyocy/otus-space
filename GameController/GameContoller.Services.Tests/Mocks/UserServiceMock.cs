
using AutoMapper;
using GameController.Database.Interfaces;
using GameController.Database.Models;
using GameController.Services.Interfaces;
using GameController.Services.Models.User;
using Moq;

namespace GameContoller.Services.Tests.Mocks
{
    public class UserServiceMock : IUserService
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;

        public UserServiceMock()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        public UserDto GetUser(Guid userId)
        {
            User? user = new User
            {
                Id = userId,
                Name = "MockName",
                PasswordHash = "qwe-erty-12ty-MOCK",
                Sessions = [new Session { Id = new Guid(), UserId = userId, ShipId = new Guid(), GeneratorId = new Guid()}]
            };

            if (user != null)
            {
                _mockUserRepository.Setup(m => m.Get(userId)).Returns(user);
            }
            else
            {
                _mockUserRepository.Setup(m => m.Get(userId)).Throws<ArgumentNullException>();
            }

            return _mockMapper.Object.Map<UserDto>(user);
        }

        UserDto IUserService.CreateUser(CreateUserDto createUserDto)
        {
            throw new NotImplementedException();
        }

        bool IUserService.DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        UserDto IUserService.GetUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        List<UserDto> IUserService.GetUsers()
        {
            throw new NotImplementedException();
        }

        UserDto IUserService.UpdateUser(Guid userId, UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}
