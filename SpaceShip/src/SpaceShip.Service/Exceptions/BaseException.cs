using System.Net;

namespace SpaceShip.Services.Exceptions;

/// <summary>
/// Base class for all custom exceptions.
/// </summary>
public abstract class BaseException : Exception
{
    #region constructors

    /// <summary>
    /// Constructor with HTTP status code only.
    /// </summary>
    protected BaseException(HttpStatusCode statusCode)
        : base()
    {
        StatusCode = statusCode;
    }

    /// <summary>
    /// Constructor with HTTP status code and message.
    /// </summary>
    protected BaseException(HttpStatusCode statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
        MessageResponse = message;
    }

    #endregion

    #region public properties

    /// <summary>
    /// Exception details.
    /// </summary>
    public Exception Exception { get; }

    /// <summary>
    /// Response status code.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Response message.
    /// </summary>
    public string MessageResponse { get; }

    #endregion
}
