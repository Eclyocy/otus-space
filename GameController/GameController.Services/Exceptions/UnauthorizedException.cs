using System.Net;

namespace GameController.Services.Exceptions
{
    /// <summary>
    /// Unauthorized (401) exception.
    /// </summary>
    public class UnauthorizedException : BaseException
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UnauthorizedException(string message)
            : base(HttpStatusCode.Unauthorized, message)
        {
        }

        #endregion
    }
}
