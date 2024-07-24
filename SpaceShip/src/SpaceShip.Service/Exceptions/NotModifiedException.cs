﻿using System.Net;

namespace GameController.Services.Exceptions
{
    /// <summary>
    /// Not modified (304) exception.
    /// </summary>
    public class NotModifiedException : BaseException
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotModifiedException()
            : base(HttpStatusCode.NotModified)
        {
        }

        #endregion
    }
}
