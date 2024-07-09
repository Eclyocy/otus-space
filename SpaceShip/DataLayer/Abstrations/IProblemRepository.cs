
using DataLayer.DTO;

namespace DataLayer.Abstrations
{
    public interface IProblemRepository
    {
        public ProblemDataDTO Create(string name);
        public bool FindByName(string name);
    }
}
