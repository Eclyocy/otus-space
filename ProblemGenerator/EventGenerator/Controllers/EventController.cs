using AutoMapper;
using EventGenerator.API.Models;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Event;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventGenerator.Controllers
{
    /// <summary>
    /// Controller for actions with events by generator.
    /// </summary>
    [ApiController]
    [Route("/api/events")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventController(IEventService service, IMapper mapper)
        {
            _eventService = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Создать новое событие.
        /// </summary>
        [HttpPost]
        [Route("{shipId}")]
        [SwaggerOperation("Создание нового события")]
        public EventResponse CreateEvent(Guid gereratopId)
        {
            EventDto eventDto = _eventService.CreateEvent(gereratopId);
            return _mapper.Map<EventResponse>(eventDto);
        }
    }
}
