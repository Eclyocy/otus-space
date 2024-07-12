using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventGenerator.Services.Helpers
{
    internal class GuidGenerator
    {
        public static async Task<Guid> GenerateGuidAsync()
        {
            Random random = new();
            await Task.Delay(TimeSpan.FromSeconds(random.Next(3, 20)));

            return Guid.NewGuid();
        }
    }
}
