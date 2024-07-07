using AutoMapper;
using GameController.Database.Interfaces;
using GameController.Database.Models;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserService(
            ILogger<UserService> logger, IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _userRepository = userRepository;
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public UserDto CreateUser(CreateUserDto createUserDto)
        {
            _logger.LogInformation("Create user");

            User user = _mapper.Map<User>(createUserDto);
            User dbUser = _userRepository.Create(user);

            return _mapper.Map<UserDto>(dbUser);
        }

        /// <inheritdoc/>
        public UserDto GetUser(Guid userId)
        {
            _logger.LogInformation("Get user by ID {userId}", userId);

            User? user = _userRepository.Get(userId);

            if (user == null)
            {
                throw new Exception($"User {userId} not found.");
            }
            else
            {
                return _mapper.Map<UserDto>(user);
            }
        }

        /// <inheritdoc/>
        public UserDto UpdateUser(Guid userId, UpdateUserDto updateUserDto)
        {
            _logger.LogInformation(
                "Update user with ID {userId} with request {updateUserDto}",
                userId,
                updateUserDto);

            User user = _mapper.Map<User>(updateUserDto);
            user.Id = userId;
            User dbUser = _userRepository.Update(user);

            return _mapper.Map<UserDto>(dbUser);
        }

        /// <inheritdoc/>
        public void DeleteUser(Guid userId)
        {
            _logger.LogInformation("Delete user with ID {userId}", userId);

            _userRepository.Delete(userId);
        }

        #endregion
    }
}
