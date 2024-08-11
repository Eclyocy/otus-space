using AutoMapper;
using EventGenerator.API.Models;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Event;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventGenerator.Controllers
{
    [ApiController]
    [Route("/api/events")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        public EventController(IEventService service, IMapper mapper)
        {
            _eventService = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Заглушка - Получение Guid корабля.
        /// </summary>
        [HttpGet]
        [SwaggerOperation("Получение Guid корабля - заглушка")]
        public CreateEventRequest GetShip(CreateEventRequest eventRequest)
        {
            return new CreateEventRequest()
            {
                IdShip = Guid.NewGuid()
            };
        }

        /// <summary>
        /// Создать новое событие.
        /// </summary>
        [HttpPost]
        [Route("{shipId}")]
        [SwaggerOperation("Создание нового события")]
        public async Task<EventResponse> CreateEvent(Guid shipId)
        {
            EventDto eventDto = await _eventService.CreateEventAsync(shipId);
            return _mapper.Map<EventResponse>(eventDto);
        }
    }
}
