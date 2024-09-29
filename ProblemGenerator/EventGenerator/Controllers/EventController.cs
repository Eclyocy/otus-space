using AutoMapper;
using EventGenerator.API.Models;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Event;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventGenerator.Controllers
{
    /// <summary>
    /// Controller for actions with generated events.
    /// </summary>
    [ApiController]
    [Route("/api/generators/{generatorId}/events")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create new event.
        /// </summary>
        [HttpPost]
        [Route("")]
        [SwaggerOperation("Создание нового события")]
        public EventResponse CreateEvent(Guid generatorId)
        {
            EventDto eventDto = _eventService.CreateEvent(generatorId);
            return _mapper.Map<EventResponse>(eventDto);
        }
    }
}
