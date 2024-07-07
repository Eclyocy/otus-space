namespace GameController.Services.Helpers
{
    /// <summary>
    /// Internal class for <see cref="Guid"/> generation.
    /// </summary>
    internal static class GuidGenerator
    {
        /// <summary>
        /// Generate <see cref="Guid"/> with a randomised delay.
        /// </summary>
        public static async Task<Guid> GenerateGuidAsync()
        {
            Random random = new();
            await Task.Delay(TimeSpan.FromSeconds(random.Next(3, 20)));

            return Guid.NewGuid();
        }
    }
}
