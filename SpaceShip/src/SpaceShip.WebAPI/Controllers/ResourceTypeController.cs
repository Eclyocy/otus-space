using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceShip.Service.Interfaces;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller for actions with resourceType.
    /// </summary>
    [ApiController]
    [Route("api/v1/resourceType")]
    public class ResourceTypeController : ControllerBase
    {
        #region private fields

        private readonly IResourceTypeService _resourceTypeService;

        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResourceTypeController(
            IResourceTypeService resourceTypeService,
            IMapper mapper)
        {
            _resourceTypeService = resourceTypeService;
            _mapper = mapper;
        }

        #endregion

        #region public methods

        #endregion
    }
}
