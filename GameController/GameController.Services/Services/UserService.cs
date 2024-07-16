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
        public List<UserDto> GetUsers()
        {
            _logger.LogInformation("Get all users");

            List<User> users = _userRepository.GetAll();

            return _mapper.Map<List<UserDto>>(users);
        }

        /// <inheritdoc/>
        public UserDto GetUser(Guid userId)
        {
            _logger.LogInformation("Get user by ID {userId}", userId);

            User user = GetRepositoryUser(userId);

            return _mapper.Map<UserDto>(user);
        }

        /// <inheritdoc/>
        public UserDto UpdateUser(Guid userId, UpdateUserDto updateUserDto)
        {
            _logger.LogInformation(
                "Update user with ID {userId} via request {updateUserDto}",
                userId,
                updateUserDto);

            User user = GetRepositoryUser(userId);

            UpdateAndValidateUser(user, updateUserDto); // updates "user" in-place

            _userRepository.Update(user); // updates "user" in-place

            return _mapper.Map<UserDto>(user);
        }

        /// <inheritdoc/>
        public bool DeleteUser(Guid userId)
        {
            _logger.LogInformation("Delete user with ID {userId}", userId);

            return _userRepository.Delete(userId);
        }

        #endregion

        #region private methods

        /// <summary>
        /// Get user from repository.
        /// </summary>
        /// <exception cref="NotFoundException">
        /// In case the user is not found by the repository.
        /// </exception>
        private User GetRepositoryUser(Guid userId)
        {
            User? user = _userRepository.Get(userId);

            if (user != null)
            {
                return user;
            }

            _logger.LogInformation("User with {userId} is not found.", userId);

            throw new NotFoundException($"User with ID {userId} not found.");
        }

        private static void UpdateAndValidateUser(
            User currentUser,
            UpdateUserDto userRequest)
        {
            bool updateRequested = false;

            if (userRequest.Name != null && userRequest.Name != currentUser.Name)
            {
                updateRequested = true;
                currentUser.Name = userRequest.Name;
            }

            if (userRequest.PasswordHash != null && userRequest.PasswordHash != currentUser.PasswordHash)
            {
                updateRequested = true;
                currentUser.PasswordHash = userRequest.PasswordHash;
            }

            if (!updateRequested)
            {
                throw new NotModifiedException();
            }
        }

        #endregion
    }
}
