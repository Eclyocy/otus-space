using AutoMapper;
using GameController.API.Models.User;
using GameController.Services.Interfaces;
using GameController.Services.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GameController.API.Controllers
{
    /// <summary>
    /// Controller for actions with users.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("/api/users")]
    public class UserController : Controller
    {
        #region private fields

        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Create a new user.
        /// </summary>
        [HttpPost]
        [Route("")]
        [SwaggerOperation("Создание пользователя")]
        public UserResponse CreateUser(CreateUserRequest createUserModel)
        {
            CreateUserDto createUserDto = _mapper.Map<CreateUserDto>(createUserModel);
            UserDto userDto = _userService.CreateUser(createUserDto);
            return _mapper.Map<UserResponse>(userDto);
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        [HttpGet]
        [Route("")]
        [SwaggerOperation("Получение списка всех пользователей")]
        public List<UserResponse> GetUsers()
        {
            List<UserDto> userDtos = _userService.GetUsers();
            return _mapper.Map<List<UserResponse>>(userDtos);
        }

        /// <summary>
        /// Get the user.
        /// </summary>
        [HttpGet]
        [Route("{userId}")]
        [SwaggerOperation("Получение информации о пользователе")]
        public UserResponse GetUser(Guid userId)
        {
            UserDto userDto = _userService.GetUser(userId);
            return _mapper.Map<UserResponse>(userDto);
        }

        /// <summary>
        /// Update the user.
        /// </summary>
        [HttpPatch]
        [Route("{userId}")]
        [SwaggerOperation("Обновление информации о пользователе")]
        public UserResponse UpdateUser(Guid userId, UpdateUserRequest updateUserModel)
        {
            UpdateUserDto updateUserDto = _mapper.Map<UpdateUserDto>(updateUserModel);
            UserDto userDto = _userService.UpdateUser(userId, updateUserDto);
            return _mapper.Map<UserResponse>(userDto);
        }

        /// <summary>
        /// Delete the user.
        /// </summary>
        [HttpDelete]
        [Route("{userId}")]
        [SwaggerOperation("Удаление пользователя")]
        public void DeleteUser(Guid userId)
        {
            _userService.DeleteUser(userId);
        }

        #endregion
    }
}
