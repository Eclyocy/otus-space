using AutoMapper;
using GameController.Database.Interfaces;
using GameController.Database.Models;
using GameController.Services.Exceptions;
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

        private readonly IUserRepository _userRepository;

        private readonly ILogger<UserService> _logger;

        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserService(
            IUserRepository userRepository,
            ILogger<UserService> logger,
            IMapper mapper)
        {
            _userRepository = userRepository;

            _logger = logger;

            _mapper = mapper;
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public UserDto CreateUser(CreateUserDto createUserDto)
        {
            _logger.LogInformation("Create user via request {createUserDto}", createUserDto);

            User userRequest = _mapper.Map<User>(createUserDto);

            User user = _userRepository.Create(userRequest);

            return _mapper.Map<UserDto>(user);
        }

        /// <inheritdoc/>
        public UserDto GetUser(Guid userId)
        {
            _logger.LogInformation("Get user by ID {userId}", userId);

            User? user = _userRepository.Get(userId);

            if (user == null)
            {
                _logger.LogInformation("User with {userId} is not found.", userId);

                throw new NotFoundException($"User with ID {userId} not found.");
            }

            return _mapper.Map<UserDto>(user);
        }

        /// <inheritdoc/>
        public UserDto UpdateUser(Guid userId, UpdateUserDto updateUserDto)
        {
            _logger.LogInformation(
                "Update user with ID {userId} via request {updateUserDto}",
                userId,
                updateUserDto);

            User userRequest = _mapper.Map<User>(updateUserDto);
            userRequest.Id = userId;

            User user = _userRepository.Update(userRequest);

            return _mapper.Map<UserDto>(user);
        }

        /// <inheritdoc/>
        public bool DeleteUser(Guid userId)
        {
            _logger.LogInformation("Delete user with ID {userId}", userId);

            return _userRepository.Delete(userId);
        }

        #endregion
    }
}
