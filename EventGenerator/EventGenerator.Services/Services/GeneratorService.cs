﻿using AutoMapper;
using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Exceptions;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Event;
using EventGenerator.Services.Models.Generator;
using Microsoft.Extensions.Logging;

namespace EventGenerator.Services.Services
{
    /// <summary>
    /// Class for working with generator service.
    /// </summary>
    public class GeneratorService : IGeneratorService
    {
        #region private fields

        /// <summary>
        /// Initial trouble coin value for generator creation.
        /// </summary>
        private const int TroubleCoinCreate = 0;

        private readonly IGeneratorRepository _generatorRepository;

        private readonly IEventService _eventService;

        private readonly ILogger<GeneratorService> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public GeneratorService(
            IGeneratorRepository generatorRepository,
            IEventService eventService,
            ILogger<GeneratorService> logger,
            IMapper mapper)
        {
            _generatorRepository = generatorRepository;

            _eventService = eventService;

            _logger = logger;
            _mapper = mapper;
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public GeneratorDto CreateGenerator(CreateGeneratorDto createGeneratorDto)
        {
            _logger.LogInformation("Create generator by request {createGeneratorDto}", createGeneratorDto);

            Generator generatorRequest = _mapper.Map<Generator>(createGeneratorDto);
            generatorRequest.TroubleCoins = TroubleCoinCreate;
            Generator generator = _generatorRepository.Create(generatorRequest);

            return _mapper.Map<GeneratorDto>(generator);
        }

        /// <inheritdoc/>
        public GeneratorDto GetGenerator(Guid generatorId)
        {
            _logger.LogInformation("Get generator by generator Id {generatorId}", generatorId);

            Generator generator = GetGeneratorFromRepository(generatorId);

            return _mapper.Map<GeneratorDto>(generator);
        }

        /// <inheritdoc/>
        public GeneratorDto AddTroubleCoin(Guid generatorId)
        {
            _logger.LogInformation("Add a trouble coin to the generator {generatorId}.", generatorId);

            Generator generator = GetGeneratorFromRepository(generatorId);

            generator.TroubleCoins++;

            _generatorRepository.Update(generator);

            return _mapper.Map<GeneratorDto>(generator);
        }

        /// <inheritdoc/>
        public List<EventDto> GetEvents(Guid generatorId)
        {
            _logger.LogInformation("Get events generated by the generator {generatorId}.", generatorId);

            Generator generator = GetGeneratorFromRepository(generatorId, includeEvents: true);

            return _mapper.Map<List<EventDto>>(generator.Events);
        }

        /// <inheritdoc/>
        public EventDto? GenerateEvent(Guid generatorId)
        {
            _logger.LogInformation("Generate an event by generator {generatorId}.", generatorId);

            Generator generator = GetGeneratorFromRepository(generatorId);

            if (generator.TroubleCoins == 0)
            {
                _logger.LogInformation("Generator {generatorId} decided to hold onto its trouble coins.", generatorId);

                return null;
            }

            _logger.LogInformation("Generator {generatorId} has generated an event with.", generatorId);

            EventDto eventEntity = _eventService.CreateEvent(
                new()
                {
                    GeneratorId = generatorId,
                    TroubleCoins = generator.TroubleCoins
                });

            _logger.LogInformation("Spend a trouble coin to the generator {generatorId} after generated event.", generatorId);
            generator.TroubleCoins -= generator.TroubleCoins;
            _generatorRepository.Update(generator);

            return _mapper.Map<EventDto>(eventEntity);
        }
        #endregion

        #region private methods

        /// <summary>
        /// Get the generator from the repository.
        /// </summary>
        /// <exception cref="NotFoundException">If generator not found.</exception>
        private Generator GetGeneratorFromRepository(Guid generatorId, bool includeEvents = false)
        {
            Generator? generator = _generatorRepository.Get(generatorId, includeEvents: includeEvents);

            if (generator == null)
            {
                _logger.LogError("Generator {generatorId} not found.", generatorId);

                throw new NotFoundException($"Generator {generatorId} not found.");
            }

            return generator;
        }
        #endregion
    }
}
