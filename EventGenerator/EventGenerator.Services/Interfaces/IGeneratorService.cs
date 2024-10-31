﻿using EventGenerator.Services.Models.Event;
using EventGenerator.Services.Models.Generator;

namespace EventGenerator.Services.Interfaces
{
    /// <summary>
    /// Interface for working with event generator.
    /// </summary>
    public interface IGeneratorService
    {
        /// <summary>
        /// Create a generator.
        /// </summary>
        GeneratorDto CreateGenerator(CreateGeneratorDto createGeneratorDto);

        /// <summary>
        /// Get the generator.
        /// </summary>
        GeneratorDto GetGenerator(Guid generatorId);

        /// <summary>
        /// Add a trouble coin to the generator.
        /// </summary>
        GeneratorDto AddTroubleCoin(Guid generatorId);

        /// <summary>
        /// Spend a trouble coin to the generator.
        /// </summary>
        GeneratorDto SpendTroubleCoin(Guid generatorId, int troubleCoint);

        /// <summary>
        /// Get list of events generated by the generator.
        /// </summary>
        List<EventDto> GetEvents(Guid generatorId);

        /// <summary>
        /// Generate an event.
        /// Generator may not actually produce a new event:
        /// either due to insufficient trouble coins,
        /// or just to hold onto its funds to have more options later.
        /// </summary>
        EventDto? GenerateEvent(Guid generatorId);
    }
}
