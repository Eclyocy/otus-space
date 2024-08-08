using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceShip.Service.Interfaces;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller for actions with Resource's.
    /// </summary>
    [ApiController]
    [Route("api/v1/resource")]
    public class ResourceController : ControllerBase
    {
        #region private fields

        private readonly IResourceService _iResourceService;

        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResourceController(
            IResourceService resourceService,
            IMapper mapper)
        {
            _iResourceService = resourceService;
            _mapper = mapper;
        }

        #endregion
    }
}
