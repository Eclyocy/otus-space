using AutoMapper;
using EventGenerator.API.Models;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Event;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace EventGenerator.Controllers
{
    [ApiController]
    [Route("/api/generators")]
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
        /// �������� - ��������� Guid �������
        /// </summary>
        /// <returns>Guid �������</returns>
        [HttpGet]
        [SwaggerOperation("��������� Guid ������� - ��������")]
        public CreateEventRequest GetShip(CreateEventRequest generatorRequest)
        {
            return new CreateEventRequest()
            {
                IdShip = Guid.NewGuid()
            };

        }

        /// <summary>
        /// ������� ����� �������
        /// </summary>
        /// <returns>200</returns>
        [HttpPost]
        [Route("{shipId}")]
        [SwaggerOperation("�������� ������ �������")]
        public async Task<EventResponse> CreateEvent(Guid shipId)
        {
            EventDto eventDto = await _eventService.CreateEventAsync(shipId);
            
            return _mapper.Map<EventResponse>(eventDto);
        }
    }
}
