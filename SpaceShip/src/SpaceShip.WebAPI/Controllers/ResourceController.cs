using Microsoft.AspNetCore.Mvc;
using SpaceShip.Service.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/resource")]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _iResourceService;

        public ResourceController(IResourceService resourceService)
        {
            _iResourceService = resourceService;
        }
    }
}
