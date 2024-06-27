using GameController.Services.Interfaces;
using GameController.Services.Models.User;
using Microsoft.Extensions.Logging;

namespace GameController.Services.Services
{
    /// <summary>
    /// Class for working with users.
    /// </summary>
    public class UserService : IUserService
    {
        #region private fields

        private readonly ILogger<UserService> _logger;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserService(
            ILogger<UserService> logger)
        {
            _logger = logger;
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public UserDto CreateUser(CreateUserDto createUserDto)
        {
            _logger.LogInformation("Create user");

            return new()
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };
        }

        /// <inheritdoc/>
        public UserDto GetUser(Guid userId)
        {
            _logger.LogInformation("Get user by ID {userId}", userId);

            return new()
            {
                Id = userId,
                Name = "Test"
            };
        }

        /// <inheritdoc/>
        public UserDto UpdateUser(Guid userId, UpdateUserDto updateUserDto)
        {
            _logger.LogInformation(
                "Update user with ID {userId} with request {updateUserDto}",
                userId,
                updateUserDto);

            return new()
            {
                Id = userId,
                Name = updateUserDto.Name
            };
        }

        /// <inheritdoc/>
        public void DeleteUser(Guid userId)
        {
            _logger.LogInformation("Delete user with ID {userId}", userId);
        }

        #endregion
    }
}
