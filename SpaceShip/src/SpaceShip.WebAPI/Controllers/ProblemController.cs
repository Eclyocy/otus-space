using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceShip.Service.Interfaces;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller for actions with problem.
    /// </summary>
    [ApiController]
    [Route("api/v1/problem")]
    public class ProblemController : ControllerBase
    {
        #region private fields

        private readonly IProblemService _iProblemService;

        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Конструктор, в качестве параметра передаем сервис для работы с сущностью проблемы
        /// </summary>
        /// <param name="iProblemService">Сервис работы с сущностью проблемы</param>
        /// <param name="mapper">Маппер ответа сервиса</param>
        public ProblemController(
            IProblemService iProblemService,
            IMapper mapper)
        {
            _iProblemService = iProblemService;
            _mapper = mapper;
        }

        #endregion

        #region public methods

        #endregion
    }
}
