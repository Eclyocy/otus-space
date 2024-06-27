namespace GameController.Services.Helpers
{
    internal static class GuidGenerator
    {
        /// <summary>
        /// Generate <see cref="Guid"/> with a randomised delay.
        /// </summary>
        /// <returns></returns>
        public static async Task<Guid> GenerateGuidAsync()
        {
            Random random = new();
            await Task.Delay(TimeSpan.FromSeconds(random.Next(3, 20)));
            return Guid.NewGuid();
        }
    }
}
