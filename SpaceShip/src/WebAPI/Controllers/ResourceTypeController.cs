using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ResourceTypeServices;
using Spaceship.DataLayer.EfClasses;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/spaceship")]
    public class ResourceTypeController : ControllerBase
    {
        private readonly IResourceTypeService _resourceTypeService;

        public ResourceTypeController(IResourceTypeService resourceTypeService)
        {
            _resourceTypeService = resourceTypeService;
        }

        [HttpPost]
        [Route("ResourceTypemAdd")]
        public ResourceTypeDTO Create(ResourceTypeDTO dto)
        {
            return _resourceTypeService.Create(dto);
        }
    }
}
