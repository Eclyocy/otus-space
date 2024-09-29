﻿using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;

namespace EventGenerator.Database.Repository
{
    /// <summary>
    /// Generator repository.
    /// </summary>
    public class GeneratorRepository : BaseRepository<Generator>, IGeneratorRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public GeneratorRepository(EventDBContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
