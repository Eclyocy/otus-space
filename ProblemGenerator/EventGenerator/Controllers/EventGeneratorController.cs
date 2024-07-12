using AutoMapper;
using EventGenerator.API.Models;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;


namespace EventGenerator.Controllers
{
    [ApiController]
    [Route("/api/generators")]
    public class EventGeneratorController : Controller
    {
        private readonly ILogger _logger;
        private readonly IGeneratorService _service;
        private readonly IMapper _mapper;

        public EventGeneratorController(IGeneratorService service, ILogger<EventGeneratorController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }    

        [HttpGet]
        public CreateGeneratorRequest GetShip(CreateGeneratorRequest generatorRequest)
        {
            return new CreateGeneratorRequest()
            {
                IdShip = Guid.NewGuid()
            };

        }

        [HttpPost]
        public CreateGeneratorResponse CreateGenerator(CreateGeneratorResponse createGeneratorResponse)
        {
            return new CreateGeneratorResponse()
            {
                GeneratorId = Guid.NewGuid()
            };

        }
    }
}
