using Microsoft.AspNetCore.Mvc;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/problem")]
    public class ProblemController : ControllerBase
    {
        private readonly IProblemService _iProblemService;

        public ProblemController(IProblemService iProblemService)
        {
            _iProblemService = iProblemService;
        }

        [HttpPost]
        [Route("ProblemAdd")]
        public ProblemDTO Create(ProblemDTO dto)
        {
            return _iProblemService.Create(dto);
        }
    }
}
