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

            // Log log level.
            string logLevel = $" [{logEntry.LogLevel}] ";
            textWriter.Write(logLevel);

            // Log actual message.
            textWriter.Write(logEntry.State);

            textWriter.WriteLine();
        }

        #endregion
    }
}
