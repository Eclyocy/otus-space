using Microsoft.AspNetCore.Mvc;
using Spaceship.DataLayer.EfClasses;
using IResourceService = ServiceLayer.ResourceServices.IResourceService;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/spaceship")]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _iResourceService;

        public ResourceController(IResourceService resourceService)
        {
            _iResourceService = resourceService;
        }

        [HttpPost]
        [Route("ResourceAdd")]
        public ResourceDTO Create(ResourceDTO resourceDTO)
        {
            return _iResourceService.Create(resourceDTO);
        }
    }
}
