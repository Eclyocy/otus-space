using AutoMapper;
using GameController.API.Models.User;
using GameController.Services.Interfaces;
using GameController.Services.Models.User;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GameController.API.Controllers
{
    /// <summary>
    /// Controller for actions with users.
    /// </summary>
    [ApiController]
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

        [HttpPost]
        [Route("")]
        [SwaggerOperation("Создание пользователя")]
        public UserModel CreateUser(CreateUserModel createUserModel)
        {
            CreateUserDto createUserDto = _mapper.Map<CreateUserDto>(createUserModel);
            UserDto userDto = _userService.CreateUser(createUserDto);
            return _mapper.Map<UserModel>(userDto);
        }

        [HttpGet]
        [Route("{userId}")]
        [SwaggerOperation("Получение информации о пользователе")]
        public UserModel GetUser(Guid userId)
        {
            UserDto userDto = _userService.GetUser(userId);
            return _mapper.Map<UserModel>(userDto);
        }

        [HttpPatch]
        [Route("{userId}")]
        [SwaggerOperation("Обновление информации о пользователе")]
        public UserModel UpdateUser(Guid userId, UpdateUserModel updateUserModel)
        {
            UpdateUserDto updateUserDto = _mapper.Map<UpdateUserDto>(updateUserModel);
            UserDto userDto = _userService.UpdateUser(userId, updateUserDto);
            return _mapper.Map<UserModel>(userDto);
        }

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
