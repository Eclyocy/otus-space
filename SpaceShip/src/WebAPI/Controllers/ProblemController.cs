using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ProblemServices;
using Spaceship.DataLayer.EfClasses;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/spaceship")]
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
