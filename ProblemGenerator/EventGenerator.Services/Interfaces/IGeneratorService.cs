using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventGenerator.Services.Interfaces
{
    public interface IGeneratorService
    {
        Task<Guid> CreateGeneratorAsync();
    }
}
