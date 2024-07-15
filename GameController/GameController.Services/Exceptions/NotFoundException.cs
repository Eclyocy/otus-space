using System.Net;

namespace GameController.Services.Exceptions
{
    /// <summary>
    /// Not found (404) exception.
    /// </summary>
    public class NotFoundException : BaseException
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotFoundException(string message)
            : base(HttpStatusCode.NotFound, message)
        {
        }

        #endregion
    }
}
