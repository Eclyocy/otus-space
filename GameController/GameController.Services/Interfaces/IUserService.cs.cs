using GameController.Services.Models.User;

namespace GameController.Services.Interfaces
{
    /// <summary>
    /// Interface for working with users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Create a user.
        /// </summary>
        UserDto CreateUser(CreateUserDto createUserDto);

        /// <summary>
        /// Retrieve all users.
        /// </summary>
        List<UserDto> GetUsers();

        /// <summary>
        /// Retrieve the user by <paramref name="userId"/>.
        /// </summary>
        UserDto GetUser(Guid userId);

        /// <summary>
        /// Method for getting user by name.
        /// </summary>
        UserDto GetUserByName(string name);

        /// <summary>
        /// Update the user with <paramref name="userId"/>.
        /// </summary>
        UserDto UpdateUser(Guid userId, UpdateUserDto updateUserDto);

        /// <summary>
        /// Delete the user with <paramref name="userId"/>.
        /// </summary>
        bool DeleteUser(Guid userId);
    }
}
