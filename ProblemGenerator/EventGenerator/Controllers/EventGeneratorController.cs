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
    public class EventGeneratorController : Controller
    {
        private readonly IGeneratorService _eventService;

        private readonly IMapper _mapper;

        public EventGeneratorController(IGeneratorService service, IMapper mapper)
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
        public CreateGeneratorRequest GetShip(CreateGeneratorRequest generatorRequest)
        {
            return new CreateGeneratorRequest()
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
        public CreateGeneratorResponse CreateEvent(Guid shipId)
        {
            CreateEventDto createEventDto = _eventService.CreateEvent(shipId);
            return _mapper.Map<CreateGeneratorResponse>(createEventDto);
            //return new CreateGeneratorResponse()
            //{
            //    IdShip = Guid.NewGuid()
            //};
        }
    }
}
