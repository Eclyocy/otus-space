using AutoMapper;
using GameController.Controllers.Models.Ship;
using GameController.Services.Models.Ship;

namespace GameController.API.Mappers
{
    /// <summary>
    /// Mappings for Ship models.
    /// </summary>
    public class ShipMapper : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ShipMapper()
        {
            // Service models -> Controller models
            CreateMap<ShipDto, ShipResponse>();
            CreateMap<ShipResourceDto, ShipResourceResponse>();
        }
    }
}
