using Microsoft.AspNetCore.Mvc;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/resourceType")]
    public class ResourceTypeController : ControllerBase
    {
        private readonly IResourceTypeService _resourceTypeService;

        public ResourceTypeController(IResourceTypeService resourceTypeService)
        {
            _resourceTypeService = resourceTypeService;
        }

        [HttpPost]
        [Route("ResourceTypemAdd")]
        public ResourceTypeDTO Create()
        {
            return new ResourceTypeDTO { };

            // return _resourceTypeService.Create();
        }
    }
}
