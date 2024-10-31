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
    /// Service for working with users.
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

            if (_userRepository.GetByName(createUserDto.Name) != null)
            {
                throw new ConflictException("The user is already taken");
            }

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
        public UserDto GetUserByName(string name)
        {
            _logger.LogInformation("Get user by username {name}", name);

            User? user = _userRepository.GetByName(name);

            if (user == null)
            {
                throw new NotFoundException($"User with name {name} not found");
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

            User user = UpdateRepositoryUser(userId, updateUserDto);

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

            if (user == null)
            {
                _logger.LogInformation("User with {userId} is not found.", userId);

                throw new NotFoundException($"User with ID {userId} not found.");
            }

            return user;
        }

  /*      private bool ValidateRepositoryUser(string name, string password) // на уровне авторизации
        {
            User? userFromBd = _userRepository.GetUserByLogin(name);

            if (userFromBd == null)
            {
                return false; // throw new NotFoundException($"User with ID {userId} not found.");
            }

            string hash = HashHelper.HashPassword(password); // это перенести в авторизацию

            if (userFromBd.PasswordHash != hash)
            {
                return false;
            }

            return true;
        }*/

        /// <summary>
        /// Update user in repository.
        /// </summary>
        /// <exception cref="NotFoundException">
        /// In case the user is not found by the repository.
        /// </exception>
        /// <exception cref="NotModifiedException">
        /// In case no changes are requested.
        /// </exception>
        private User UpdateRepositoryUser(
            Guid userId,
            UpdateUserDto userRequest)
        {
            User currentUser = GetRepositoryUser(userId);

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

            _userRepository.Update(currentUser); // updates entity in-place

            return currentUser;
        }

        #endregion
    }
}
