using System.Net;

namespace EventGenerator.Services.Exceptions
{
    /// <summary>
    /// Precondition failed (412) exception.
    /// </summary>
    public class PreconditionFailedException : BaseException
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PreconditionFailedException(string message)
            : base(HttpStatusCode.PreconditionFailed, message)
        {
        }

        #endregion
    }
}
