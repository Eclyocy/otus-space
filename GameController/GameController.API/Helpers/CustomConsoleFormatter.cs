using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

namespace GameController.API.Helpers
{
    /// <summary>
    /// Custom console formatter.
    /// </summary>
    public sealed class CustomConsoleFormatter : ConsoleFormatter
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomConsoleFormatter()
            : base(nameof(CustomConsoleFormatter))
        {
        }

        #endregion

        #region public methods

        /// <summary>
        /// Log a formatted message.
        /// </summary>
        /// <typeparam name="TState"><inheritdoc/></typeparam>
        public override void Write<TState>(
            in LogEntry<TState> logEntry,
            IExternalScopeProvider? scopeProvider,
            TextWriter textWriter)
        {
            // Log timestamp.
            DateTime timestamp = DateTime.UtcNow;
            textWriter.Write(timestamp);

            // Log log level and category.
            textWriter.Write($" [{logEntry.LogLevel}] <{logEntry.Category}> ");

            // Log actual message.
            textWriter.Write(logEntry.State);

            if (logEntry.Exception != null)
            {
                textWriter.Write(logEntry.Exception);
            }

            textWriter.WriteLine();
        }

        #endregion
    }
}
