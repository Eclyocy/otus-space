using AutoMapper;
using GameController.Services.Models.Message;
using GameController.Services.Models.Session;

namespace GameController.Services.Mappers
{
    /// <summary>
    /// Mappings for Rabbit MQ messages.
    /// </summary>
    public class RabbitMQMapper : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RabbitMQMapper()
        {
            CreateMap<SessionDto, NewDayMessage>();
        }
    }
}
