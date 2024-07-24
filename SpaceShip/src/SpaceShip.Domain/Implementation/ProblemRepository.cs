﻿using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Implementation
{
    public class ProblemRepository : BaseRepository<Problem>, IProblemRepository
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProblemRepository(EfCoreContext context)
            : base(context)
        {
        }

        #endregion
    }
}
