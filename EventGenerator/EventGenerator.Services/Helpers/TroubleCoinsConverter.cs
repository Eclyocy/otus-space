using Shared.Enums;

namespace EventGenerator.Services.Helpers
{
    /// <summary>
    /// Helper for converting event levels to trouble coins and reverse.
    /// </summary>
    internal static class TroubleCoinsConverter
    {
        /// <summary>
        /// Get maximum event level which could be bought for <paramref name="troubleCoins"/>.
        /// </summary>
        public static EventLevel? ConvertTroubleCoins(int troubleCoins)
        {
            return troubleCoins switch
            {
                0 => null,
                1 => EventLevel.Low,
                2 => EventLevel.Medium,
                _ => EventLevel.High
            };
        }

        /// <summary>
        /// Get <paramref name="eventLevel"/> cost.
        /// </summary>
        public static int ConvertEventLevel(EventLevel? eventLevel)
        {
            return eventLevel switch
            {
                null => 0,
                EventLevel.Low => 1,
                EventLevel.Medium => 2,
                EventLevel.High => 3,
                _ => throw new ArgumentException(
                    $"Event level {eventLevel} is not supported.")
            };
        }
    }
}
