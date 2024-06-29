using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

namespace GameController.API.Helpers
{
    /// <summary>
    /// Custom console formatter.
    /// </summary>
    public sealed class CustomConsoleFormatter : ConsoleFormatter
    {
        #region public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomConsoleFormatter()
            : base(nameof(CustomConsoleFormatter))
        {
        }

        /// <summary>
        /// Log a formatted message.
        /// </summary>
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

            textWriter.WriteLine();
        }

        #endregion
    }
}
